using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	#region Exposed

	[Header("Musics Asset")]
	[SerializeField]
	private AudioClip[] _dayMusics;
	[SerializeField]
	private AudioClip[] _nightMusics;

	[Header("Events")]
	[SerializeField]
	private GameEvent _nightHasFallen;
	[SerializeField]
	private GameEvent _dayHasDawned;

	#endregion


	#region Unity API

	private void Awake()
    {
        if (_audioSource == null) { _audioSource = GetComponent<AudioSource>(); }
		_maxVolume = _audioSource.volume;
	}

    private void Start()
    {
		_dayHasDawned.AddListener(DayMusic);
		_nightHasFallen.AddListener(NightMusic);
		PlayMusic(_dayMusics);
	}

	#endregion


	#region Main

	private void DayMusic()
    {
		StartCoroutine(FadeVolumeAndPlay(_dayMusics));
	}

	private void NightMusic()
	{
		StartCoroutine(FadeVolumeAndPlay(_nightMusics));
	}

	#endregion


	#region Utils

	private void PlayMusic(AudioClip[] musics)
	{
		_audioSource.volume = _maxVolume;

		int randomIndex = Random.Range(0, musics.Length);
		_audioSource.clip = musics[randomIndex];

		_audioSource.Play();
	}

	private IEnumerator FadeVolumeAndPlay(AudioClip[] musics)
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			_lerpValue += 0.005f;

			_audioSource.volume = Mathf.Lerp(_maxVolume, 0, _lerpValue);

			if (_lerpValue >= 1)
			{
				_lerpValue = 0;
				PlayMusic(musics);
				StopAllCoroutines();
			}
		}
	}

	#endregion


	#region Private and Protected Members

	private AudioSource _audioSource;
	private float _lerpValue;
	private float _maxVolume;

	#endregion
}