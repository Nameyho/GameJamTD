using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class EnemyHealth : MonoBehaviour
{
	#region Exposed

    [Header("Stats")]
	[SerializeField]
	private int _baseHealth = 10;
    [SerializeField]
    private float _healthMultiplier = 1f;
    [SerializeField]
    private int _goldIncome = 10;

    [Header("Scriptable Objects")]
    [SerializeField]
    private IntVariable _turn;
    [SerializeField]
    private IntVariable _gold;

    #endregion


    #region Events

    public UnityEvent OnEnemyDead;

    #endregion


    #region Unity API

    private void Start()
    {
        InitHealth();
    }

    #endregion


    #region Main

    public void ReceiveDamages(int damages)
    {
        _health -= damages;

        if (!CheckIsDead()) return;
        GiveGold();
        OnEnemyDead?.Invoke();
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

        _health += (int)bonusHealth;
    }

    private void GiveGold()
    {
        _gold.Value += _goldIncome;
    }

    #endregion


    #region Private And Protected Members

    private int _health;

    #endregion
}