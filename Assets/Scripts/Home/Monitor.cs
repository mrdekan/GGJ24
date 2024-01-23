using UnityEngine;

public class Monitor : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private GameObject _screenPanel;
    private PCScreen _screen;
    [SerializeField] private Printer _printer;
    private PlayerMovement _playerMovement;
    private bool _enabled = false;
    private void Start()
    {
        _screen = _screenPanel.GetComponent<PCScreen>();
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }
    public void SetInteractable(bool state) => _enabled = state;
    public void Interact()
    {
        if (_enabled && !_screenPanel.activeSelf && !Game.Instance.Pause.IsPaused)
        {
            _screenPanel.SetActive(true);
            _screen.SetJokes(Game.Instance.Jokes.UserJokes, Game.Instance.Jokes.SelectedJokes);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerMovement ??= Game.Instance.Player.GetComponent<PlayerMovement>();
            _playerMovement.BanAllMovement();
        }
    }
    public void ReleaseScreen()
    {
        _screenPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerMovement ??= Game.Instance.Player.GetComponent<PlayerMovement>();
        _playerMovement.AllowAllMovement();
    }
    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state && _enabled ? outlineWidth : 0;
    }
    public void Print()
    {
        ReleaseScreen();
        _printer.Print(_screen.SelectedJokes);
    }
}
