using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Rigidbody))]
public class EnemyWalker : MonoBehaviour
{
	#region Exposed

    [Header("Movement")]
	[SerializeField]
	private float _moveSpeed = 2f;
    [SerializeField]
    private float _rotationSpeed = 5f;

    [Header("Path")]
    [SerializeField, Tooltip("Liste de Vector3 que l'ennemis va suivre dans l'ordre")]
    private Vector3Collection _pathToFollow;

    #endregion


    #region Unity API

    private void Awake()
    {
        if(_transform == null) { _transform = transform; }
        if (_rigidbody == null) { _rigidbody = GetComponent<Rigidbody>(); }
        _pathSize = _pathToFollow.Count;
    }

    private void Update()
    {
        MoveToTarget(_pathToFollow[_pathIndex]);
    }

    private void FixedUpdate()
    {
        TurnTowardTarget(_pathToFollow[_pathIndex]);
    }

    #endregion


    #region Utils

    private void MoveToTarget(Vector3 target)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, target, _moveSpeed * Time.deltaTime);

        if (CanMove() && HasReachTarget(target))
        {
            _pathIndex++;
        }
    }

    private void TurnTowardTarget(Vector3 target)
    {
        var targetDirection = target - _transform.position;
        var lookRotation = Quaternion.Euler(Vector3.zero);

        if (targetDirection != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(targetDirection);
        }

        var rotateTowards = Quaternion.RotateTowards(_transform.rotation, lookRotation, _rotationSpeed);

        _rigidbody.MoveRotation(rotateTowards);
    }

    private bool HasReachTarget(Vector3 target)
    {
        return Vector3.Distance(_transform.position, target) == 0;
    }

    private bool CanMove()
    {
        return _pathIndex+1 < _pathSize ? true : false;
    }

    #endregion


    #region Private and Protected Members

    private Transform _transform;
    private Rigidbody _rigidbody;
    private int _pathIndex = 0;
    private int _pathSize;
	
	#endregion
}