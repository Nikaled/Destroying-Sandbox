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
        CurrentReloadTime = 0; // �������
        CurrrentTime = Time.time;
        if (CurrrentTime - CurrentReloadTime > SecondsBetweenHits)
        {      
            CrosshairWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
            {
                DestroyLimiter.ResetCurrentDestroyed();
                CurrentReloadTime = Time.time;
                CrosshairWorldPosition = raycastHit.point;
                var lightning = Instantiate(LightningPrefab, CrosshairWorldPosition, Quaternion.identity);
                lightning.Fire();
                lightning.SubscribeOnSwitchState();
                SoundManager.instance.PlayLightningSound();
            }
        }
    }
    private void OnEnable()
    {
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { TryFire(); });
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
        if(Geekplay.Instance.mobile == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryFire();
            }
        }
    }
}
