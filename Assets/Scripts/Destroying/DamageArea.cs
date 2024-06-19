using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
   [HideInInspector] public List<DestroyCollision> targetsInExplosion = new();

    private void OnTriggerEnter(Collider other)
    {
        var DestrCol = other.GetComponent<DestroyCollision>();
        if (DestrCol != null)
        {
            targetsInExplosion.Add(DestrCol);
        }
    }
}
