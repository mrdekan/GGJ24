using UnityEngine;

public class Centerentering : MonoBehaviour
{
    private Animator _anim;
    private void Start() =>
        _anim = GetComponent<Animator>();
    private void OnTriggerEnter(Collider other)
    {
        var plMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (plMovement == null) return;
        Game.Instance.Main.SetPlayerMovement(plMovement);
        plMovement.BanWalking();
        plMovement.MoveTo(new Vector3(0, plMovement.transform.position.y, 0));
        Game.Instance.Main.Wave();
        _anim.SetTrigger("Hide");
    }
    public void DestroyCheckpoint() =>
        Destroy(gameObject);
}
