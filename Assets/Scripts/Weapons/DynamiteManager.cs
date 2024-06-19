using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DynamiteManager : MonoBehaviour
{
    private Vector3 CrosshairWorldPosition;
    public Image Crosshair;
    public LayerMask AbleToBuildMask;
    private BuildCellSide currentCell;
    private Player player;
    public static DynamiteManager instance;
    [SerializeField] Dynamite DynamitePrefab;
    public static Action ExplodeDynamite;
    public static Action StateSwitched;
     KeyCode ExplodeButton = KeyCode.Y;
    private void Start()
    {
        player = Player.instance;
        instance = this;
        Crosshair = CanvasManager.instance.Crosshair;
        DestroyCounter.instance.AllBlockDestroyed += OnWeaponSwitched;
    }
    private void OnWeaponSwitched()
    {
        StateSwitched?.Invoke();
    }
    private void OnDisable()
    {
        OnWeaponSwitched();
    }
    private void Update()
    {
        if (Player.instance.InterfaceActive)
        {
            return;
        }
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 26, AbleToBuildMask))
        {

            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject.GetComponent<BuildCellSide>();
        }
        else
        {
            if (currentCell != null)
            {
                CrosshairWorldPosition = ray.GetPoint(19);
            }
            currentCell = null;
        }
        if (currentCell != null)
        {
            if (currentCell.GetComponent<BuildCellSide>() != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 pos = currentCell.GetPositionToPlace();
                    Dynamite newDynamite = Instantiate(DynamitePrefab, pos, Quaternion.identity);
                    newDynamite.SubscribeOnExplosion();
                    newDynamite.SubscribeOnSwitchState();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (currentCell.parentBlock.CompareTag("Undestructable") == false)
                    {
                        if (currentCell.transform.parent.GetComponent<Dynamite>() != null)
                        {
                            Destroy(currentCell.transform.parent.gameObject);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(ExplodeButton))
        {
            ExplodeDynamite?.Invoke();
        }
    }
}
