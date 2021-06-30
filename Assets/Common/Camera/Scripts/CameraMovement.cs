using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private float _speed = 20;

    #endregion


    #region Unity API

    private void Awake()
    {
        if (_transform == null) { _transform = transform; }
    }

    private void Update()
    {
        MoveController();
    }

    #endregion


    #region Utils

    private void MoveController()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var velocity = direction * _speed * Time.deltaTime;

        _transform.Translate(velocity);
    }

    #endregion


    #region Private and Protected Members

    private Transform _transform;

    #endregion
}