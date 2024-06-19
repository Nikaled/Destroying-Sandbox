using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dynamite : MonoBehaviour
{
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion;
    [SerializeField] DestroyEffect DestroyAnimation;
    [SerializeField] int ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;

    public void SubscribeOnExplosion()
    {
        DynamiteManager.ExplodeDynamite += OnLaunch;
    }
    public void SubscribeOnSwitchState()
    {
        DynamiteManager.StateSwitched += OnSwitchState;
    }
    private void OnSwitchState()
    {
        Destroy(gameObject);
    }
    public void OnLaunch()
    {
        StartCoroutine(Explosion());
       var rb =  gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    private void OnDestroy()
    {
        DynamiteManager.StateSwitched -= OnSwitchState;
        DynamiteManager.ExplodeDynamite -= OnLaunch;
    }
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(DelayBeforeExplosion);
        //Source.PlayExplosionSound();
        GetComponent<SphereCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;

        yield return new WaitForSeconds(0.1f);
        DestroyAnimation.transform.parent = null;
        DestroyAnimation.ShowEffectAndDestroyAfterDelay();
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
                targetsInExplosion[i].TakeDamage(transform.position);
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
}