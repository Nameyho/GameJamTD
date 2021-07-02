using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(SphereCollider))]
public class EnemySpawner : MonoBehaviour
{
	#region Exposed

	[Header("Spawn Parameter")]
	[SerializeField]
	private float _timeBetweenSpawn = 2f;
	[SerializeField]
	private int _amountToSpawn = 5;
	[SerializeField]
	private int _turnActivation = 0;

    [Header("Boat Animation")]
    [SerializeField]
    private Boat _boat;

    [Header("Enemy")]
	[SerializeField]
	private Vector3Collection _path;
	[SerializeField]
	private EnemyWalker[] _enemyPrefabs;
	[SerializeField, Tooltip("Ordre basé sur la liste des prefabs Enemy")]
	private int[] _spawnProbabilities = new int[] {};
	[SerializeField]
	private int[] _bonusProbabilitiesByTurn = new int[] { };
	[SerializeField]
	private int _amountOfTinyGuy = 3;
	[SerializeField, Range(0, 100)]
	private int _percentSquadChance = 5;

	[SerializeField]
	private EnemyWalker _tinyGuyPrefab;

	[Header("Scriptable Objects")]
	[SerializeField]
	private IntVariable _turn;

	[Header("Events")]
	[SerializeField]
	private GameEvent _nightHasFallen;
	[SerializeField]
	private GameEvent _dayHasDawned;

	#endregion


	#region Unity API

	private void Awake()
	{
		if (_transform == null) { _transform = transform; }
		if (_sphereCollider == null) { _sphereCollider = GetComponent<SphereCollider>(); }
		_baseSpawnAmount = _amountToSpawn;
		_spawnRoutine = SpawnRoutine();
	}

    private void Start()
    {
		_nightHasFallen.AddListener(StartSpawnRoutine);
		_nightHasFallen.AddListener(UpdateProbabilityOnNightPassed);
		_dayHasDawned.AddListener(StopSpawnRoutine);
	}

    #endregion


    #region Main

    private void SpawnEnemy()
	{
		float radius = _sphereCollider.radius;
		float randomPositionX = _transform.position.x + Random.Range(-radius, radius);
		float randomPositionZ = _transform.position.z + Random.Range(-radius, radius);
		int chance = 100 / _percentSquadChance;
		int pickNumber = Random.Range(1, chance);

		var spawnPosition = new Vector3(randomPositionX, _transform.position.y, randomPositionZ);

		var enemyPick = RandomEnemyByProbability();

		if (enemyPick == _tinyGuyPrefab && pickNumber == 1)
        {
			for (int i = 0; i < 4; i++)
			{
				randomPositionX = _transform.position.x + Random.Range(-radius * 1.5f, radius * 1.5f);
				randomPositionZ = _transform.position.z + Random.Range(-radius * 1.5f, radius * 1.5f);

				spawnPosition = new Vector3(randomPositionX, _transform.position.y, randomPositionZ);

				var newTinyGuy = Instantiate(_tinyGuyPrefab, spawnPosition, _transform.rotation);
				newTinyGuy.PathToFollow = _path;
			}
		}
		else
        {
			var newEnemy = Instantiate(enemyPick, spawnPosition, _transform.rotation);
			newEnemy.PathToFollow = _path;
		}


		_amountToSpawn--;
	}

	private IEnumerator SpawnRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(_timeBetweenSpawn);
			SpawnEnemy();

			if (_amountToSpawn == 0)
			{
				StopCoroutine(_spawnRoutine);
				_amountToSpawn = _baseSpawnAmount;
			}
		}
	}

	private void StopSpawnRoutine()
    {
		if (_turnActivation > _turn.Value) return;
		if (_boat != null) { _boat.UndockBoat(); }
		
		StopCoroutine(_spawnRoutine);
    }

	private void StartSpawnRoutine()
	{
		if (_turnActivation > _turn.Value) return;
		if (_boat != null)
		{	
			_boat.DockBoat();
		}

        StartCoroutine(_spawnRoutine);
	}

    #endregion


    #region Utils

	private EnemyWalker RandomEnemyByProbability()
    {
		int index = 0;
		int total = 0;
		int random;

		for (int i = 0; i < _enemyPrefabs.Length; i++)
		{
			if (_spawnProbabilities.Length <= i) continue;
			total += _spawnProbabilities[i];
		}

		random = Random.Range(0, total);

		while (random > 0)
		{
			random -= _spawnProbabilities[index];
			index++;
		}
		index--;

		if (index < 0)
		{
			index = 0;
		}

		return _enemyPrefabs[index];
	}

	private void UpdateProbabilityOnNightPassed()
    {
        for (int i = 0; i < _spawnProbabilities.Length; i++)
        {
			if (_spawnProbabilities.Length <= i) continue;
			_spawnProbabilities[i] += _bonusProbabilitiesByTurn[i];
		}
    }

    #endregion


    #region Private and Protected Members

    private IEnumerator _spawnRoutine;
	private Transform _transform;
	private SphereCollider _sphereCollider;
	private int _baseSpawnAmount;

	#endregion
}