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

	[SerializeField]
	private IntVariable _turn;

	[Header("Event")]
	[SerializeField]
	private GameEvent _nightHasFallen;
	[SerializeField]
	private GameEvent _dayHasDawned;

	#endregion


	#region Unity API

	private void Awake()
    {
		_nightSecondsRemaining = _nightSeconds;
		_daySecondsRemaining = _daySeconds;
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
		GUILayout.Label($"Day seconds remaining: {_daySecondsRemaining}");
		GUILayout.Label($"Night seconds remaining: {_nightSecondsRemaining}");
	}

	#endregion


	#region Main

	private IEnumerator DayCycle()
    {
		while (true)
        {
			yield return new WaitForSeconds(1);
			_daySecondsRemaining -= 1;

			if(_daySecondsRemaining <= 0)
            {
				_daySecondsRemaining = _daySeconds;

				StopCoroutine(_dayCycle);
				StartCoroutine(_nightCycle);
				_nightHasFallen.Raise();
			}
        }
    }

	private IEnumerator NightCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			_nightSecondsRemaining -= 1;

			if (_nightSecondsRemaining <= 0)
			{
				_nightSecondsRemaining = _nightSeconds;

				StopCoroutine(_nightCycle);
				StartCoroutine(_dayCycle);
				_dayHasDawned.Raise();
				_turn.Value += 1;
			}
		}
	}

	#endregion


	#region Private and Protected Members

	private int _nightSecondsRemaining;
	private int _daySecondsRemaining;
	private IEnumerator _dayCycle;
	private IEnumerator _nightCycle;

	#endregion
}