using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject LightningAnimation;
    [SerializeField] GameObject ParentObject;
    [SerializeField] DamageArea DestroyArea;
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion=0.6f;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    public void Fire()
    {       
            StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        DestroyArea.GetComponent<CapsuleCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        yield return new WaitForSeconds(DelayBeforeExplosion);
        LightningAnimation.SetActive(true);
        //Source.PlayExplosionSound();

        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        targetsInExplosion = DestroyArea.targetsInExplosion;
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
            {
                targetsInExplosion[i].TakeDamage(targetsInExplosion[i].transform.position + new Vector3(0,2,0));
            }
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(explosionForceChecker.gameObject);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
