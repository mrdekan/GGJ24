using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int Balance => _progress?.Balance ?? 1000;
    public IEnumerable<Upgrades> UnlockedUpgrades => _progress?.UnlockedUpgrades ?? new List<Upgrades>();
    private ProgressSaveModel _progress;
    private UpgradesManager _upgradesManager;
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
    public void Buy(Upgrades upgrade, float price)
    {
        if (_progress.UnlockedUpgrades.Contains(upgrade)) return;
        if (upgrade == Upgrades.Jokes)
        {
            Game.Instance.Jokes.AddUserJokes(GlobalJokesList.GenerateNewJokes(Game.Instance.Jokes.UserJokes, UnlockedUpgrades.Contains(Upgrades.InputDevices)));
            _progress.Balance -= (int)((price * 1000) * (UnlockedUpgrades.Contains(Upgrades.Monitor) ? 0.7 : 1));
            Save();
            return;
        }
        _progress.Balance -= (int)(price * 1000);
        _progress.UnlockedUpgrades.Add(upgrade);
        _upgradesManager?.UpdateAllFurniture();
        Save();
    }
}
