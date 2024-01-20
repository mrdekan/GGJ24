using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileWorker
{
    private const string FILE_PATH = "config.cfg";
    public static GameSettings LoadSettings()
    {
        if (File.Exists(FILE_PATH))
        {
            BinaryFormatter formatter = new();
            using FileStream stream = new(FILE_PATH, FileMode.Open);
            return (GameSettings)formatter.Deserialize(stream);
        }
        else
        {
            return new GameSettings();
        }
    }
    public static void SaveSettings(GameSettings gameSettings)
    {
        BinaryFormatter formatter = new();
        using FileStream stream = new(FILE_PATH, FileMode.Create);
        formatter.Serialize(stream, gameSettings);
    }
}
