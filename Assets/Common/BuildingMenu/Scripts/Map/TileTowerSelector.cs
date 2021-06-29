
using UnityEngine;
using ScriptableObjectArchitecture;

public class TileTowerSelector : MonoBehaviour
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
    private SelectedTower _currentlySelecterTower;

    [SerializeField]
    private IntVariable _golds;
    #endregion

    #region Public Methods
    private void Start() {
        this.rend = GetComponent<Renderer>();
    }

    private void OnMouseDown() {
        if(_golds.Value >= _currentlySelecterTower._SelectedTower.GetComponent<TowerController>().goldCost){
            Instantiate(_currentlySelecterTower._SelectedTower, this.transform);
        }else
        {
            Debug.Log("get rich");
        }
        
    }

    public void OnSelection(){
        
        rend.material = _onSelected;
    }

    public void OnUnSelection(){
        rend.material = _onExit;
    }

    #endregion
  
}
