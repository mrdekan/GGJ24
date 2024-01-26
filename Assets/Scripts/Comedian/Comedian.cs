using UnityEngine;
[RequireComponent(typeof(PlayRandomSound))]
public class Comedian : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Animator _anim;
    private float funScale = 0;
    private PlayRandomSound _sound;
    private int jokeNumber = 0;
    [Range(1, 2)]
    [SerializeField] private float difficulty = 1;
    public void SetFunLvl(int funLvl)
    {
        funScale = funLvl;
        jokeNumber = 0;
        _sound = GetComponent<PlayRandomSound>();
    }
    public void Laugh()
    {
        _anim.SetTrigger("Laugh");
        _particles.Play();
        _sound.Play();
    }
    public bool TellJoke(Joke joke)
    {
        float jokeMultipier = 1;
        switch (jokeNumber)
        {
            case 0:
                jokeMultipier = 2.5f;
                break;
            case 1:
                jokeMultipier = 1.75f;
                break;
        }
        jokeNumber++;
        if (Random.value * 100 * difficulty * jokeMultipier <= (int)joke.Rarity)
        {
            Laugh();
            return true;
        }
        funScale += (int)joke.Rarity * 0.75f;
        if (Random.value * 105 * difficulty <= funScale)
        {
            Laugh();
            return true;
        }
        return false;
    }
}
