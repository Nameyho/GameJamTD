using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region private

    public Camera cam;

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
            Debug.Log(objectHit.tag);
            if (objectHit.CompareTag("Building"))
            {
                TileTowerSelector cube = objectHit.GetComponent<TileTowerSelector>();
                lastcubeSelected = cube;
                cube.OnSelection();
                
            }else{
                if(lastcubeSelected){
                    Debug.Log(lastcubeSelected);
                    lastcubeSelected.OnUnSelection();
                }
             
            }

        }

        #endregion
    }
}