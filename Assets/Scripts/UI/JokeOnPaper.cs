using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokeOnPaper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Joke _joke;
    [SerializeField] private List<AudioClip> _clips = new();
    [SerializeField] private List<Image> funLvlImages;
    [SerializeField] private List<Color> funLvlColors;
    [SerializeField] private Button btn;
    private AudioSource _audio;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void SetInteractable(bool state)
    {
        btn.interactable = state;
    }
    public void SetInfo(Joke joke)
    {
        _joke = joke;
        title.text = _joke.Title;
        int lvl = 1;
        switch (joke.Rarity)
        {
            case JokeRarity.Default: lvl = 1; break;
            case JokeRarity.Rare: lvl = 2; break;
            case JokeRarity.Epic: lvl = 3; break;
            case JokeRarity.Legendary: lvl = 4; break;
        }
        for (int i = 0; i < lvl; i++)
            funLvlImages[i].color = funLvlColors[i];
    }
    public void Clicked()
    {
        Game.Instance.Comedians.StartTellingJoke(_joke);
        Game.Instance.Main.StartTellingJoke();
        StartCoroutine(Play());
    }
    private IEnumerator Play()
    {
        _audio.clip = _clips[Random.Range(0, _clips.Count)];
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        Game.Instance.Main.UpdateOneJoke(this);
        Game.Instance.Comedians.TellJoke(_joke);
    }
}
