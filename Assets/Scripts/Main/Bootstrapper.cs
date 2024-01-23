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
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private GameObject player;
    [SerializeField] private JokesManager jokesManager;
    [SerializeField] private ProgressManager progressManager;
    private void Awake()
    {
        settingsManager.LoadSettings();
        progressManager?.Load();
        jokesManager?.LoadJokes();
        if (jokesManager == null)
            jokesManager = FindAnyObjectByType<JokesManager>();
        if (pauseManager && uiManager)
            pauseManager.OnPauseChange += uiManager.SetPausePanel;
        Game.Instance.SetManagers(settingsManager, musicManager, pauseManager, uiManager, poolManager, mainAction, comedianManager, buttonManager, player, jokesManager, progressManager);
    }
}
