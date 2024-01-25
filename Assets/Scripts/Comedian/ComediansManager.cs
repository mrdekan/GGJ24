using System.Collections.Generic;
using UnityEngine;

public class ComediansManager : MonoBehaviour
{
    [SerializeField] private List<Comedian> _comedians = new();
    [SerializeField] private PlayRandomSound _hystericalLaughter;
    [SerializeField] private PlayRandomSound _audience;
    [SerializeField] private AudioClip _cheering;
    public void SetComedianFunLvls(int funLvl)
    {
        foreach (Comedian co in _comedians) co.SetFunLvl(funLvl);
    }
    public bool TellJoke(Joke joke)
    {
        if (Random.Range(0, 25) == 0)
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
        return res;
    }
}
