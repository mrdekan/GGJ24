using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class DeleteAfterPlayingSound : MonoBehaviour
{
    private AudioSource _audio;
    public void SetClip(AudioClip clip, float pitch = 1)
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = clip;
        _audio.pitch = pitch;
        _audio.Play();
        StartCoroutine(DeleteAfterSound());
    }
    private IEnumerator DeleteAfterSound()
    {
        float audioLength = _audio.clip.length;
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + audioLength)
            yield return null;
        Game.Instance.Pool.AddSound(this);
    }
}
