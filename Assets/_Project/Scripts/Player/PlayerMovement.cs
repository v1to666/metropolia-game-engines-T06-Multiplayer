using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    private CharacterController _characterController;

    private float _speed = 5f;

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

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude > 0.5f)
        {
            input = input.normalized;
        }

        Vector3 targetDirection = transform.right * input.x + transform.forward * input.y;

        Vector3 targetVelocity = targetDirection * _speed * Time.deltaTime;

        _characterController.Move(targetVelocity);
    }
}
