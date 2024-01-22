using Assets.Scripts.Settings;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private TextMeshProUGUI _subtitles;
    [SerializeField] private float _subtitlesTimer;
    public void ShowSubtitle(string engSubtitle, string uaSubtitle)
    {
        StopAllCoroutines();
        _subtitles.text = Game.Instance.Settings.Language == Languages.Ua ? uaSubtitle : engSubtitle;
        _subtitles.gameObject.SetActive(true);
        StartCoroutine(HideSubtitle());
    }
    private IEnumerator HideSubtitle()
    {
        yield return new WaitForSeconds(_subtitlesTimer);
        _subtitles.gameObject.SetActive(false);
    }
    public void SetPausePanel(bool isPaused) => _pausePanel.SetActive(isPaused);
}
