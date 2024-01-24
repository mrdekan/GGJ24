using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyWakeUp : MonoBehaviour
{
    private Rigidbody _rb;
    private void Start() => _rb = GetComponent<Rigidbody>();
    private void Update() => _rb.WakeUp();
}
