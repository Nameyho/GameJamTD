using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
	#region Exposed

	[SerializeField]
	ParticleSystem _particle;

    #endregion


    #region Unity API

    private void Awake()
    {
        _particle.Play();
    }

    private void Update()
    {
		if (_particle.isPlaying) return;
		Destroy(gameObject);
    }
	
	#endregion
}