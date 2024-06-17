using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] float DelayBeforeDeleting;
    [SerializeField] GameObject Effect;
    [SerializeField] float EffectScale = 1;
   
    public void ShowEffectAndDestroyAfterDelay()
    {
        var  Eff = Instantiate(Effect, transform.position, transform.rotation);
        Eff.transform.localScale *= EffectScale;

        StartCoroutine(WaitForDestroy());
    }
    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(DelayBeforeDeleting);
        Destroy(gameObject);
    }
}
