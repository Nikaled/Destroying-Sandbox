using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForceChecker : MonoBehaviour
{
    [SerializeField] SphereCollider SphereExplosionRange;
    [SerializeField] float VecolityModifier = 400f;
    [SerializeField] bool HasFireEffect;
    private float partOfSphereRange;
    private void OnTriggerEnter(Collider other)
    {
        var DestroyCol = other.GetComponent<DestroyCollision>();
        if (DestroyCol != null)
        {
            //  Debug.Log("ExplosionForced:" + other.gameObject);
            DestroyCol.TakeExplosion(CalculateForce(other.gameObject));
            if (HasFireEffect)
            {
                if (partOfSphereRange < 0.8f)
                {
                    DestroyCol.TakeFire();
                }
            }

        }
    }
    public Vector3 CalculateForce(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        float SphereRange = SphereExplosionRange.radius; 
        float ForceModifier = SphereRange / distance;
        partOfSphereRange = distance / SphereRange;
        //ForceModifier /= 1.2f;
        Vector3 Direction = (target.transform.position - transform.position).normalized;
        Vector3 ForceVelocity = Direction * ForceModifier;
        //Debug.Log("ForceVelocity:" + ForceVelocity.sqrMagnitude);
        return ForceVelocity* VecolityModifier;

    }
}
