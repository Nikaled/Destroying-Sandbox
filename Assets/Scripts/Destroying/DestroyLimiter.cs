using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLimiter : MonoBehaviour
{
    public static int DestroyedThisWeapon { get; private set; }
    private const int MaxDestroyed = 150;
    public static bool IsWeaponWithLimit;

    public static bool AvailableToDestroyAndAddCount()
    {
        if(IsWeaponWithLimit == false)
        {
            return true;
        }
        if(DestroyedThisWeapon < MaxDestroyed)
        {
            DestroyedThisWeapon++;
            return true;
        }
        return false;
    }
    public static void ResetCurrentDestroyed()
    {
        DestroyedThisWeapon = 0;
    }
    public static bool IsDestroyedMaximum()
    {
        if (DestroyedThisWeapon >= MaxDestroyed)
        {
            return true;
        }
        return false;
    }
}
