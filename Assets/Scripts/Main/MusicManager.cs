using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _songs = new();
    public void PlayShortSound(AudioClip clip, float pitch = 1) =>
        Game.Instance.Pool.GetSound().SetClip(clip, pitch);
}
