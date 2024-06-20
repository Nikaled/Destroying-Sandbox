using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    public Rigidbody carRb;
    float CarRbVelocityToKill = 100;
    private void OnTriggerEnter(Collider other)
    {
        var DestrCol = other.GetComponent<DestroyCollision>();
        if (DestrCol != null)
        {
            DestrCol.TakeDamage(transform.position);
        }
        //if (carRb.velocity.sqrMagnitude > CarRbVelocityToKill)
        //{
           
        //}
       
    }
}
