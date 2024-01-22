using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void Triggered(Collider obj);
    public event Triggered OnTrigger;
    private Animator _anim;
    private void Start() =>
        _anim = GetComponent<Animator>();
    private void OnTriggerEnter(Collider obj)
    {
        _anim.SetTrigger("Hide");
        OnTrigger?.Invoke(obj);
    }
    public void DestroyCheckpoint() =>
        Destroy(gameObject);
}
