
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class TileTowerSelector : MonoBehaviour
{
    #region Private

    Camera cam;
    private Renderer rend;
    public TowerController towerBuilt = null;
    #endregion

    #region Exposed

    [SerializeField]
    private Material _onSelected;
    [SerializeField]
    private Material _onExit;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private IntVariable _golds;

    [SerializeField]
    //private GameObject MapSelector.Instance._panel;

    public static  bool IsmenuMustBeOpen = true;
    #endregion

    #region Public Methods
    private void Start() {
        this.rend = GetComponent<Renderer>();
    }

    //private void OnMouseDown() {
    //    if(IsmenuMustBeOpen){
    //        // Instantiate(_currentlySelecterTower._SelectedTower, this.transform);
    //        MapSelector.Instance._constructionMenuPanel.SetActive(true);
    //        //MapSelector.Instance._constructionMenuCanvas.transform.position = Input.mousePosition;
    //        MapSelector.Instance._constructionMenuPanel.transform.position = transform.position + Vector3.up * 4.9f;
    //    }
    //    else
    //    {
    //        Debug.Log("panel close");
    //        MapSelector.Instance._constructionMenuPanel.SetActive(false);
    //        //Debug.Log("get rich");
    //    }
        
    //}

    public void BuildTower(GameObject towerPrefab)
    {
        if (towerPrefab && !towerBuilt)
        {
            _audioSource.Play();
            towerBuilt = Instantiate(towerPrefab, transform).GetComponent<TowerController>();
            OnUnSelection();
            MapSelector.Instance.CloseMenu();
        }
    }

    public void LevelUpTower()
    {
        if (towerBuilt)
        {
            _audioSource.Play();
            towerBuilt.LevelUpTower();
            MapSelector.Instance.CloseMenu();
        }
    }

    public void DeleteTower()
    {
        if(towerBuilt)
        {
            _audioSource.Play();
            _golds.Value += towerBuilt.goldCost;
            Destroy(towerBuilt.gameObject);
            towerBuilt = null;
            MapSelector.Instance.CloseMenu();
        }
    }

    public void OnSelection(){
        if(!towerBuilt)
            rend.material = _onSelected;
        
    }

    public void OnUnSelection(){
        rend.material = _onExit;
        
    }

    #endregion
  
}
