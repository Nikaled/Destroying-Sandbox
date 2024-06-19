using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Molotov : MonoBehaviour
{
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion;
    [SerializeField] DestroyEffect DestroyAnimation;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(DelayBeforeExplosion);
        Source.PlayExplosionSound();
        GetComponent<SphereCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        DestroyAnimation.transform.parent = null;
        DestroyAnimation.ShowEffectAndDestroyAfterDelay();
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
                targetsInExplosion[i].TakeFire();
        }
        Destroy(explosionForceChecker.gameObject);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DestroyCollision>() != null)
        {
            targetsInExplosion.Add(other.GetComponent<DestroyCollision>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        DelayBeforeExplosion = 0;
        StartCoroutine(Explosion());
        if (collision.gameObject.GetComponent<DestroyCollision>() != null)
        {
            collision.gameObject.GetComponent<DestroyCollision>().TakeDamage(transform.position);
           
        }
       
    }
}