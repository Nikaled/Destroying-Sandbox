using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    public DestroySystem destroySystem;

    public void TakeDamage(Vector3 projectilePosition)
    {
        if (destroySystem != null)
            destroySystem.DamageTaked(projectilePosition);
    }
    public void TakeExplosion(Vector3 Force)
    {
        if (destroySystem != null)
            destroySystem.ExplosionForce(Force);
    }
    public void TakeFire()
    {
        Debug.Log("TakedFire");
        if (destroySystem != null)
            destroySystem.FireObject();
    }
}
