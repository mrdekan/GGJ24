using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void Triggered(Collider obj, Action callback);
    public event Triggered OnTrigger;
    private Animator _anim;
    private void Start() =>
        _anim = GetComponent<Animator>();
    private void OnTriggerEnter(Collider obj)
    {
        OnTrigger?.Invoke(obj, Hide);
    }
    public void PreventDefault()
    {
        StopAllCoroutines();
    }
    private void Hide()
    {
        _anim.SetTrigger("Hide");
    }
    public void DestroyCheckpoint() =>
        Destroy(gameObject);
}
