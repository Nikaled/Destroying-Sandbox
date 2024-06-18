using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
   [HideInInspector] public List<DestroyCollision> targetsInExplosion = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DestroyCollision>() != null)
        {
            targetsInExplosion.Add(other.GetComponent<DestroyCollision>());
        }
    }
}
