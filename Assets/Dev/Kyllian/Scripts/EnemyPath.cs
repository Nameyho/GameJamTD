using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyPath : MonoBehaviour
{
	#region Exposed

	[SerializeField]
	private Vector3Collection _keyPointsList;

	#endregion


	#region Unity API

	private void Awake()
    {
		if (_transform == null) { _transform = GetComponent<Transform>(); }
		_keyPointsList.Clear();
		SetPath();
	}

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _keyPointsList.Count; i++)
        {
			if (i+1 == _keyPointsList.Count) return;
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_keyPointsList[i], _keyPointsList[i+1]);
		}
    }

    #endregion


    #region Utils

    private void SetPath()
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
			var child = _transform.GetChild(i);
			_keyPointsList.Add(child.transform.position);
		}
    }

	#endregion


	#region Private and Protected Members

	private Transform _transform;

	#endregion
}