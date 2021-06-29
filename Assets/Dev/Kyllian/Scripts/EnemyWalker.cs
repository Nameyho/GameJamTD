using UnityEngine;
using UnityEngine.Events;
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

    [Header("Scriptable Object")]
    [SerializeField, Tooltip("Liste de points clés que l'ennemis va suivre dans l'ordre")]
    private Vector3Collection _pathToFollow;

    public bool IsAlive { set => _isAlive = value; }
    public Vector3Collection PathToFollow { set => _pathToFollow = value; }

    #endregion


    #region Events

    public UnityEvent _hasReachedPlayer;

    #endregion


    #region Unity API

    private void Awake()
    {
        if(_transform == null) { _transform = transform; }
        if (_rigidbody == null) { _rigidbody = GetComponent<Rigidbody>(); }
    }

    private void Start()
    {
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


    #region Main

    private void MoveToTarget(Vector3 target)
    {
        if (!_isAlive) return;
        _transform.position = Vector3.MoveTowards(_transform.position, target, _moveSpeed * Time.deltaTime);

        if (CanMoveToLastIndex() && HasReachTarget(target))
        {
            _pathIndex++;
        }
        else if (HasReachTarget(target))
        {
            _hasReachedPlayer?.Invoke();
        }
    }

    private void TurnTowardTarget(Vector3 target)
    {
        if (!_isAlive) return;
        var targetDirection = new Vector3(target.x, _transform.position.y, target.z) - _transform.position;
        var lookRotation = Quaternion.Euler(Vector3.zero);

        if (targetDirection != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(targetDirection);
        }

        var rotateTowards = Quaternion.RotateTowards(_transform.rotation, lookRotation, _rotationSpeed);

        _rigidbody.MoveRotation(rotateTowards);
    }

    #endregion


    #region Utils

    private bool HasReachTarget(Vector3 target)
    {
        return Vector3.Distance(_transform.position, target) < 1f;
    }

    private bool CanMoveToLastIndex()
    {
        return _pathIndex + 1 < _pathSize ? true : false;
    }

    #endregion


    #region Private and Protected Members

    private Transform _transform;
    private Rigidbody _rigidbody;
    private int _pathIndex = 0;
    private int _pathSize;
    private bool _isAlive = true;
	
	#endregion
}