using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 30)]
    private int _shortSoundsCapacity = 10;
    private ObjectsPool<DeleteAfterPlayingSound> _shortSoundsPool;
    [SerializeField]
    private DeleteAfterPlayingSound _shortSoundPrefab;
    private void Start() => _shortSoundsPool = new(_shortSoundsCapacity);

    public DeleteAfterPlayingSound GetSound()
    {
        if (_shortSoundsPool.HasElements)
            return _shortSoundsPool.Pop();
        return Instantiate(_shortSoundPrefab, Game.Instance.Music.transform);
    }
    public void AddSound(DeleteAfterPlayingSound sound)
    {
        if (!_shortSoundsPool.IsFull)
            _shortSoundsPool.Push(sound);
        else
            Destroy(sound.gameObject);
    }
}
