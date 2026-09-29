using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    [SerializeField] private Camera _playerCamera;

    private void Start()
    {
        _playerCamera.gameObject.SetActive(photonView.IsMine);
    }
}
