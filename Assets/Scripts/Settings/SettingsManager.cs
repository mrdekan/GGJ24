using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private GameSettings gameSettings;
    public delegate void VolumeChange(float volume);
    public event VolumeChange OnEffectsVolumeChange;
    public event VolumeChange OnMusicVolumeChange;


    public void LoadSettings() => gameSettings = FileWorker.LoadSettings();
    public void SaveSettings() => FileWorker.SaveSettings(gameSettings);
    public void RestoreSettings()
    {
        LoadSettings();
        OnEffectsVolumeChange?.Invoke(gameSettings.EffectsVolume);
        OnMusicVolumeChange?.Invoke(gameSettings.MusicVolume);
    }

    public float GetVolume(SoundsType soundType) =>
        soundType switch
        {
            SoundsType.Effect => gameSettings.EffectsVolume,
            SoundsType.Music => gameSettings.MusicVolume,
            _ => 0.5f,
        };

    public void SetVolume(SoundsType soundType, float volume)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        switch (soundType)
        {
            case SoundsType.Effect:
                gameSettings.EffectsVolume = volume;
                OnEffectsVolumeChange?.Invoke(volume);
                break;
            case SoundsType.Music:
                gameSettings.MusicVolume = volume;
                OnMusicVolumeChange?.Invoke(volume);
                break;
        }
    }
}
