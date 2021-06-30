using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Renderer tileRenderer;

    [SerializeField] private Material grassMaterial;
    [SerializeField] private Material roadWEMaterial;
    [SerializeField] private Material roadNSMaterial;
    [SerializeField] private Material roadTurnSWMaterial;
    [SerializeField] private Material roadTurnSEMaterial;
    [SerializeField] private Material roadTurnNWMaterial;
    [SerializeField] private Material roadTurnNEMaterial;
    [SerializeField] private Material roadCrossMaterial;

    [SerializeField] private GameObject constructionZone;


    //[SerializeField] private bool constructable = false;

    public enum TileType
    {
        grass,
        roadWE,
        roadNS,
        roadTurnSW,
        roadTurnSE,
        roadTurnNW,
        roadTurnNE,
        roadCross,
        nonConstructible
    }

    [Header("Parameters")]
    [SerializeField] private TileType tileType = TileType.grass;

    private void OnValidate()
    {
        UpdateTile();
    }

    private void UpdateTile()
    {
        //constructionZone.SetActive(constructable);
        Material newMaterial = null;

        bool constructible = false;

        switch (tileType)
        {
            case TileType.grass:
                constructible = true;
                if (grassMaterial)
                    newMaterial = grassMaterial;
                break;
            case TileType.roadWE:
                if (roadWEMaterial)
                    newMaterial = roadWEMaterial;
                break;
            case TileType.roadNS:
                if (roadNSMaterial)
                    newMaterial = roadNSMaterial;
                break;
            case TileType.roadTurnSW:
                if (roadTurnSWMaterial)
                    newMaterial = roadTurnSWMaterial;
                break;
            case TileType.roadTurnSE:
                if (roadTurnSEMaterial)
                    newMaterial = roadTurnSEMaterial;
                break;
            case TileType.roadTurnNW:
                if (roadTurnNWMaterial)
                    newMaterial = roadTurnNWMaterial;
                break;
            case TileType.roadTurnNE:
                if (roadTurnNEMaterial)
                    newMaterial = roadTurnNEMaterial;
                break;
            case TileType.roadCross:
                if (roadCrossMaterial)
                    newMaterial = roadCrossMaterial;
                break;
            case TileType.nonConstructible:
                if (grassMaterial)
                    newMaterial = grassMaterial;
                break;
            default:
                break;
        }

        constructionZone.SetActive(constructible);

        if (newMaterial)
            tileRenderer.material = newMaterial;
    }
}
