using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class ButtonSelector : MonoBehaviour
{

    #region Exposed

    [SerializeField] private Button button;

    [SerializeField]
    private GameObject _TowerPrefab;

    [SerializeField]
    private IntVariable _golds;


    [SerializeField]
    private SelectedTile _selectedTitleScriptableObjet;
    #endregion

    #region Methods

    public void OnClick()
    {
        if (_golds.Value >= _TowerPrefab.GetComponent<TowerController>().goldCost)
        {
            _selectedTitleScriptableObjet._CurrentSelectedTile.BuildTower(_TowerPrefab);// Instantiate(_TowerPrefab, _selectedTitleScriptableObjet._CurrentSelectedTile);
        }
    }

    private void Update()
    {
        button.interactable = _golds.Value >= _TowerPrefab.GetComponent<TowerController>().goldCost;

    }
    #endregion
}
