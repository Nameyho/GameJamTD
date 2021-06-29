using UnityEngine;
using UnityEngine.UI;

public class TextSelectedTower : MonoBehaviour
{
    #region exposed

    [SerializeField]
    private SelectedTower _selectedTowerScriptableobject;
    #endregion

    #region Members

    private string _text;
    #endregion

    #region Unity API

    void Update()
    {

     _text = _selectedTowerScriptableobject._SelectedTower.name;
        Debug.Log(_text);
        
    }

    private void Awake() {
        _text = GetComponent<Text>().text;
        Debug.Log(_text);
    }
   #endregion

}
