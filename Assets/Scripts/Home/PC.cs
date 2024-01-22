using UnityEngine;
[RequireComponent(typeof(Outline))]
public class PC : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private GameObject monitorUI;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private Monitor _monitor;
    [SerializeField] private Printer _printer;
    private AudioSource _audio;
    private bool state = false;
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _audio = GetComponent<AudioSource>();
        _outline.OutlineWidth = 0;
    }
    public void Interact()
    {
        state = !state;
        monitorUI.SetActive(state);
        Game.Instance.Music.PlayShortSound(_startSound);
        if (state) _audio.Play();
        else _audio.Stop();
        _monitor.SetInteractable(state);
        if (!state) _printer.Stop();
    }

    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
    }
}
