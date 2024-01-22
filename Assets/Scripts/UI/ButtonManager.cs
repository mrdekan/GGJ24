using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ButtonManager : MonoBehaviour
{
    private AudioSource _audio;
    private LoadingScene _sceneLoader;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _authorsPanel;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _sceneLoader = GetComponent<LoadingScene>();
    }
    private void Click() => _audio.Play();
    public void Quit() => StartCoroutine(ActionAfterSound(CloseTheGame));
    public void Play() => StartCoroutine(ActionAfterSound(EnterTheGame));
    public void LeaveToMenu() => StartCoroutine(ActionAfterSound(BackToMenu));
    public void LoadHomeScene() => _sceneLoader.LoadScene(2);
    public void ApplySettings()
    {
        Click();
        SaveSettings();
    }
    public void CancelSettings()
    {
        Click();
        RestoreSettings();
    }
    public void OpenSettings()
    {
        Click();
        OpenSettingsPanel();
    }
    public void Resume()
    {
        Click();
        Game.Instance.Pause.SetPause(false);
    }
    public void ToggleAuthors(bool state)
    {
        Click();
        _authorsPanel.SetActive(state);
    }

    private IEnumerator ActionAfterSound(Action callback)
    {
        Click();
        float audioLength = _audio.clip.length;
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + audioLength)
            yield return null;
        callback?.Invoke();
    }
    private void CloseTheGame() => Application.Quit();
    private void SaveSettings()
    {
        Game.Instance.Settings.SaveSettings();
        _settingsPanel.SetActive(false);
    }
    private void RestoreSettings()
    {
        Game.Instance.Settings.RestoreSettings();
        _settingsPanel.SetActive(false);
    }
    private void EnterTheGame() => _sceneLoader.LoadScene(2);
    private void BackToMenu() => _sceneLoader.LoadScene(0);
    private void OpenSettingsPanel() => _settingsPanel.SetActive(true);
}
