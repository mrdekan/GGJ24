using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int Balance => _progress?.Balance ?? 1000;
    public IEnumerable<Upgrades> Upgrades => _progress?.UnlockedUpgrades ?? new List<Upgrades>();
    private ProgressSaveModel _progress;
    public void Load()
    {
        _progress = FileWorker.LoadProgress();
    }
    public void Save()
    {
        FileWorker.SaveProgress(_progress);
    }

}
