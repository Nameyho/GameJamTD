
using UnityEngine;
using UnityEngine.UI;
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

    private GameObject _tower;

    [SerializeField]
    private GameObject _panel;

    public static  bool IsmenuMustBeOpen = true;
    #endregion

    #region Public Methods
    private void Start() {
        this.rend = GetComponent<Renderer>();
    }

    private void OnMouseDown() {
        Debug.Log(IsmenuMustBeOpen);
        if(IsmenuMustBeOpen){
            // Instantiate(_currentlySelecterTower._SelectedTower, this.transform);
            _panel.SetActive(true);
            _panel.transform.position = Input.mousePosition;
        }else
        {
            Debug.Log("panel close");
            _panel.SetActive(false);
            //Debug.Log("get rich");
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
