using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    [SerializeField] GameObject FireThrowAnimation;
    [SerializeField] BoxCollider HitCollider;
    private IEnumerator WaitForActiveFire;
    public static Flamethrower instance;
    private void Awake()
    {
        instance = this;
    }
    public void StartFire()
    {
        Debug.Log("Fire Flame");

        if (WaitForActiveFire != null)
        {
            StopCoroutine(WaitForActiveFire);
        }
        WaitForActiveFire = FireActiveDelay();
        StartCoroutine(WaitForActiveFire);

        FireThrowAnimation.SetActive(true);
    }
    private void ActivateFire()
    {    
        HitCollider.enabled = true;
    }
    public void EndFire()
    {
        if (WaitForActiveFire != null)
        {
            StopCoroutine(WaitForActiveFire);
        }
        FireThrowAnimation.SetActive(false);
        HitCollider.enabled = false;
    }
    private IEnumerator FireActiveDelay()
    {
        yield return new WaitForSeconds(0.4f);
        ActivateFire();
    }
}
