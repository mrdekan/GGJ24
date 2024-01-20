using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class VolumeController : MonoBehaviour
{
    [SerializeField] private SoundsType _soundType;
    private AudioSource _audio;
    private float _defaultVolume;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _defaultVolume = _audio.volume;
        ApplyVolume(Game.Instance.Settings.GetVolume(_soundType));
        switch (_soundType)
        {
            case SoundsType.Effect:
                Game.Instance.Settings.OnEffectsVolumeChange += ApplyVolume;
                break;
            case SoundsType.Music:
                Game.Instance.Settings.OnMusicVolumeChange += ApplyVolume;
                break;
        }
    }
    public void ApplyVolume(float volume)
    {
        try
        {
            _audio.volume = _defaultVolume * volume;
        }
        catch
        {
            Game.Instance.Settings.OnEffectsVolumeChange -= ApplyVolume;
            Game.Instance.Settings.OnMusicVolumeChange -= ApplyVolume;
        }
    }
}
