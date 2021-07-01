using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Image))]
public class DayNightCycleDisplayer : MonoBehaviour
{
	#region Exposed

	[Header("Scriptable Objects")]
	[SerializeField]
	private IntVariable _daySecondsRemaining;
	[SerializeField]
	private IntVariable _nightSecondsRemaining;

	[Header("Events")]
	[SerializeField]
	private GameEvent _nightHasFallen;
	[SerializeField]
	private GameEvent _dayHasDawned;

	#endregion


	#region Unity API

	private void Start()
	{
		if (_transform == null) { _transform = GetComponent<RectTransform>(); }
		if (_animator == null) { _animator = GetComponent<Animator>(); }
		_nightHasFallen.AddListener(PlayNight);
		_dayHasDawned.AddListener(PlayDay);
		_animator.SetFloat("DayMultiplier", 1f / _daySecondsRemaining);
		_animator.SetFloat("NightMultiplier", 1f / _nightSecondsRemaining);
		PlayDay();
	}

    #endregion


    #region Utils

	private void PlayDay()
    {
		Debug.Log("Play day");
		_animator.SetTrigger("IsDay");
    }

	private void PlayNight()
	{
		Debug.Log("Play night");
		_animator.SetTrigger("IsNight");
	}

	#endregion


	#region Private and Protected Members

	private RectTransform _transform;
	private Animator _animator;

	#endregion
}