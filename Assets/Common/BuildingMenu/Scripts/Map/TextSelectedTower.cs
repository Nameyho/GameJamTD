using UnityEngine;
using UnityEngine.UI;

public class TextSelectedTower : MonoBehaviour
{
    #region exposed

    [SerializeField]
    private SelectedTower _selectedTowerScriptableobject;
    #endregion

    #region Members

    private Text _text;
    #endregion

    #region Unity API

    void Update()
    {
      
      if(_text.text !=_selectedTowerScriptableobject._SelectedTower.name ){
     _text.text =_selectedTowerScriptableobject._SelectedTower.name;
      

      

      }
        
    }


private void Awake() {
        _text = GetComponent<Text>();
       
    }
   #endregion

}
