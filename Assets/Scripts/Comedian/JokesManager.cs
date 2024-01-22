using System.Collections.Generic;
using UnityEngine;

public class JokesManager : MonoBehaviour
{
    public List<Joke> Jokes { get; private set; }
    public bool HasJokes => Jokes != null && Jokes.Count > 0;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetJokes(List<Joke> jokes)
    {
        Jokes = jokes;
    }
}
