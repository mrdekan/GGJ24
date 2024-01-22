using UnityEngine;

public class Monitor : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private GameObject _screenPanel;
    private PlayerMovement _playerMovement;
    private bool _enabled = false;
    public void SetInteractable(bool state) => _enabled = state;
    public void Interact()
    {
        if (_enabled)
        {
            _screenPanel.SetActive(true);
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

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
