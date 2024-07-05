using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Meteor : MonoBehaviour
{
    [SerializeField] DestroyEffect MeteorAnimation;
    [SerializeField] GameObject ParentObject;
    [SerializeField] DamageArea DestroyArea;
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion = 0.6f;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    [SerializeField] Rigidbody MeteorRb;
    [SerializeField] AudioSource FlyingSoundSource;
    Vector3 CurrentDestination;


    public void SubscribeOnSwitchState()
    {
        MeteorManager.StateSwitched += DestroyingOnSwitch;
    }
    private void DestroyingOnSwitch()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        MeteorManager.StateSwitched -= DestroyingOnSwitch;
    }
    private void Update()
    {
        transform.LookAt(CurrentDestination);
    }
    public void Fire(Vector3 Destination)
    {
        CurrentDestination = Destination;
        transform.LookAt(Destination);
        //transform.position += new Vector3(100, 50, 100);
        MeteorRb.DOMove(Destination, 2).SetEase(Ease.InExpo).OnComplete(StartExplosion);
        //StartCoroutine(Explosion());
    }
    private void StartExplosion()
    {
        StartCoroutine(Explosion());
}
    private IEnumerator Explosion()
    {
        DestroyArea.GetComponent<SphereCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        yield return new WaitForSeconds(DelayBeforeExplosion);
        //Source.PlayExplosionSound();
        MeteorAnimation.enabled = true;
        MeteorAnimation.ShowEffectAndDestroyAfterDelay();

        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        targetsInExplosion = DestroyArea.targetsInExplosion;
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
            {
                targetsInExplosion[i].TakeDamage(targetsInExplosion[i].transform.position + new Vector3(0, 2, 0));
            }
        }
        SoundManager.instance.PlayMeteorCrushedSound();
        yield return new WaitForSeconds(0.1f);
        FlyingSoundSource.Pause();
        Destroy(explosionForceChecker.gameObject);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        var DestrCol = other.GetComponent<DestroyCollision>();
        if (DestrCol != null)
        {
            DestrCol.TakeDamage(transform.position);
        }
    }
}
