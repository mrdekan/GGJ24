using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private MainAction mainAction;
    [SerializeField] private ComediansManager comedianManager;
    private void Awake()
    {
        settingsManager.LoadSettings();
        if (pauseManager && uiManager)
            pauseManager.OnPauseChange += uiManager.SetPausePanel;
        Game.Instance.SetManagers(settingsManager, musicManager, pauseManager, uiManager, poolManager, mainAction, comedianManager);
    }
}
