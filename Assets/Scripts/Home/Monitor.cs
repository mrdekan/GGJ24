using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private GameObject _screenPanel;
    [SerializeField] private Printer _printer;
    private PlayerMovement _playerMovement;
    private bool _enabled = false;
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }
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
    public void Print()
    {
        var jokes = new List<Joke>
        {
            new(5, "joke joke joke", "Joke title!"),
            new(10, "joka joka joka", "Joke tutel!"),
            new(3, "joka joka joka", "Joke tutelki!"),
            new(5, "joka joka joka", "Joke tutturu!"),
            new(15, "joka joka joka", "Joke tutellya!")
        };
        ReleaseScreen();
        _printer.Print(jokes);
    }
}
