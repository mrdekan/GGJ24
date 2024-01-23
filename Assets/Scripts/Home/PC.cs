using System.Linq;
using UnityEngine;
public class PC : MonoBehaviour, IInteractable
{
    //private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private GameObject monitorUI;
    [SerializeField] private GameObject newMonitorUI;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private Monitor _monitor;
    [SerializeField] private Printer _printer;
    [SerializeField] private Outline outlineOld;
    [SerializeField] private Outline outlineNew;
    private AudioSource _audio;
    private bool state = false;
    private void Start()
    {
        //_outline = GetComponent<Outline>();
        _audio = GetComponent<AudioSource>();
        SetOutline(false);
    }
    public void SetOutline(bool state)
    {
        if (outlineOld.gameObject.activeSelf)
        {
            outlineOld.OutlineWidth = state ? outlineWidth : 0;
            outlineNew.OutlineWidth = 0;
        }
        else
        {
            outlineNew.OutlineWidth = state ? outlineWidth : 0;
            outlineOld.OutlineWidth = 0;
        }
    }
    public void Interact()
    {
        if (Game.Instance.Pause.IsPaused) return;
        state = !state;
        if (Game.Instance.Progress.UnlockedUpgrades.Contains(Upgrades.Monitor))
        {
            newMonitorUI.SetActive(state);
            monitorUI.SetActive(false);
        }
        else
        {
            monitorUI.SetActive(state);
            newMonitorUI.SetActive(false);
        }
        Game.Instance.Music.PlayShortSound(_startSound);
        if (state) _audio.Play();
        else _audio.Stop();
        _monitor.SetInteractable(state);
        if (!state) _printer.Stop();
    }
    /*public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
    }*/
}
