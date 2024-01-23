using System.Linq;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameObject oldScreen;
    [SerializeField] private GameObject newScreen;
    [SerializeField] private GameObject oldScreenUI;
    [SerializeField] private GameObject newScreenUI;
    [SerializeField] private GameObject oldPC;
    [SerializeField] private GameObject newPC;
    [SerializeField] private GameObject oldInputDevice;
    [SerializeField] private GameObject newInputDevices;
    private void Start()
    {
        UpdateAllFurniture();
    }
    private bool HasUpgrade(Upgrades upgrade) => Game.Instance.Progress.UnlockedUpgrades.Contains(upgrade);
    public void UpdateAllFurniture()
    {
        bool pc = HasUpgrade(Upgrades.PC);
        oldPC.SetActive(!pc);
        newPC.SetActive(pc);
        bool screen = HasUpgrade(Upgrades.Monitor);
        oldScreen.SetActive(!screen);
        newScreen.SetActive(screen);
        if (oldScreenUI.activeSelf || newScreenUI.activeSelf)
        {
            oldScreenUI.SetActive(!screen);
            newScreenUI.SetActive(screen);
        }
        bool inputDevices = HasUpgrade(Upgrades.InputDevices);
        oldInputDevice.SetActive(!inputDevices);
        newInputDevices.SetActive(inputDevices);
    }
}
