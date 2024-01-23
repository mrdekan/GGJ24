using System.Collections.Generic;
using UnityEngine;

public class JokesManager : MonoBehaviour
{
    public List<Joke> Jokes { get; private set; }
    public bool HasJokes => Jokes != null && Jokes.Count > 0;
    public List<Joke> UserJokes { get; private set; }
    public List<Joke> SelectedJokes { get; private set; }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadJokes()
    {
        UserJokes = FileWorker.LoadUserJokes();
        SelectedJokes = FileWorker.LoadSelectedJokes();
    }
    public void SetJokes(List<Joke> jokes)
    {
        Jokes = jokes;
        FileWorker.SaveSelectedJokes(jokes);
    }
}
