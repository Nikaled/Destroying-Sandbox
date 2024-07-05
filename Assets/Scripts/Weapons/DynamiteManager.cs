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
    [SerializeField] AvailablePlaceBlockChecker placeChecker;
    public static Action ExplodeDynamite;
    public static Action StateSwitched;
    KeyCode ExplodeButton = KeyCode.T;
    private void Start()
    {
        player = Player.instance;
        instance = this;
        Crosshair = CanvasManager.instance.Crosshair;
        DestroyCounter.instance.AllBlockDestroyed += OnWeaponSwitched;
    }
    private void OnEnable()
    {
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { PlaceDynamite(); });
            CanvasManager.instance.ChangeDoButtonImageToMode(true);
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.InteracteButton.gameObject.SetActive(true);
            CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { DeleteDynamite(); });
            CanvasManager.instance.WeaponSpecialInteracteButton.gameObject.SetActive(true);
            CanvasManager.instance.WeaponSpecialInteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.WeaponSpecialInteracteButton.onClick.AddListener(delegate { DoExplostion(); });
        }
    }
    private void OnWeaponSwitched()
    {
        StateSwitched?.Invoke();
    }
    private void OnDisable()
    {
        if(currentCell != null)
        {
            currentCell.ShowCellMesh(false);
        }
        OnWeaponSwitched();

        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ChangeDoButtonImageToMode(false);
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
            CanvasManager.instance.WeaponSpecialInteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.WeaponSpecialInteracteButton.gameObject.SetActive(false);
        }
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
            if (currentCell != null)
            {
                currentCell.ShowCellMesh(false);
            }
            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject.GetComponent<BuildCellSide>();
        }
        else
        {
            if (currentCell != null)
            {
                CrosshairWorldPosition = ray.GetPoint(19);
                currentCell.ShowCellMesh(false);
            }
            currentCell = null;
        }
        if (currentCell != null)
        {
            if (currentCell.GetComponent<BuildCellSide>() != null)
            {
                if (Geekplay.Instance.mobile == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceDynamite();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (currentCell.parentBlock.CompareTag("Undestructable") == false)
                        {
                            if (currentCell.transform.parent.GetComponent<Dynamite>() != null)
                            {
                                DeleteDynamite();
                            }
                        }
                    }
                }
            }

        }

        if (Input.GetKeyDown(ExplodeButton))
        {
            DoExplostion();
        }
    }
    private void PlaceDynamite()
    {
        if (currentCell != null)
        {
            StartCoroutine(waitPlaceCheckerCallback());

        }
        IEnumerator waitPlaceCheckerCallback()
        {
            Vector3 pos = currentCell.GetPositionToPlace();
            var Checker = Instantiate(placeChecker, pos, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
            if (Checker.PlayerInCell == false && Checker.BlockInCell == false)
            {
                PlaceLogic();
            }
            else
            {
                Debug.Log("Игрок или другой блок в клетке для установки!");
            }
            Destroy(Checker.gameObject);


            void PlaceLogic()
            {
                BuildCellManager.instance.PlayPlaceBlockSound();
                Dynamite newDynamite = Instantiate(DynamitePrefab, pos, Quaternion.identity);
                newDynamite.SubscribeOnExplosion();
                newDynamite.SubscribeOnSwitchState();
            }
        }
    }
    private void DeleteDynamite()
    {
        BuildCellManager.instance.PlayDeleteBlockSound();
        Destroy(currentCell.transform.parent.gameObject);
    }
    private void DoExplostion()
    {
        ExplodeDynamite?.Invoke();
        SoundManager.instance.PlayDynamiteSound();
    }
}
