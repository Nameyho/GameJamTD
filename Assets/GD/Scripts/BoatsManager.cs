using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsManager : MonoBehaviour
{
    [SerializeField] private Boat[] boats;

    public void DockBoat(uint boatToDock)
    {
        if (boatToDock < boats.Length)
            boats[boatToDock].DockBoat();
    }

    public void UndockBoat(uint boatToUndock)
    {
        if (boatToUndock < boats.Length)
            boats[boatToUndock].UndockBoat();
    }

    public void DockAllBoatsAtOnce()
    {
        foreach (var item in boats)
        {
            item.DockBoat();
        }
    }

    public void UndockAllBoatsAtOnce()
    {
        foreach (var item in boats)
        {
            item.UndockBoat();
        }
    }
}
