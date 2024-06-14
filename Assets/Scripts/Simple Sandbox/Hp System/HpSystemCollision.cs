using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystemCollision : MonoBehaviour
{
    [SerializeField] HpSystem hpSystem;

    public void TakeDamage(int DamageCount)
    {
        if(hpSystem !=null)
        hpSystem.TakeDamage(DamageCount);
    }
}
