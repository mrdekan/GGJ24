using Assets.Scripts.Settings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnFirstLoad : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private LanguageSelector eng;
    [SerializeField] private LanguageSelector ua;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI hint;
    [SerializeField] private TextMeshProUGUI button;
    [SerializeField] private string engTitle = "Choose a language";
    [SerializeField] private string uaTitle = "Оберіть мову";
    [SerializeField] private string engHint = "* it cannot be changed in the future";
    [SerializeField] private string uaHint = "* її не можна змінити в майбутньому";
    [SerializeField] private string engButton = "Apply";
    [SerializeField] private string uaButton = "Застосувати";
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private List<ChangeLanguage> textsWithLang = new();
    private Languages selectedLanguage = Languages.En;
    private void Awake()
    {
        if (!FileWorker.HasSettingsFile())
            panel.SetActive(true);
    }
    private IEnumerator PrintCoroutine()
    {
        title.text = "";
        hint.text = "";
        button.text = "";
        char[] titleText = (selectedLanguage == Languages.Ua ? uaTitle : engTitle).ToCharArray();
        char[] hintText = (selectedLanguage == Languages.Ua ? uaHint : engHint).ToCharArray();
        char[] buttonText = (selectedLanguage == Languages.Ua ? uaButton : engButton).ToCharArray();
        int i = 0;
        while (i < titleText.Length || i < hintText.Length || i < buttonText.Length)
        {
            if (i < titleText.Length)
                title.text += titleText[i];
            if (i < hintText.Length)
                hint.text += hintText[i];
            if (i < buttonText.Length)
                button.text += buttonText[i];
            i++;
            yield return new WaitForSeconds(delay);
        }
    }
    private void Update()
    {
        if (panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                ApplyLanguage();
            if (Input.GetAxisRaw("Horizontal") > 0)
                SelectUa();
            else if (Input.GetAxisRaw("Horizontal") < 0)
                SelectEng();
        }
    }
    private void SelectUa()
    {
        if (selectedLanguage == Languages.Ua) return;
        selectedLanguage = Languages.Ua;
        ua.SetSelect(true);
        eng.SetSelect(false);
        StopAllCoroutines();
        StartCoroutine(PrintCoroutine());
    }
    private void SelectEng()
    {
        if (selectedLanguage == Languages.En) return;
        selectedLanguage = Languages.En;
        eng.SetSelect(true);
        ua.SetSelect(false);
        StopAllCoroutines();
        StartCoroutine(PrintCoroutine());
    }
    public void ApplyLanguage()
    {
        Game.Instance.Settings.SetLanguage(selectedLanguage);
        panel.SetActive(false);
        textsWithLang.ForEach(t => { t.UpdateText(); });
    }
    public void SetSelected(LanguageSelector selector)
    {
        var newSelectLang = selector == eng ? Languages.En : Languages.Ua;
        if (newSelectLang == selectedLanguage) return;
        if (selector == eng)
            SelectEng();
        else
            SelectUa();
    }
}
