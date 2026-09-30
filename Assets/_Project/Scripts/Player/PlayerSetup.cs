using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _model;

    private void Start()
    {
        _playerCamera.gameObject.SetActive(photonView.IsMine);

        _model.gameObject.SetActive(!photonView.IsMine);
    }
}
