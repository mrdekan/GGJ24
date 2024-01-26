using UnityEngine;
[RequireComponent(typeof(PlayRandomSound))]
public class Comedian : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Animator _anim;
    private int funScale = 0;
    private PlayRandomSound _sound;
    public void SetFunLvl(int funLvl)
    {
        funScale = funLvl;
        _sound = GetComponent<PlayRandomSound>();
    }
    private void Laugh()
    {
        _anim.SetTrigger("Laugh");
        _particles.Play();
        _sound.Play();
    }
    public bool TellJoke(Joke joke)
    {
        if (Random.value * 100 <= (int)joke.Rarity)
        {
            Laugh();
            return true;
        }
        funScale += (int)joke.Rarity;
        if (Random.value * 100 <= funScale)
        {
            Laugh();
            return true;
        }
        return false;
    }
}
