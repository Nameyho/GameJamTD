using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

public class DayNightCycle : MonoBehaviour
{
	#region Exposed

	[Header("Time")]
	[SerializeField]
	private int _daySeconds = 30;
	[SerializeField]
	private int _nightSeconds = 60;

	[Header("Scriptable Objects")]
	[SerializeField]
	private IntVariable _turn;
	[SerializeField]
	private IntVariable _nightSecondsRemaining;
	[SerializeField]
	private IntVariable _daySecondsRemaining;

	[Header("Events")]
	[SerializeField]
	private GameEvent _nightHasFallen;
	[SerializeField]
	private GameEvent _dayHasDawned;

	#endregion


	#region Unity API

	private void Awake()
    {
		_nightSecondsRemaining.Value = _nightSeconds;
		_daySecondsRemaining.Value = _daySeconds;
		_dayCycle = DayCycle();
		_nightCycle = NightCycle();
	}

    private void Start()
    {
		StartCoroutine(_dayCycle);
    }

    private void OnGUI()
    {
		GUILayout.Label($"TURN: {_turn.Value}");
		GUILayout.Label($"State: {_timeState}");
		GUILayout.Label($"Day seconds remaining: {_daySecondsRemaining.Value}");
		GUILayout.Label($"Night seconds remaining: {_nightSecondsRemaining.Value}");
	}

	#endregion


	#region Main

	private IEnumerator DayCycle()
    {
		while (true)
        {
			yield return new WaitForSeconds(1);
			_daySecondsRemaining.Value -= 1;

			if(_daySecondsRemaining.Value <= 0)
            {
				_daySecondsRemaining.Value = _daySeconds;

				StopCoroutine(_dayCycle);
				StartCoroutine(_nightCycle);
				_timeState = "NIGHT";
				_nightHasFallen.Raise();
			}
        }
    }

	private IEnumerator NightCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			_nightSecondsRemaining.Value -= 1;

			if (_nightSecondsRemaining.Value <= 0)
			{
				_nightSecondsRemaining.Value = _nightSeconds;

				StopCoroutine(_nightCycle);
				StartCoroutine(_dayCycle);
				_dayHasDawned.Raise();
				_timeState = "DAY";
				_turn.Value += 1;
			}
		}
	}

	#endregion


	#region Private and Protected Members

	private IEnumerator _dayCycle;
	private IEnumerator _nightCycle;
	private string _timeState = "DAY";

	#endregion
}