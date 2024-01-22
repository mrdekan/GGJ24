using Assets.Scripts.Settings;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameSettings GameSettings { get; private set; }
    public delegate void VolumeChange(float volume);
    public delegate void ValueChanged(bool value);
    public Languages Language { get => GameSettings.Language ?? Languages.En; }
    public event VolumeChange OnEffectsVolumeChange;
    public event VolumeChange OnMusicVolumeChange;
    public event ValueChanged OnVSyncChange;
    private void Start()
    {
        Application.targetFrameRate = 120;
        OnVSyncChange += ApplyVSync;
    }
    private void ApplyVSync(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
        Debug.Log(QualitySettings.vSyncCount);
    }
    public void SetValue(string name, bool value)
    {
        if (name == "VSync")
        {
            GameSettings.VSync = value;
            OnVSyncChange?.Invoke(value);
        }
    }
    public void LoadSettings()
    {
        GameSettings = FileWorker.LoadSettings();
        GameSettings.VSync ??= false;
    }
    public void SaveSettings() => FileWorker.SaveSettings(GameSettings);
    public void RestoreSettings()
    {
        LoadSettings();
        OnEffectsVolumeChange?.Invoke(GameSettings.EffectsVolume);
        OnMusicVolumeChange?.Invoke(GameSettings.MusicVolume);
        OnVSyncChange?.Invoke(GameSettings.VSync ?? false);
    }

    public float GetVolume(SoundsType soundType) =>
        soundType switch
        {
            SoundsType.Effect => GameSettings.EffectsVolume,
            SoundsType.Music => GameSettings.MusicVolume,
            _ => 0.5f,
        };

    public void SetVolume(SoundsType soundType, float volume)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        switch (soundType)
        {
            case SoundsType.Effect:
                GameSettings.EffectsVolume = volume;
                OnEffectsVolumeChange?.Invoke(volume);
                break;
            case SoundsType.Music:
                GameSettings.MusicVolume = volume;
                OnMusicVolumeChange?.Invoke(volume);
                break;
        }
    }
}
