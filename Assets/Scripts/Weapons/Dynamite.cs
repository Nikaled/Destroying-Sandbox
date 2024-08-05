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
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    [SerializeField] DamageArea DamageSphere;
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
        DamageSphere.GetComponent<SphereCollider>().enabled = true;
    

        yield return new WaitForSeconds(0.05f);
        DestroyAnimation.transform.parent = null;
        DestroyAnimation.ShowEffectAndDestroyAfterDelay();
        for (int i = 0; i < DamageSphere.targetsInExplosion.Count; i++)
        {
            if (DamageSphere.targetsInExplosion[i] != null)
                DamageSphere.targetsInExplosion[i].TakeDamage(transform.position);
        }
        explosionForceChecker.transform.parent = null;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        //Time.timeScale = 0;
        //Debug.Break();
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