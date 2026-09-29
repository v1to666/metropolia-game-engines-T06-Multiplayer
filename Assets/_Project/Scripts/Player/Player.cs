using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private int _maxHealth = 40;
    
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void OnValueChanged()
    {

    }

    public void GetDamage(int damage)
    {
        _health -= damage;
    }
}
