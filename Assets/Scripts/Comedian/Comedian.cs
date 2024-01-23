using UnityEngine;

public class Comedian : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private int funScale = 0;

    public bool TellJoke(Joke joke)
    {
        if (Random.value * 100 <= (int)joke.Rarity)
        {
            _particles.Play();
            return true;
        }
        funScale += (int)joke.Rarity;
        if (Random.value * 100 <= funScale)
        {
            _particles.Play();
            return true;
        }
        return false;
    }
}
