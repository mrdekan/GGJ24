﻿using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        XandY,
        X,
        Y
    }
    public RotationAxes _axes = RotationAxes.XandY;
    public float _rotationSpeedHor = 2f;
    public float _rotationSpeedVer = 2f;

    public float _minVer = -45f;
    public float _maxVer = 45f;

    private float _rotationX = 0;
    private bool canRotate = true;
    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }
    public void BanRotation() => canRotate = false;
    public void AllowRotation() => canRotate = true;
    public void RotateTo(Vector3 targetRotationVector)
    {
        StartCoroutine(RotateTowardsTarget(targetRotationVector));
    }

    private IEnumerator RotateTowardsTarget(Vector3 targetRotationVector)
    {
        if (_axes == RotationAxes.X) targetRotationVector.x = 0;
        else if (_axes == RotationAxes.Y) targetRotationVector.y = 0;
        Quaternion targetQuaternion = Quaternion.Euler(targetRotationVector);

        float elapsedTime = 0f;
        float rotationTime = 2f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.rotation = targetQuaternion;
    }
    private void Update()
    {
        if (!Game.Instance.Pause.IsPaused && canRotate)
        {
            if (_axes == RotationAxes.XandY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
                _rotationX = Mathf.Clamp(_rotationX, _minVer, _maxVer);

                float delta = Input.GetAxis("Mouse X") * _rotationSpeedHor;
                float _rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
            }
            else if (_axes == RotationAxes.X)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSpeedHor, 0);
            }
            else if (_axes == RotationAxes.Y)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
                _rotationX = Mathf.Clamp(_rotationX, _minVer, _maxVer);

                float _rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
            }
        }
    }
}
