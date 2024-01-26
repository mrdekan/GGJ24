using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float _speed = 6.0f;
    private bool isWalkable = true;
    private CharacterController _characterController;
    private AudioSource _audio;
    private bool walkedOnPreviousFrame = false;
    [SerializeField] private List<MouseLook> mouseLooks = new();
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
        if (_characterController == null)
            Debug.LogError("CharacterController not found on " + gameObject.name);
    }
    public void BanAllMovement()
    {
        BanWalking();
        foreach (var rotater in mouseLooks)
            rotater.BanRotation();
    }
    public void AllowAllMovement()
    {
        AllowWalking();
        foreach (var rotater in mouseLooks)
            rotater.AllowRotation();
    }
    private void Update()
    {
        if (!isWalkable)
        {
            _audio.Stop();
            return;
        }
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        if (deltaX == 0 && deltaZ == 0 && _audio.isPlaying)
            _audio.Stop();
        else if ((deltaX != 0 || deltaZ != 0) && !walkedOnPreviousFrame && !_audio.isPlaying)
            _audio.Play();
        walkedOnPreviousFrame = deltaX != 0 || deltaZ != 0;
        Vector3 movement = new(deltaX, -9.8f, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }

    public void AllowWalking() =>
        isWalkable = true;

    public void BanWalking() =>
        isWalkable = false;

    public void MoveTo(Vector3 targetPosition, Action callback) =>
        StartCoroutine(MoveToCoroutine(targetPosition, callback));
    public void RotateTo(Vector3 targetRotationVector)
    {
        foreach (var rotater in mouseLooks)
            rotater.RotateTo(targetRotationVector);
    }
    private IEnumerator MoveToCoroutine(Vector3 targetPosition, Action callback)
    {

        while ((transform.position - targetPosition).magnitude > 0.05)
        {
            Vector3 position = targetPosition - transform.position;
            position = _speed / 2 * Time.deltaTime * position.normalized;
            _characterController.Move(position);
            yield return null;
        }

        transform.position = targetPosition;
        callback();
    }
}
