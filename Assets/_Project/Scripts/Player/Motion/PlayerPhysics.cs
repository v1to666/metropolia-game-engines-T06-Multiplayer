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
    private float _groundCheckRayDistance = 0.5f;

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
        _verticalVelocity += _gravity * Time.deltaTime;

        _verticalVelocity = Mathf.Clamp(_verticalVelocity, -10f, 10f);
    }

    private void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(_groundCheckPivot.position, Vector3.down, _groundCheckRayDistance);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawRay(_groundCheckPivot.position, Vector3.down * _groundCheckRayDistance);
    }
}