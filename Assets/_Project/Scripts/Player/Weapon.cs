using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float _damage = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();

            Debug.Log("Fire in the hole!");
        }
    }

    private void Fire()
    {
        foreach (RaycastHit raycastHit in Physics.RaycastAll(_camera.transform.position, Vector3.forward, 100f))
        {
            if (raycastHit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}
