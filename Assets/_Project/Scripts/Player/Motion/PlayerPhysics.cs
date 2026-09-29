using Photon.Pun;
using UnityEngine;

public class PlayerPhysics : MonoBehaviourPun
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _groundCheckPivot;

    private float _gravity = -25f;
    private float _jumpHeight = 3f;

    private float _verticalVelocity;
    private bool _isGrounded;

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

        _isGrounded = IsGrounded();

        ApplyGravity();
        Jump();

        _characterController.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }

        _verticalVelocity += _gravity * Time.deltaTime;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private bool IsGrounded()
    {
        return true;
    }
}