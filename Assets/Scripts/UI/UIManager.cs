using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    public void SetPausePanel(bool isPaused) => _pausePanel.SetActive(isPaused);
}
