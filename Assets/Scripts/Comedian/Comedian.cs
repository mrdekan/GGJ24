using UnityEngine;

public class Comedian : MonoBehaviour
{
    private int funScale = 0;
    public bool TellJoke(Joke joke)
    {
        if (Random.value * 100 <= joke.FunLvl)
            return true;
        funScale += joke.FunLvl;
        if (Random.value * 100 <= funScale)
            return true;
        return false;
    }
}
