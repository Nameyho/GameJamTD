using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private float _speed = 20;
    [SerializeField]
    private Vector2 _limitX = new Vector2(-50, 50);
    [SerializeField]
    private Vector2 _limitZ = new Vector2(-50, 50);

    #endregion


    #region Unity API

    private void Awake()
    {
        if (_transform == null) { _transform = transform; }
    }

    private void Update()
    {
        MoveController();
        PositionLimit();
    }

    #endregion


    #region Utils

    private void MoveController()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var velocity = direction * _speed * Time.deltaTime;

        _transform.Translate(velocity);
    }

    private void PositionLimit()
    {
        var position = _transform.position;

        position.x = Mathf.Clamp(_transform.position.x, _limitX.x, _limitX.y);
        position.z = Mathf.Clamp(_transform.position.z, _limitZ.x, _limitZ.y);

        _transform.position = position;
    }

    #endregion


    #region Private and Protected Members

    private Transform _transform;

    #endregion
}