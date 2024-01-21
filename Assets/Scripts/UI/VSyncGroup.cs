using UnityEngine;
using UnityEngine.UI;

public class VSyncGroup : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    public delegate void ToggleChanged(bool state);
    public event ToggleChanged OnToggleChanged;
    private void Start()
    {
        toggle.isOn = Game.Instance.Settings.GameSettings.VSync ?? false;
        Game.Instance.Settings.OnVSyncChange += SetValue;
        toggle.onValueChanged.AddListener(delegate { ValueChanged(); });
    }
    private void SetValue(bool value) =>
        toggle.isOn = value;
    private void ValueChanged() =>
        Game.Instance.Settings.SetValue("VSync", toggle.isOn);
}
