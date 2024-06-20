using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] float DelayBeforeDeleting;
    [SerializeField] GameObject Effect;
    [SerializeField] float EffectScale = 1;
   
    public void ShowEffectAndDestroyAfterDelay()
    {
        transform.parent = null;
        var  Eff = Instantiate(Effect, transform.position, transform.rotation);
        Eff.transform.localScale *= EffectScale;
        Eff.transform.parent = gameObject.transform;
        StartCoroutine(WaitForDestroy());
    }
    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(DelayBeforeDeleting);
        Destroy(gameObject);
    }
}
