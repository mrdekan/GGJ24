using System.Collections.Generic;
using UnityEngine;

public class JokesManager : MonoBehaviour
{
    public bool HasJokes => SelectedJokes != null && SelectedJokes.Count > 0;
    public List<Joke> UserJokes { get; private set; }
    public List<Joke> SelectedJokes { get; private set; }
    public delegate void NewJokes(List<Joke> jokes);
    public event NewJokes AddedJokes;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadJokes()
    {
        UserJokes = FileWorker.LoadUserJokes();
        SelectedJokes = new();
    }
    public void AddUserJokes(List<Joke> newJokes)
    {
        UserJokes.AddRange(newJokes);
        AddedJokes?.Invoke(newJokes);
        FileWorker.SaveUserJokes(UserJokes);
    }
    public void SetJokes(List<Joke> jokes)
    {
        SelectedJokes = jokes;
        FileWorker.SaveSelectedJokes(SelectedJokes);
    }
}
