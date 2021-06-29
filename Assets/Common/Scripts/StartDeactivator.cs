using UnityEngine;

public class StartDeactivator : MonoBehaviour
{
	#region Unity API
	
    private void Awake()
    {
        gameObject.SetActive(false);
    }
	
	#endregion
}