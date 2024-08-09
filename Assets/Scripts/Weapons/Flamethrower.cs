using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flamethrower : MonoBehaviour
{
    [SerializeField] GameObject FireThrowAnimation;
    [SerializeField] BoxCollider HitCollider;
    private IEnumerator WaitForActiveFire;
    public bool IsFiring;
    public static Flamethrower instance;
    [SerializeField] Transform FlameRotator;

    private void Awake()
    {
        instance = this;
    }
    public void StartFire()
    {
        Debug.Log("Fire Flame");
        IsFiring = true;
        if (WaitForActiveFire != null)
        {
            StopCoroutine(WaitForActiveFire);
        }
        WaitForActiveFire = FireActiveDelay();
        StartCoroutine(WaitForActiveFire);

        FireThrowAnimation.SetActive(true);
    }
    private void Update()
    {
        if(Player.instance.CurrentWeapon == Player.WeaponType.FlameThrower && Player.instance.currentState == Player.PlayerState.Idle)
        {
            if (IsFiring)
            {
                Player.instance.FireFlameThrower();
                Vector3 dir = PlayerShooting.instance.CrosshairWorldPosition;
                FlameRotator.LookAt(dir);
                FireThrowAnimation.gameObject.transform.LookAt(dir);
                //PlayerShooting.instance.FireFlameThrower();
            }
        }
    }
    private void ActivateFire()
    {    
        HitCollider.enabled = true;
        NotFlamingUI.instance.FiringObjects.Clear();
    }
    public void EndFire()
    {
        if (WaitForActiveFire != null)
        {
            StopCoroutine(WaitForActiveFire);
        }
        IsFiring = false;
        FireThrowAnimation.SetActive(false);
        HitCollider.enabled = false;
    }
    private IEnumerator FireActiveDelay()
    {
        yield return new WaitForSeconds(0.4f);
        ActivateFire();
    }
}
