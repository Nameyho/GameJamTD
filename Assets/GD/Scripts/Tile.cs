using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Renderer tileRenderer;

    [SerializeField] private GameObject constructionZone;

    [SerializeField] private bool constructable = false;

    public enum TileType
    {
        grass,
        sand,
        roadWE,
        roadNS,
        roadTurnSW,
        roadTurnSE,
        roadTurnNW,
        roadTurnNE,
        roadCross
    }

    [SerializeField] private TileType tileType = TileType.grass;

    void Start()
    {
        
    }

    private void UpdateTile()
    {
        constructionZone.SetActive(constructable);
        Material newMaterial = null;

        switch (tileType)
        {
            case TileType.grass:
                break;
            case TileType.sand:
                break;
            case TileType.roadWE:
                break;
            case TileType.roadNS:
                break;
            case TileType.roadTurnSW:
                break;
            case TileType.roadTurnSE:
                break;
            case TileType.roadTurnNW:
                break;
            case TileType.roadTurnNE:
                break;
            case TileType.roadCross:
                break;
            default:
                break;
        }

        if(newMaterial)
            tileRenderer.material = newMaterial;
    }

    void Update()
    {
        
    }
}
