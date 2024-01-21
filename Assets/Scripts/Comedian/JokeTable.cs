using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class JokeTable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Joke _joke;
    [SerializeField] private List<AudioClip> _clips = new();
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
        _audio.clip = _clips[Random.Range(0, _clips.Count)];
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        Game.Instance.Main.UpdateOneJoke(this);
        Game.Instance.Comedians.TellJoke(_joke);
    }
}
