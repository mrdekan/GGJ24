using System.Collections.Generic;
using UnityEngine;

public class ComediansManager : MonoBehaviour
{
    [SerializeField] private List<Comedian> _comedians = new();
    [SerializeField] private PlayRandomSound _hystericalLaughter;
    [SerializeField] private PlayRandomSound _audience;
    [SerializeField] private AudioClip _cheering;
    private Joke currentJoke = null;
    private bool isTraining = false;
    private int currentJokeNumber = 0;
    public delegate void BaseEvent(int currentJoke);
    public event BaseEvent OnJoke;
    public bool IsJokeTelling => currentJoke != null;
    public void SetIsOnTraining()
    {
        isTraining = true;
    }
    public void StartTellingJoke(Joke joke) => currentJoke = joke;
    public bool AcceptNewJokes = true;
    public void SetComedianFunLvls(int funLvl)
    {
        AcceptNewJokes = true;
        foreach (Comedian co in _comedians) co.SetFunLvl(funLvl);
    }
    public bool TellJoke() => TellJoke(currentJoke);
    public bool TellJoke(Joke joke)
    {
        if (!AcceptNewJokes || currentJoke == null) return false;
        currentJoke = null;

        if (isTraining)
        {
            bool willLaugh = currentJokeNumber == 1;
            if (willLaugh)
            {
                foreach (var co in _comedians)
                {
                    co.Laugh();
                }
                _audience.Play(_cheering);
                Game.Instance.Main.WaveAfterComedianLaugh();
            }
            OnJoke?.Invoke(currentJokeNumber);
            currentJokeNumber++;
            return willLaugh;
        }

        if (Random.Range(0, 12) == 0)
            _hystericalLaughter.Play();
        bool res = false;
        foreach (var comedian in _comedians)
            if (comedian.TellJoke(joke))
                res = true;
        if (res)
        {
            Game.Instance.Main.WaveAfterComedianLaugh();
            _audience.Play(_cheering);
        }
        else
            _audience.Play();
        if (!res && Game.Instance.Main.JokesCount == 1)
        {
            Game.Instance.Main.JokesEnded();
        }
        return res;
    }
}
