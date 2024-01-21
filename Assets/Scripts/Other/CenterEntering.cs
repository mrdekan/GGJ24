using UnityEngine;

public class Centerentering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var plMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (plMovement == null) return;

        plMovement.BanWalking();

        plMovement.MoveTo(new Vector3(0, plMovement.transform.position.y, 0));
    }
}
