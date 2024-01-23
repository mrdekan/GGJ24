using UnityEngine;

public class Game
{
    private static Game instance;
    public SettingsManager Settings { get; private set; }
    public MusicManager Music { get; private set; }
    public PauseManager Pause { get; private set; }
    public UIManager UI { get; private set; }
    public PoolManager Pool { get; private set; }
    public MainAction Main { get; private set; }
    public ComediansManager Comedians { get; private set; }
    public ButtonManager Buttons { get; private set; }
    public GameObject Player { get; private set; }
    public JokesManager Jokes { get; private set; }
    public ProgressManager Progress { get; private set; }
    private Game()
    {

    }
    public static Game Instance
    {
        get
        {
            instance ??= new Game();
            return instance;
        }
    }
    public void SetManagers(SettingsManager settingsManager, MusicManager musicManager, PauseManager pauseManager, UIManager uIManager, PoolManager poolManager, MainAction main, ComediansManager comedianManager, ButtonManager buttonManager, GameObject player, JokesManager jokesManager, ProgressManager progressManager)
    {
        Settings = settingsManager;
        Music = musicManager;
        Pause = pauseManager;
        UI = uIManager;
        Pool = poolManager;
        Main = main;
        Comedians = comedianManager;
        Buttons = buttonManager;
        Player = player;
        Jokes = jokesManager;
        Progress = progressManager;
    }
}
