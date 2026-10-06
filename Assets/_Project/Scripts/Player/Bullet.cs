using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private float _lifeTime = 10f;
    private float _damage;

    private bool _canDealDamage;
    private bool _hasHit;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit)
        {
            return;
        }

        _hasHit = true;

        if (_canDealDamage)
        {
            IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }

    public void Initialize(float damage, float bulletSpeed, PhotonView shooter)
    {
        _damage = damage;
        _canDealDamage = shooter.IsMine;

        Collider[] bulletColliders = GetComponentsInChildren<Collider>();
        Collider[] playerColliders = shooter.GetComponentsInChildren<Collider>(true);

        foreach (Collider bulletCollider in bulletColliders)
        {
            foreach (Collider playerCollider in playerColliders)
            {
                Physics.IgnoreCollision(bulletCollider, playerCollider);
            }
        }

        _rigidBody.linearVelocity = transform.forward * bulletSpeed;
    }
}