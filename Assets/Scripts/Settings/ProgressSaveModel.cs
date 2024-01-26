using System;
using System.Collections.Generic;

[Serializable]
public class ProgressSaveModel
{
    public List<Upgrades> UnlockedUpgrades { get; set; }
    public int Balance { get; set; }
    public bool TrainingCompleted { get; set; }
    public ProgressSaveModel()
    {
        Balance = 1000;
        UnlockedUpgrades = new();
        TrainingCompleted = false;
    }
}
