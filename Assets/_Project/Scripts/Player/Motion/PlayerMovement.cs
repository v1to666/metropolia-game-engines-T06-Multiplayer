using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    private CharacterController _characterController;

    private float _walkSpeed = 4f;
    private float _runSpeed = 8f;

    private float _currentSpeed;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        _currentSpeed = Mathf.Lerp(_currentSpeed, UpdateSpeed(), 5f * Time.deltaTime);

        Move();
    }

    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude > 0.5f)
        {
            input = input.normalized;
        }

        Vector3 targetDirection = transform.right * input.x + transform.forward * input.y;

        Vector3 targetVelocity = targetDirection * _currentSpeed * Time.deltaTime;

        _characterController.Move(targetVelocity);
    }

    private float UpdateSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return _runSpeed;
        }

        return _walkSpeed;
    }
}
