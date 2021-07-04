using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

public class LightTransition : MonoBehaviour
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
		if (_light == null) { _light = GetComponent<Light>(); }
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
		StartCoroutine(LerpFunction(_dayColor, _daySecondsRemaining.Value * 0.5f));
	}

	private void NightToDay()
	{
		StartCoroutine(LerpFunction(_nightColor, _nightSecondsRemaining.Value * 0.5f));
	}

	IEnumerator LerpFunction(Color endValue, float duration)
	{
		float time = 0;
		Color startValue = _light.color;

		while (time < duration)
		{
			_light.color = Color.Lerp(startValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		_light.color = endValue;
	}

	#endregion


	#region Private and Protected Members

	private Light _light;
	private Color _dayColor = new Color(1, 0.9568f, 0.8382f);
	private Color _nightColor = new Color(0, 0.012f, 0.34f);

	#endregion
}