using UnityEngine;

public class ClosedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private float outlineWidth = 10;
    [SerializeField] private string engText = "Nothing interesting here now. Maybe it's worth looking in other directions.";
    [SerializeField] private string uaText = "Тут нічого цікавого зараз. Можливо, варто подивитися в інших напрямках.";

    private void Start()
    {
        _outline.OutlineWidth = 0;
    }
    public void Interact()
    {
        if (Game.Instance.Pause.IsPaused) return;
        Game.Instance.UI.ShowSubtitle(engText, uaText);
    }

    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
    }
}
