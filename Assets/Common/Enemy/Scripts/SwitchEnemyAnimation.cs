using UnityEngine;

public class SwitchEnemyAnimation : MonoBehaviour
{
	#region Exposed

	[SerializeField]
	private Animator[] _animators;
	
	#endregion


	#region Unity API
	
    private void Start()
    {
		int index = Random.Range(0, _animators.Length);
		_animator = _animators[index];

        for (int i = 0; i < _animators.Length; i++)
        {
			if (i == index) continue;
			_animators[i].gameObject.SetActive(false);
		}

		_animator.SetFloat("RunSpeed", 1);
    }
	
	#endregion
	
	
	#region Utils
	
	public void PlayDead()
    {
		var triggers = new string[] { "Death1", "Death2" };
		int index = Random.Range(0, triggers.Length);

		_animator.SetTrigger(triggers[index]);
	}

	#endregion


	#region Utils

	private Animator _animator;

	#endregion
}