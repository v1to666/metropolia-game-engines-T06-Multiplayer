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
        Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);

        bullet.Initialize(_damage, _bulletSpeed);
    }
}
