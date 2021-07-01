using UnityEngine;
using UnityEngine.UI;

public class TextSelectedTower : MonoBehaviour
{
    #region exposed

    [SerializeField]
    private SelectedTile _selectedTowerScriptableobject;
    #endregion

    #region Members

    private Text _text;
    #endregion

    #region Unity API

    void Update()
    {


      

      
        
    }


private void Awake() {
        _text = GetComponent<Text>();
       
    }
   #endregion

}
