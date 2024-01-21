using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed = 6.0f;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
            Debug.LogError("CharacterController not found on " + gameObject.name);
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        Vector3 movement = new Vector3(deltaX, -1, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}
