
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    #region Private

    Camera cam;
    private Renderer rend;
    #endregion

    #region Exposed

    [SerializeField]
    private Material _onSelected;
    [SerializeField]
    private Material _onExit;

    [SerializeField]
    private SelectedTower _towerPrefab;
    #endregion

    #region Public Methods
    private void Start() {
        this.rend = GetComponent<Renderer>();
    }

    private void OnMouseDown() {
        Instantiate(_towerPrefab._SelectedTower, this.transform);
    }

    public void OnSelection(){
        
        rend.material = _onSelected;
    }

    public void OnUnSelection(){
        rend.material = _onExit;
    }

    #endregion
  
}
