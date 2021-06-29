using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyAttacker : MonoBehaviour
{
	#region Exposed

	[Header("Attack")]
	[SerializeField]
	private int _damages = 1;

	[Header("Scriptable Objects")]
	[SerializeField]
	private IntVariable _playerLife;

	#endregion
	
	
	#region Utils
	
	public void AttackPlayerAndDisappear()
    {
		_playerLife.Value -= _damages;
		Destroy(gameObject);
    }
	
	#endregion
}