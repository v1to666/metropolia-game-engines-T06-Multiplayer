using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _healthSlider;

    private float _maxHealth = 100;
    
    private float _health;

    private void Start()
    {
        _health = _maxHealth;

        _healthSlider.value = _health;
    }

    public void OnValueChanged()
    {
        _healthSlider.value = _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        OnValueChanged();

        CheckDead();
    }

    private void CheckDead()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeDamage(10);
        }
    }
}
