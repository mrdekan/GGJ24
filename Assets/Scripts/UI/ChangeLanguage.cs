using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeLanguage : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private string eng;
    [SerializeField] private string ua;
    private void Awake() => text = GetComponent<TextMeshProUGUI>();
    private void Start() => UpdateText();
    public void UpdateText() => text.text = Game.Instance.Settings.CorrectLanguageString(eng, ua);
}
