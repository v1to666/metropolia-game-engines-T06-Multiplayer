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

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void Start()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _health;

        _myHealthSlider.maxValue = _maxHealth;
        _myHealthSlider.value = _health;

        _myHealthSlider.gameObject.SetActive(photonView.IsMine);

        _deathScreen.SetActive(_isDead && photonView.IsMine);
    }

    public void TakeDamage(float damage)
    {
        photonView.RPC(nameof(RPC_TakeDamage), photonView.Owner, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if (!photonView.IsMine || _isDead || damage <= 0f)
        {
            return;
        }

        _health = Mathf.Max(0f, _health - damage);

        photonView.RPC(nameof(RPC_UpdateHealth), RpcTarget.All, _health);
    }

    [PunRPC]
    private void RPC_UpdateHealth(float health)
    {
        _health = health;

        _healthSlider.value = _health;
        _myHealthSlider.value = _health;

        if (_health <= 0f && !_isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;

        _model.SetActive(false);
        _healthSlider.gameObject.SetActive(false);

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider playerCollider in colliders)
        {
            playerCollider.enabled = false;
        }

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