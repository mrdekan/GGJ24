using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumeSliderValue;
    [SerializeField] private SoundsType _soundType;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _pitch = 1.2f;
    private void Start()
    {
        UpdateValue(Game.Instance.Settings.GetVolume(_soundType));
        _volumeSlider.onValueChanged.AddListener(delegate { ValueChanged(); });
        switch (_soundType)
        {
            case SoundsType.Effect:
                Game.Instance.Settings.OnEffectsVolumeChange += UpdateValue;
                break;
            case SoundsType.Music:
                Game.Instance.Settings.OnMusicVolumeChange += UpdateValue;
                break;
        }
    }
    private void ValueChanged()
    {
        Game.Instance.Music.PlayShortSound(_audioClip, _pitch);
        float value = _volumeSlider.value / _volumeSlider.maxValue;
        Game.Instance.Settings.SetVolume(_soundType, value);
        SetText(value);
    }
    private void UpdateValue(float value)
    {
        _volumeSlider.value = value * _volumeSlider.maxValue;
        SetText(value);
    }
    private void SetText(float value) => _volumeSliderValue.text = $"{value * 100}%";
}
