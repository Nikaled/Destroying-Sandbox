using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<DestroyCollision>() != null)
        {
            other.GetComponent<DestroyCollision>().TakeFire();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<DestroyCollision>() != null)
        {
            collision.gameObject.GetComponent<DestroyCollision>().TakeFire();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<DestroyCollision>() != null)
        {
            collision.gameObject.GetComponent<DestroyCollision>().TakeFire();
        }
    }
}
