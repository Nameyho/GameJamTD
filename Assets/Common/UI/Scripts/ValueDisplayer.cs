using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Text))]
public class ValueDisplayer : MonoBehaviour
{
	#region Exposed

	[SerializeField]
	private IntVariable _amount;

    #endregion


    #region Unity API

    private void Awake()
    {
        if (_label == null) { _label = GetComponent<Text>(); }
    }

    private void Update()
    {
        _label.text = $"{_amount.Value}";
    }

	#endregion


	#region Private and Protected Members

	private Text _label;
	
	#endregion
}