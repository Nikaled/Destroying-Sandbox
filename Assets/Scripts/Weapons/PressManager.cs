using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PressManager : MonoBehaviour
{
    [SerializeField] Press PressPrefab;
    private Image Cross;
    Vector3 CrosshairWorldPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    public static Action StateSwitched;
    public static PressManager instance;
    Press currentPress;
    private void Awake()
    {
        instance = this;
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
        if(currentPress != null)
        {
        Destroy(currentPress.gameObject);
        }
    }
    public void TryFire()
    {
        if(currentPress != null)
        {
            Debug.Log("Current Press is Existing");
            return;
        }

        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000, aimColliderLayerMask))
        {
            DestroyLimiter.ResetCurrentDestroyed();
            CrosshairWorldPosition = raycastHit.point;
            Vector3 PressUpPosition = new Vector3(0, 100, 0);
            currentPress = Instantiate(PressPrefab, CrosshairWorldPosition + PressUpPosition, Quaternion.identity);
        }
    }

    void Start()
    {
        Cross = CanvasManager.instance.Crosshair;
    }

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
