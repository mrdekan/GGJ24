using System.Collections.Generic;
using UnityEngine;

public class ComediansManager : MonoBehaviour
{
    [SerializeField] private List<Comedian> _comedians = new();
    [SerializeField] private PlayRandomSound _hystericalLaughter;
    [SerializeField] private PlayRandomSound _audience;
    [SerializeField] private AudioClip _cheering;
    private Joke currentJoke = null;
    public bool IsJokeTelling => currentJoke != null;
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
