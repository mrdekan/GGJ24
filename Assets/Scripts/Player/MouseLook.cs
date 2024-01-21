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

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    void Update()
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
