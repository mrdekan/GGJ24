using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    public bool IsPaused { get; private set; } = false;
    public delegate void PauseChange(bool isPaused);
    public event PauseChange OnPauseChange;
    private bool wasCursorVisible = false;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
            SetPause(!IsPaused);
    }
    public void SetPause(bool isPaused)
    {
        if (isPaused) wasCursorVisible = Cursor.lockState != CursorLockMode.Locked;
        if (isPaused || !isPaused && !wasCursorVisible)
        {
            Cursor.visible = isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }
        IsPaused = isPaused;
        OnPauseChange?.Invoke(IsPaused);
        Time.timeScale = IsPaused ? 0 : 1;
    }
}
