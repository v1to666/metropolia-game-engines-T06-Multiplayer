using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private float _sensitivity = 2f;

    private float _minXRotation = -85f;
    private float _maxXRotation = 85f;

    private float _yRotation;
    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    public void SetRotation(float xRotation, float yRotation)
    {
        _xRotation = xRotation;
        _yRotation = yRotation;

        LimitXRotation();

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.root.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
    }

    private void Look()
    {
        float xInput = Input.GetAxis("Mouse X") * _sensitivity;
        float yInput = Input.GetAxis("Mouse Y") * _sensitivity;

        _xRotation -= yInput;
        _yRotation += xInput;

        LimitXRotation();

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.root.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
    }

    private void LimitXRotation()
    {
        _xRotation = Mathf.Clamp(_xRotation, _minXRotation, _maxXRotation);
    }
}