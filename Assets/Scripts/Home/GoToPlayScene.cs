using UnityEngine;
public class GoToPlayScene : MonoBehaviour, IInteractable
{
    [SerializeField] private LoadingScene _sceneLoader;
    //[SerializeField] private Checkpoint _checkpoint;
    [SerializeField] private string engSubtitle;
    [SerializeField] private string uaSubtitle;
    [SerializeField] private Outline _outline;
    [SerializeField] private float outlineWidth = 10;

    private void Start()
    {
        _outline.OutlineWidth = 0;
    }
    public void Interact()
    {
        if (Game.Instance.Pause.IsPaused) return;
        if (!Game.Instance.Jokes.HasJokes)
        {
            Game.Instance.UI.ShowSubtitle(engSubtitle, uaSubtitle);
            return;
        }
        _sceneLoader.LoadScene(1);
    }

    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
    }
}
