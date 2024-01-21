using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed = 6.0f;
    private bool isWalkable = true;
    private CharacterController _characterController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
            Debug.LogError("CharacterController not found on " + gameObject.name);
    }

    private void Update()
    {
        if (!isWalkable) return;
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        Vector3 movement = new Vector3(deltaX, -9.8f, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }

    public void AllowWalking()
    {
        isWalkable = true;
    }

    public void BanWalking()
    {
        isWalkable = false;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        StartCoroutine(MoveToCoroutine(targetPosition));
    }

    private IEnumerator MoveToCoroutine(Vector3 targetPosition)
    {

        while ((transform.position - targetPosition).magnitude > 0.001)
        {
            Vector3 position = targetPosition - transform.position;
            position = _speed * Time.deltaTime * position.normalized;
            _characterController.Move(position);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
