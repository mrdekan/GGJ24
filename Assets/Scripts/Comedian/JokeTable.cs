using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class JokeTable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Joke _joke;
    [SerializeField] private List<AudioClip> _clips = new();
    private AudioSource _audio;
    private Outline _outline;
    [Range(0, 10)]
    [SerializeField] private float outlineWidth = 10;
    private bool isInteracted = false;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }
    public void SetInfo(Joke joke)
    {
        _joke = joke;
        title.text = _joke.Title;
        content.text = _joke.Text;
    }
    public void Interact()
    {
        if (!isInteracted && !Game.Instance.Pause.IsPaused)
            StartCoroutine(Play());
    }
    public void SetOutline(bool state)
    {
        _outline.OutlineWidth = state ? outlineWidth : 0;
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
