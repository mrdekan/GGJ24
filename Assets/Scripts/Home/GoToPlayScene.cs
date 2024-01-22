using System;
using UnityEngine;

public class GoToPlayScene : MonoBehaviour
{
    [SerializeField] private LoadingScene _sceneLoader;
    [SerializeField] private Checkpoint _checkpoint;
    [SerializeField] private string engSubtitle;
    [SerializeField] private string uaSubtitle;
    private void Start() => _checkpoint.OnTrigger += Triggered;
    private void Triggered(Collider col, Action callback)
    {
        if (!Game.Instance.Jokes.HasJokes)
        {
            Game.Instance.UI.ShowSubtitle(engSubtitle, uaSubtitle);
            _checkpoint.PreventDefault();
            return;
        }
        callback();
        _sceneLoader.LoadScene(1);
    }
}
