using UnityEngine;
using ScriptableObjectArchitecture;

public class GameManager : MonoBehaviour
{
	#region Exposed

	[Header("Game Data")]
	[SerializeField]
	private int _startGold = 100;
	[SerializeField]
	private int _startPlayerLife = 10;
	[SerializeField]
	private int _startTurn = 0;

	[Header("Scriptable Objects")]
	[SerializeField]
	private IntVariable _gold;
	[SerializeField]
	private IntVariable _playerLife;
	[SerializeField]
	private IntVariable _turn;

	[Header("Debug")]
	[SerializeField]
	private bool _isDebug;

	#endregion


	#region Unity API

	private void Awake()
    {
		_gold.Value = _startGold;
		_playerLife.Value = _startPlayerLife;
		_turn.Value = _startTurn;
	}

    private void OnGUI()
    {
		if (!_isDebug) return;
		GUILayout.Label($"gold: {_gold.Value}");
		GUILayout.Label($"turn: {_turn.Value}");
		GUILayout.Label($"Player life: {_playerLife.Value}");
	}

    #endregion
}