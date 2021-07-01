
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class TileTowerSelector : MonoBehaviour
{
    #region Private

    Camera cam;
    private Renderer rend;
    private TowerController towerBuilt = null;
    #endregion

    #region Exposed

    [SerializeField]
    private Material _onSelected;
    [SerializeField]
    private Material _onExit;



    [SerializeField]
    private IntVariable _golds;

    private GameObject _tower;

    [SerializeField]
    //private GameObject MapSelector.Instance._panel;

    public static  bool IsmenuMustBeOpen = true;
    #endregion

    #region Public Methods
    private void Start() {
        this.rend = GetComponent<Renderer>();
    }

    private void OnMouseDown() {
        //Debug.Log(IsmenuMustBeOpen);

        if ((IsmenuMustBeOpen) && (this.CompareTag("Building")))
        {
            Debug.Log("Click building");
            // Instantiate(_currentlySelecterTower._SelectedTower, this.transform);
            MapSelector.Instance._constructionMenuCanvas.SetActive(true);
            //MapSelector.Instance._constructionMenuCanvas.transform.position = Input.mousePosition;
            MapSelector.Instance._constructionMenuCanvas.transform.position = transform.position + Vector3.up * 4.9f;
            
        }
        else if (!this.CompareTag("overlay"))
        {
            Debug.Log("panel close");
            IsmenuMustBeOpen = true;
            MapSelector.Instance._constructionMenuCanvas.SetActive(false);
            //Debug.Log("get rich");
        }

    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (towerPrefab && !towerBuilt)
        {
            towerBuilt = Instantiate(towerPrefab, transform).GetComponent<TowerController>();
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
