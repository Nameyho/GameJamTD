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

   
    #endregion



    #region Methods
    private void Update()
    {

        OnClick();

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

    private void OnClick(){
          RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
          
            if (objectHit.CompareTag("Building"))
            {
            
                TileTowerSelector cube = objectHit.GetComponent<TileTowerSelector>();
                lastcubeSelected = cube;
                TileTowerSelector.IsmenuMustBeOpen = true;
                Debug.Log(cube);
                _selectTileScriptable._CurrentTileTransform = cube.transform;
                cube.OnSelection();
                
                //Debug.Log("entrée");

            }

       
           if(!objectHit.CompareTag("Building")) {
                //Debug.Log("sortie");
                TileTowerSelector.IsmenuMustBeOpen = false;
                
                if(lastcubeSelected)
                    lastcubeSelected.OnUnSelection();
                

            }

                

             
            

        }

    }
        #endregion
}