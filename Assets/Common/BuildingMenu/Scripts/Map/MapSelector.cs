using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region private

    [SerializeField]
    private SelectedTile _selectTileScriptable;
    private static MapSelector instance;

    public static MapSelector Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<MapSelector>();
            return instance;
        }
    }
    public Camera cam;

    [SerializeField]
    public GameObject _constructionMenuPanel;

    TileTowerSelector lastcubeSelected = null;

    public LayerMask _layerMask;

    #endregion



    #region Methods
    private void Update()
    {

        OnHover();
        if(Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }
    }

    private void LeftClick()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask.value))
        {
            if(lastcubeSelected)
            {
                if(!lastcubeSelected.towerBuilt)
                {
                    _constructionMenuPanel.SetActive(true);
                    _constructionMenuPanel.transform.position = lastcubeSelected.transform.position + Vector3.up * 5.9f;
                }
            }
            //if (!objectHit.CompareTag("Building"))
            //{
            //    _constructionMenuCanvas.SetActive(false);
            //}
        }
        else
        {
            _constructionMenuPanel.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        _constructionMenuPanel.SetActive(false);
        lastcubeSelected = null;
    }

    private void UnselectTile()
    {
        if (lastcubeSelected)
        {
            lastcubeSelected.OnUnSelection();
            lastcubeSelected = null;
        }
    }

    private void OnHover()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask.value))
        {
            Transform objectHit = hit.transform;
            //Debug.Log(objectHit.tag);
            //if (objectHit.CompareTag("Building"))
            //{

            TileTowerSelector cube = objectHit.GetComponent<TileTowerSelector>();
            if(cube)
            {
                if (lastcubeSelected)
                {
                    lastcubeSelected.OnUnSelection();
                }
                lastcubeSelected = cube;
                TileTowerSelector.IsmenuMustBeOpen = true;

                _selectTileScriptable._CurrentSelectedTile = cube;
                cube.OnSelection();
            }

            //Debug.Log("entrée");

            //}


            //if (!objectHit.CompareTag("Building"))
            //{
            //    //Debug.Log("sortie");
            //    //TileTowerSelector.IsmenuMustBeOpen = false;

            //    if (lastcubeSelected)
            //    {
            //        lastcubeSelected.OnUnSelection();
            //        lastcubeSelected = null;
            //    }


            //}
        }
        else
        {
            UnselectTile();
        }
    }
    #endregion
}