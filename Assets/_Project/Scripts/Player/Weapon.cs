using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviourPun
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    private float _damage = 10f;
    private float _bulletSpeed = 50f;

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        photonView.RPC(nameof(RPC_Fire), RpcTarget.All, _shootPoint.position, _shootPoint.rotation);
    }

    [PunRPC]
    private void RPC_Fire(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = Instantiate(_bulletPrefab, position, rotation);

        bullet.Initialize(_damage, _bulletSpeed, photonView);
    }
}