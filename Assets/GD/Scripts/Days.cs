using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class Days : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private IntVariable _turn;

    void Update()
    {
        if(textMesh)
        {
            textMesh.text = ("Day "+ (_turn.Value + 1));
        }
    }
}
