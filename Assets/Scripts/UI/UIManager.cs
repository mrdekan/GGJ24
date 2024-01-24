using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private TextMeshProUGUI _subtitles;
    [SerializeField] private float _subtitlesTimer;
    [SerializeField] private float _printDelay = 0.05f;
    private string currentPrintingText = "";
    public void ShowSubtitle(string engSubtitle, string uaSubtitle)
    {
        string temp = Game.Instance.Settings.CorrectLanguageString(engSubtitle, uaSubtitle);
        if (temp == currentPrintingText) return;
        StopAllCoroutines();
        StartCoroutine(PrintCoroutine(temp));
    }
    private IEnumerator PrintCoroutine(string text)
    {
        currentPrintingText = text;
        _subtitles.gameObject.SetActive(true);
        _subtitles.text = "";
        char[] arrayText = text.ToCharArray();
        int i = 0;
        while (i < arrayText.Length)
        {
            if (i < arrayText.Length)
                _subtitles.text += arrayText[i];
            i++;
            yield return new WaitForSeconds(_printDelay);
        }
        StartCoroutine(HideSubtitle());
    }
    private IEnumerator HideSubtitle()
    {
        yield return new WaitForSeconds(_subtitlesTimer);
        _subtitles.gameObject.SetActive(false);
        currentPrintingText = "";
    }
    public void SetPausePanel(bool isPaused) => _pausePanel.SetActive(isPaused);
}
