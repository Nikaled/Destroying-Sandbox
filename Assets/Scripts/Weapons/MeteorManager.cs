using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MeteorManager : MonoBehaviour
{
    [SerializeField] Meteor MeteorPrefab;
    private Image Cross;
    private float CurrrentTime;
    public float SecondsBetweenHits;
    private float CurrentReloadTime;
    Vector3 CrosshairWorldPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    public static Action StateSwitched;

    public void TryFire()
    {
        //CurrentReloadTime = 0; // ������� ��� �����������
        CurrrentTime = Time.time;
        if (CurrrentTime - CurrentReloadTime > SecondsBetweenHits)
        {
            CrosshairWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
            {
                CurrentReloadTime = Time.time;
                CrosshairWorldPosition = raycastHit.point;
                Vector3 MeteorUpPosition = new Vector3(20, 50, 20);
                var Meteor = Instantiate(MeteorPrefab, CrosshairWorldPosition+ MeteorUpPosition, Quaternion.identity);
                Meteor.Fire(CrosshairWorldPosition);
            }
        }
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
