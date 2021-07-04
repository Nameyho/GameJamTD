using System.Collections;
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
		_nightHasFallen.AddListener(NightToDay);
		_dayHasDawned.AddListener(DayToNight);
		DayToNight();
	}

	private void OnDestroy()
	{
		_nightHasFallen.RemoveListener(NightToDay);
		_dayHasDawned.RemoveListener(DayToNight);
		StopAllCoroutines();
	}

	#endregion


	#region Utils

	private void DayToNight()
    {
		_transform.rotation = Quaternion.Euler(0, 0, 0);
		StartCoroutine(LerpFunction(Quaternion.Euler(0, 0, -180), _daySecondsRemaining.Value));
	}

	private void NightToDay()
	{
		StartCoroutine(LerpFunction(Quaternion.Euler(0, 0, -360), _nightSecondsRemaining.Value));
	}

	IEnumerator LerpFunction(Quaternion endValue, float duration)
	{
		float time = 0;
		Quaternion startValue = _transform.rotation;

		while (time < duration)
		{
			_transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		_transform.rotation = endValue;
	}

	#endregion


	#region Private and Protected Members

	private RectTransform _transform;

	#endregion
}