using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region private

    public Camera cam;

    CubeSelector lastcubeSelected = null;
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
                CubeSelector cube = objectHit.GetComponent<CubeSelector>();
                lastcubeSelected = cube;
                cube.OnSelection();
                
            }else{
                Debug.Log(lastcubeSelected);
                lastcubeSelected.OnUnSelection();
            }

        }

        #endregion
    }
}