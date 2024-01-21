using System.Collections;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class JokeTable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Joke _joke;
    private AudioSource _audio;
    private bool isInteracted = false;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void SetInfo(Joke joke)
    {
        _joke = joke;
        title.text = _joke.Title;
        content.text = _joke.Text;
    }
    public void Interact()
    {
        if (!isInteracted)
            StartCoroutine(Play());
    }
    private IEnumerator Play()
    {
        isInteracted = true;
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        Game.Instance.Main.UpdateOneJoke(this);
        Game.Instance.Comedians.TellJoke(_joke);
    }
}
