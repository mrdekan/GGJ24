using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class JokesPaper : MonoBehaviour, IInteractable
{
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    private List<Joke> _jokes = new();
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }
    public void Interact()
    {
        Game.Instance.Jokes.SetJokes(_jokes);
        Destroy(gameObject);
    }

    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
    }
    public void WriteJokes(List<Joke> jokes)
    {
        _jokes = jokes;
    }
}
