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

	[Header("Enemy")]
	[SerializeField]
	private Vector3Collection _path;
	[SerializeField]
	private EnemyWalker _enemy;

	[Header("Debug")]
	[SerializeField]
	private GameObject _gizmo;

	#endregion


	#region Unity API

	private void Awake()
	{
		if (_transform == null) { _transform = transform; }
		if (_sphereCollider == null) { _sphereCollider = GetComponent<SphereCollider>(); }
		_gizmo.SetActive(false);

		_spawnEnemy = SpawnRoutine();

		StartCoroutine(_spawnEnemy);
	}

    #endregion


    #region Main

    private void SpawnEnemy()
	{
		float radius = _sphereCollider.radius;
		float randomPositionX = _transform.position.x + Random.Range(-radius, radius);
		float randomPositionZ = _transform.position.z + Random.Range(-radius, radius);

		var spawnPosition = new Vector3(randomPositionX, _transform.position.y, randomPositionZ);

		var newEnemy = Instantiate(_enemy, spawnPosition, _transform.rotation);
		newEnemy.PathToFollow = _path;
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
				StopCoroutine(_spawnEnemy);
			}
		}
	}

	#endregion


	#region Private and Protected Members

	private IEnumerator _spawnEnemy;
	private Transform _transform;
	private SphereCollider _sphereCollider;

	#endregion
}