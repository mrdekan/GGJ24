using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(VolumeController))]
public class PlayRandomSound : MonoBehaviour
{
    private AudioSource _audio;
    [SerializeField] private List<AudioClip> audioClips = new();
    private void Start() =>
        _audio = GetComponent<AudioSource>();
    public void Play() =>
        Play(audioClips[Random.Range(0, audioClips.Count)]);
    public void Play(AudioClip _clip)
    {
        if (!_audio.isPlaying)
        {
            _audio.clip = _clip;
            _audio.Play();
        }
    }
}
