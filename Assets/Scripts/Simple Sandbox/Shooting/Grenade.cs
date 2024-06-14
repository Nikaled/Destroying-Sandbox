using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grenade : MonoBehaviour
{
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion;
    [SerializeField] GameObject DestroyAnimation;
    [SerializeField] int GrenadeDamage = 10;
    [SerializeField] int ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    public void OnLaunch()
    {
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(DelayBeforeExplosion);
        Source.PlayExplosionSound();
        GetComponent<SphereCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;

        yield return new WaitForSeconds(0.1f);
        var explosion = Instantiate(DestroyAnimation, transform.position, DestroyAnimation.transform.rotation);
        explosion.transform.DOScale(new Vector3(ExplosionScale, ExplosionScale, ExplosionScale), 0);
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] !=null)
            targetsInExplosion[i].TakeDamage(transform.position);
        }
        Destroy(explosionForceChecker);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DestroyCollision>() != null)
        {
            targetsInExplosion.Add(other.GetComponent<DestroyCollision>());
        }
    }
}
