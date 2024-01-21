using System;

[Serializable]
public class GameSettings
{
    public float EffectsVolume { get; set; }
    public float MusicVolume { get; set; }
    public bool? VSync { get; set; }
    public GameSettings()
    {
        EffectsVolume = 0.5f;
        MusicVolume = 0.5f;
        VSync = false;
    }
}
