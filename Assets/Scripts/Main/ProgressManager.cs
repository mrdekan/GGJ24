using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int Balance => _progress?.Balance ?? 1000;
    public IEnumerable<Upgrades> UnlockedUpgrades => _progress?.UnlockedUpgrades ?? new List<Upgrades>();
    private ProgressSaveModel _progress;
    private UpgradesManager _upgradesManager;
    public delegate void BalanceUpdated();
    public event BalanceUpdated OnBalanceUpdate;
    private void Start()
    {
        _upgradesManager = GetComponent<UpgradesManager>();
    }
    public void Load()
    {
        _progress = FileWorker.LoadProgress();
    }
    public void Save()
    {
        FileWorker.SaveProgress(_progress);
    }
    public bool Has(Upgrades upgrade) => UnlockedUpgrades.Contains(upgrade);
    public void AddMoney(int money)
    {
        money = Mathf.Max(money, 0);
        _progress.Balance += money;
        Save();
    }
    public void PurchaseMoney(int money, bool force = false)
    {
        if (money > Balance && !force) return;
        else if (money > Balance && force)
        {
            _progress.Balance = 0;
            Save();
            return;
        }
        _progress.Balance -= money;
        Save();
    }
    public void Buy(Upgrades upgrade, float price)
    {
        if (_progress.UnlockedUpgrades.Contains(upgrade) || price * 1000 > Balance) return;
        if (upgrade == Upgrades.Jokes)
        {
            Game.Instance.Jokes.AddUserJokes(GlobalJokesList.GenerateNewJokes(Game.Instance.Jokes.UserJokes, Has(Upgrades.InputDevices), Has(Upgrades.Chair)));
            _progress.Balance -= (int)((price * 1000) * (UnlockedUpgrades.Contains(Upgrades.Monitor) ? 0.7 : 1));
            OnBalanceUpdate?.Invoke();
            Save();
            return;
        }
        PurchaseMoney((int)(price * 1000));
        OnBalanceUpdate?.Invoke();
        _progress.UnlockedUpgrades.Add(upgrade);
        _upgradesManager?.UpdateAllFurniture();
        Save();
    }
}
