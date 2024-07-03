using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHpSystem : MonoBehaviour
{
    [SerializeField] UnitMovement unitMovement;
    [SerializeField] DestroySystem destroySystem;
    private int HitPoints = 5;
    public bool TryGetPunch(GameObject Puncher)
    {
        if(unitMovement.AwaitForGroundAfterPunch == false)
        {
            unitMovement.GetPunch(Puncher);
            GetDamage();
            return true;
        }
        return false;
    }
    private void GetDamage()
    {
        HitPoints--;
        if(HitPoints <=0 )
        {
            destroySystem.DamageTaked(Vector3.zero);
        }
    }
}
