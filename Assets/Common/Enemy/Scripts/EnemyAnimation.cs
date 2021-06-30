using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	#region Exposed

	[SerializeField]
	private Animator _animator;
	
	#endregion


	#region Unity API
	
    private void Awake()
    {
		_animator.SetFloat("RunSpeed", 1);
    }
	
	#endregion
	
	
	#region Utils
	
	public void PlayDead()
    {
		var triggers = new string[] { "Death1", "Death2"};
		int index = Random.Range(0, triggers.Length);

		_animator.SetTrigger(triggers[index]);
    }
	
	#endregion
}