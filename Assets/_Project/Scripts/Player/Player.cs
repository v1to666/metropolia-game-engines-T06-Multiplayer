using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviourPun, IDamageable
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _myHealthSlider;

    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _deathScreen;

    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private PlayerView _view;
    [SerializeField] private Weapon _weapon;

    private float _maxHealth = 100f;
    private float _health;

    private bool _isDead;

    private void Start()
    {
        _health = _maxHealth;

        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _health;

        _myHealthSlider.maxValue = _maxHealth;
        _myHealthSlider.value = _health;

        _deathScreen.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if (_isDead)
        {
            return;
        }

        _health -= damage;
        _healthSlider.value = _health;
        _myHealthSlider.value = _health;

        if (_health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;

        _model.SetActive(false);

        if (!photonView.IsMine)
        {
            return;
        }

        _movement.enabled = false;
        _physics.enabled = false;
        _view.enabled = false;
        _weapon.enabled = false;

        _deathScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}