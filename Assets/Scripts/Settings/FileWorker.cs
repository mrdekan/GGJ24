using Assets.Scripts.Settings;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileWorker
{
    private const string SETTINGS_FILE_PATH = "config.cfg";
    private const string SELECTED_JOKES_FILE_PATH = "selectedJokes.bin";
    private const string USER_JOKES_FILE_PATH = "jokesInvent.bin";
    private const string PROGRESS_FILE_PATH = "progress.bin";
    public static GameSettings LoadSettings()
    {
        if (File.Exists(SETTINGS_FILE_PATH))
        {
            BinaryFormatter formatter = new();
            using FileStream stream = new(SETTINGS_FILE_PATH, FileMode.Open);
            return (GameSettings)formatter.Deserialize(stream);
        }
        else
        {
            var res = new GameSettings();
            SaveSettings(res);
            return res;
        }
    }
    public static void SaveSettings(GameSettings gameSettings)
    {
        BinaryFormatter formatter = new();
        using FileStream stream = new(SETTINGS_FILE_PATH, FileMode.Create);
        formatter.Serialize(stream, gameSettings);
    }
    public static ProgressSaveModel LoadProgress()
    {
        if (File.Exists(PROGRESS_FILE_PATH))
        {
            BinaryFormatter formatter = new();
            using FileStream stream = new(PROGRESS_FILE_PATH, FileMode.Open);
            return (ProgressSaveModel)formatter.Deserialize(stream);
        }
        else
        {
            var res = new ProgressSaveModel();
            SaveProgress(res);
            return res;
        }
    }
    public static void SaveProgress(ProgressSaveModel progress)
    {
        BinaryFormatter formatter = new();
        using FileStream stream = new(PROGRESS_FILE_PATH, FileMode.Create);
        formatter.Serialize(stream, progress);
    }
    public static List<Joke> LoadSelectedJokes()
    {
        if (File.Exists(SELECTED_JOKES_FILE_PATH))
        {
            BinaryFormatter formatter = new();
            using FileStream stream = new(SELECTED_JOKES_FILE_PATH, FileMode.Open);
            return ((JokesSaveModel)formatter.Deserialize(stream)).Jokes;
        }
        else
        {
            var jokes = new List<Joke>();
            SaveSelectedJokes(jokes);
            return jokes;
        }
    }
    public static List<Joke> LoadUserJokes()
    {
        if (File.Exists(USER_JOKES_FILE_PATH))
        {
            BinaryFormatter formatter = new();
            using FileStream stream = new(USER_JOKES_FILE_PATH, FileMode.Open);
            return ((JokesSaveModel)formatter.Deserialize(stream)).Jokes;
        }
        else
        {
            var jokes = LoadSelectedJokes();
            SaveUserJokes(jokes);
            return jokes;
        }
    }
    public static void SaveUserJokes(List<Joke> jokes) => SaveJokesList(USER_JOKES_FILE_PATH, jokes);
    public static void SaveSelectedJokes(List<Joke> jokes) => SaveJokesList(SELECTED_JOKES_FILE_PATH, jokes);
    private static void SaveJokesList(string path, List<Joke> jokes)
    {
        BinaryFormatter formatter = new();
        using FileStream stream = new(path, FileMode.Create);
        formatter.Serialize(stream, new JokesSaveModel(jokes));
    }
}
