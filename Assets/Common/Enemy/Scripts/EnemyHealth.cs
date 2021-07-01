using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class EnemyHealth : MonoBehaviour
{
	#region Exposed

    [Header("Stats")]
	[SerializeField]
	private float _baseHealth = 10;
    [SerializeField]
    private float _healthMultiplier = 1f;
    [SerializeField]
    private int _goldDropAmount = 10;

    [Header("Scriptable Objects")]
    [SerializeField]
    private IntVariable _turn;
    [SerializeField]
    private IntVariable _playerGold;

    public bool IsAlive { get => _baseHealth > 0; }

    #endregion


    #region Events

    public UnityEvent OnEnemyDead;

    #endregion


    #region Unity API

    private void Start()
    {
        InitHealth();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReceiveDamages(1000);
        }
    }

    #endregion


    #region Main

    public void ReceiveDamages(float damages)
    {
        if (!IsAlive) return;
        _health -= damages;

        if (!CheckIsDead()) return;
        GiveGold();
        OnEnemyDead?.Invoke();
        Destroy(gameObject, 5f);
    }

    public bool CheckIsDead()
    {
        return _health <= 0;
    }

    #endregion


    #region Utils

    private void InitHealth()
    {
        float bonusHealth = (_baseHealth * _turn) * _healthMultiplier;
        _health = _baseHealth;

        _health += bonusHealth;
    }

    private void GiveGold()
    {
        _playerGold.Value += _goldDropAmount;
    }

    #endregion


    #region Private And Protected Members

    private float _health;

    #endregion
}