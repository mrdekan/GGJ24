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
    public void SetManagers(SettingsManager settingsManager, MusicManager musicManager, PauseManager pauseManager, UIManager uIManager, PoolManager poolManager, MainAction main, ComediansManager comedianManager)
    {
        Settings = settingsManager;
        Music = musicManager;
        Pause = pauseManager;
        UI = uIManager;
        Pool = poolManager;
        Main = main;
        Comedians = comedianManager;
    }
}
