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
    public GameObject _constructionMenuCanvas;

    TileTowerSelector lastcubeSelected = null;

    public LayerMask _layerMask;
   
    #endregion



    #region Methods
    private void Update()
    {

        OnHover();

    }

       private void OnMouseDown() {
              RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (!objectHit.CompareTag("Building"))
            {
                _constructionMenuCanvas.SetActive(false);
            }

        }
    }

    private void OnHover(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
       



        if (Physics.Raycast(ray, out hit,Mathf.Infinity,_layerMask.value))
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit.tag);
            if (objectHit.CompareTag("Building"))
            {
                
                TileTowerSelector cube = objectHit.GetComponent<TileTowerSelector>();
                lastcubeSelected = cube;
                TileTowerSelector.IsmenuMustBeOpen = true;
                
                _selectTileScriptable._CurrentTileTransform = objectHit.transform;
                cube.OnSelection();
                
                //Debug.Log("entrée");

            }

       
           if(!objectHit.CompareTag("Building")) {
                //Debug.Log("sortie");
                //TileTowerSelector.IsmenuMustBeOpen = false;
                
                if(lastcubeSelected)
                    lastcubeSelected.OnUnSelection();
                

            }

                

             
            

        }else{
           

        }

    }
        #endregion
}