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
        if (_transform == null) { _transform = transform; }
        if (_rigidbody == null) { _rigidbody = GetComponent<Rigidbody>(); }
    }

    private void Start()
    {
        _pathSize = _pathToFollow.Count;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void FixedUpdate()
    {
        TurnTowardTarget();
    }

    #endregion


    #region Main

    private void MoveToTarget()
    {
        if (!_isAlive) return;
        Vector3 target = _pathToFollow[_pathIndex];

        _transform.position = Vector3.MoveTowards(_transform.position, target, _moveSpeed * Time.deltaTime);

        if (CanMoveToLastIndex() && HasReachTarget())
        {
            _pathIndex++;
        }
        else if (HasReachTarget())
        {
            _hasReachedPlayer?.Invoke();
        }
    }

    private void TurnTowardTarget()
    {
        if (!_isAlive) return;
        Vector3 target = _pathToFollow[_pathIndex];
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

    public float GetRemainingDistance()
    {
        float totalLength = Vector3.Distance(_transform.position, _pathToFollow[_pathIndex]);

        for (int i = _pathIndex; i < _pathToFollow.Count; i++)
        {
            if (i+1 == _pathToFollow.Count) continue;

            totalLength += Vector3.Distance(_pathToFollow[i], _pathToFollow[i+1]);
        }
        return totalLength;
    }

    private bool HasReachTarget()
    {
        return Vector3.Distance(_transform.position, _pathToFollow[_pathIndex]) < 1f;
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