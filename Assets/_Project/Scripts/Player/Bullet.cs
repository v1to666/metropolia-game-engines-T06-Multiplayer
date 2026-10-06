using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidBody;

    float _lifeTime = 10f;
    float _damage;

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
        if (GetComponent<Collider>().TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);

            Destroy(gameObject);
        }

    }

    public void Initialize(float damage, float bulletSpeed)
    {
        _damage = damage;

        _rigidBody.linearVelocity = transform.forward * bulletSpeed;
    }
}
