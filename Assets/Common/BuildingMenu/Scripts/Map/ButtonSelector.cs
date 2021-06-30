using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelector : MonoBehaviour
{

    #region Exposed

    [SerializeField]
    private GameObject _TowerPrefab;

    [SerializeField]
    private GameObject _GhostTowerPrefab;

    [SerializeField]
    private SelectedTower _SelectedTowerScriptableobject;
    #endregion

    #region Methods

    public void OnClick(){
        _SelectedTowerScriptableobject._SelectedTower = _TowerPrefab;
        _SelectedTowerScriptableobject._GhostTower = _GhostTowerPrefab;
    }

    #endregion
}
