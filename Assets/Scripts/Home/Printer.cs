using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField] private JokesPaper _paperPrefab;
    [SerializeField] private GameObject _paperSpawn;
    [SerializeField] private float delay = 9;
    private List<Joke> _jokes;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    public void Stop()
    {
        StopAllCoroutines();
        _audio.Stop();
        _anim.SetTrigger("Stop");
    }
    public void Print(List<Joke> jokes)
    {
        _jokes = jokes;
        Stop();
        _audio.Play();
        StartCoroutine(PlayAnim());
    }
    private IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(delay);
        _paperSpawn.SetActive(true);
        _anim.SetTrigger("Print");
    }
    public void SpawnPaper()
    {
        Instantiate(_paperPrefab, _paperSpawn.transform.position, _paperSpawn.transform.rotation).WriteJokes(_jokes);
        _paperSpawn.SetActive(false);
    }
}
