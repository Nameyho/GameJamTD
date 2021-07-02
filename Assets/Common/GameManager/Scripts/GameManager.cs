using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.SceneManagement;

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

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _panel;

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
        Time.timeScale = 1f;
    }

    private void OnGUI()
    {
		if (!_isDebug) return;
		GUILayout.Label($"gold: {_gold.Value}");
		GUILayout.Label($"turn: {_turn.Value}");
		GUILayout.Label($"Player life: {_playerLife.Value}");
	}

	private void Update() {
		if(_playerLife.Value <= 0){
            _panel.SetActive(true);
            Time.timeScale = 0f;
        }
	}





    #endregion

	#region Public Methods

	public void ReloadGames(){
		Time.timeScale = 1f;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        



    }
	#endregion
}