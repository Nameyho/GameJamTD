using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private Animator boatAnimator;

    public void DockBoat()
    {
        boatAnimator.SetTrigger("Dock");
    }

    public void UndockBoat()
    {
        boatAnimator.SetTrigger("Leave");
    }

    public bool IsInTransition()
    {
        return boatAnimator.IsInTransition(0);
    }
}
