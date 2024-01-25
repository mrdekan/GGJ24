using System.Collections.Generic;
using UnityEngine;

public class ComediansManager : MonoBehaviour
{
    [SerializeField] private List<Comedian> _comedians = new();

    public void SetComedianFunLvls(int funLvl)
    {
        foreach (Comedian co in _comedians) co.SetFunLvl(funLvl);
    }
    public bool TellJoke(Joke joke)
    {
        bool res = false;
        foreach (var comedian in _comedians)
            if (comedian.TellJoke(joke))
                res = true;
        if (res)
            Game.Instance.Main.WaveAfterComedianLaugh();
        return res;
    }
}
