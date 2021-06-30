using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region private
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
        #endregion
}