using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class LightningManager : MonoBehaviour
{
    [SerializeField] Lightning LightningPrefab;
    private Image Cross;
    private float CurrrentTime;
    public float SecondsBetweenHits;
    private float CurrentReloadTime;
    Vector3 CrosshairWorldPosition;
    public static Action StateSwitched;
    [SerializeField] private LayerMask aimColliderLayerMask;
    public void TryFire()
    {
        CurrentReloadTime = 0; // стереть
        CurrrentTime = Time.time;
        if (CurrrentTime - CurrentReloadTime > SecondsBetweenHits)
        {      
            CrosshairWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
            {
                CurrentReloadTime = Time.time;
                CrosshairWorldPosition = raycastHit.point;
                var lightning = Instantiate(LightningPrefab, CrosshairWorldPosition, Quaternion.identity);
                lightning.Fire();
                lightning.SubscribeOnSwitchState();
            }
        }
    }
    private void OnDisable()
    {
        StateSwitched?.Invoke();
    }
    void Start()
    {
        Cross = CanvasManager.instance.Crosshair;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.InterfaceActive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            TryFire();
        }
    }
}
