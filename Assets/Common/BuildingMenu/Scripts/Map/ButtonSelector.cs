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

    public bool levelUp = false;

    [SerializeField]
    private SelectedTile _selectedTitleScriptableObjet;
    #endregion

    #region Methods

    public void OnClick()
    {
        if (!_selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt)
        {
            if (_golds.Value >= _TowerPrefab.GetComponent<TowerController>().goldCost)
            {

                _selectedTitleScriptableObjet._CurrentSelectedTile.BuildTower(_TowerPrefab);// Instantiate(_TowerPrefab, _selectedTitleScriptableObjet._CurrentSelectedTile);
            }
        }
        else if (_golds.Value >= _selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt.goldCost)
        {
            _selectedTitleScriptableObjet._CurrentSelectedTile.LevelUpTower();
        }
    }

    public void DeleteTower()
    {
        if (_selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt)
        {
            _selectedTitleScriptableObjet._CurrentSelectedTile.DeleteTower();
        }
    }

    private void Update()
    {
        if(_TowerPrefab)
            button.interactable = _golds.Value >= _TowerPrefab.GetComponent<TowerController>().goldCost;

        if(levelUp && _selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt)
        {
            button.interactable = (_golds.Value >= _selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt.goldCost) && (_selectedTitleScriptableObjet._CurrentSelectedTile.towerBuilt.IsMaxLevel);
        }

    }
    #endregion
}
