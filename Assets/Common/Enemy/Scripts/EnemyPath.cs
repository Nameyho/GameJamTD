using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyPath : MonoBehaviour
{
	#region Exposed

	[Header("Scriptable Objects")]
	[SerializeField]
	private Vector3Collection _path;

	#endregion


	#region Unity API

	private void Awake()
    {
		if (_transform == null) { _transform = GetComponent<Transform>(); }
		_path.Clear();
		SetPath();
	}

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _path.Count; i++)
        {
			if (i+1 == _path.Count) return;

			var offset = new Vector3(0, 0.1f, 0);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_path[i] + offset, _path[i+1] + offset);
		}
    }

    #endregion


    #region Utils

    private void SetPath()
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
			var child = _transform.GetChild(i);
			_path.Add(child.transform.position);
		}
    }

	#endregion


	#region Private and Protected Members

	private Transform _transform;

	#endregion
}