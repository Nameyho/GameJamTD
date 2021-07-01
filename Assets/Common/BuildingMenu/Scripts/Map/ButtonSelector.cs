using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ButtonSelector : MonoBehaviour
{

    #region Exposed

    [SerializeField]
    private GameObject _TowerPrefab;

    [SerializeField]
    private IntVariable _golds;


    [SerializeField]
    private SelectedTile _selectedTitleScriptableObjet;
    #endregion

    #region Methods

    public void OnClick(){
       if(_golds.Value> _TowerPrefab.GetComponent<TowerController>().goldCost){
            Instantiate(_TowerPrefab, _selectedTitleScriptableObjet._CurrentTileTransform);
        }
    }

    #endregion
}
