using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField] public TextMeshProUGUI CoinsText;
    [SerializeField] public Image Crosshair;
    [SerializeField] private GameObject BuildingMenu;
    [SerializeField] private GameObject InAppShop;
    [SerializeField] public GameObject SaveMapUI;
    [SerializeField] public GameObject CoinsUI;
    [SerializeField] public GameObject MultiplatformUI;
    [SerializeField] public GameObject WeaponSlotsUI;
    [Header("PC Interfaces")]
    [SerializeField] GameObject _helicopterInstruction;
    [SerializeField] GameObject _planeInstruction;
    [SerializeField] GameObject _objectInteractionInstruction;
    [SerializeField] GameObject _ControlCarInstruction;

    [SerializeField] GameObject _EnterTransportInstruction;
    [SerializeField] GameObject _EnterCitizenInstruction;

    [SerializeField] GameObject _rotatingModeInstruction;
    [SerializeField] GameObject _idleInstruction;
    [SerializeField] GameObject _buildingModeInstruction;
    [SerializeField] GameObject _deletingModeInstruction;
    [SerializeField] GameObject CanvasPCInterface;
    [SerializeField] GameObject CarShootingText;
    [Header("Mobile Interfaces")]
    [SerializeField] GameObject CanvasMobileInterface;
    [SerializeField] GameObject LeftButtonsZone;
    [SerializeField] GameObject RightButtonsZone;
    [SerializeField] GameObject HelicopterMobileInstruction;
    [SerializeField] GameObject PlaneMobileInstruction;
    [SerializeField] GameObject CarMobileInstruction;
    [SerializeField] GameObject AppShopButton;
    [SerializeField] GameObject UpLeftButtons;
    [SerializeField] public Button DoButton;
    [SerializeField] public Button InteracteButton;
    [SerializeField] Button BuildingButton;
    [SerializeField] Image BuildingButtonImage;
    [SerializeField] Button DeletingButton;
    [SerializeField] Image DeletingButtonImage;
    [SerializeField] Button RotatingButton;
    [SerializeField] Image RotatingButtonImage;
    [SerializeField] Image[] InteracteSymbolInButton;
    [SerializeField] Image DoButtonImageInIdle;
    [SerializeField] Image DoButtonImageInMode;
    [Header("Rotating Mode")]
    [SerializeField] GameObject _rotatingChosenObjectModeInstruction;
    [SerializeField] Slider[] RotatingModeSlidersScale;
    [SerializeField] Slider[] RotatingModeSlidersRotation;


    [Header("DestroyingSandbox")]
    [SerializeField] GameObject BlockSlots;
    [SerializeField] GameObject WeaponSlots;
    [SerializeField] GameObject CurrentDestroyBarUI;
    [SerializeField] GameObject CurrentDestroyBarFilledImage;
    [SerializeField] GameObject OnWinMapUI;
    [SerializeField] Image[] PhaseButtonImages;
    [SerializeField] public Button WeaponSpecialInteracteButton;
    [SerializeField] public Button ChangePhaseButton;
    [SerializeField] public GameObject UnlockWeaponUI;
    [Header("Parkour")]
    [SerializeField] GameObject ParkourUI;
    [SerializeField] GameObject OnWinParkourMapUI;
    [SerializeField] TextMeshProUGUI CurrentDestroyedText;
    private bool InAppShopActive;
    private bool SaveMapUIActive;
    [Header("Unlock cursor Windows")]
    [SerializeField] private List<GameObject> UnlockCursorWindows;

    private IEnumerator cursorLocker;
    private readonly string LoadedInGameplay = "LoadedInGameplay";

    #region DestroyingSandbox
    public void ChangePhaseButtonIcon(int index)
    {
        for (int i = 0; i < PhaseButtonImages.Length; i++)
        {
            PhaseButtonImages[i].gameObject.SetActive(false);
        }
        PhaseButtonImages[index].gameObject.SetActive(true);
    }
    public void ShowUnlockWeaponSlotUI(bool Is)
    {
        UnlockWeaponUI.SetActive(Is);
        CheckActiveUnlockCursorWindows();
    }
    public void SwitchPlayerWeapon()
    {
        Player.instance.SwitchWeapon(Player.instance.CurrentWeaponIndex);
    }
    public void ShowWinParkourUI(bool Is)
    {
        OnWinParkourMapUI.SetActive(Is);
        CheckActiveUnlockCursorWindows();
    }
    private void DestroyCountChanged(int CurrentDestroyed)
    {
        CurrentDestroyedText.text = $"{CurrentDestroyed} / {DestroyCounter.instance.DestroyedMax}";
        if (DestroyCounter.instance.DestroyedMax > 0)
            CurrentDestroyBarFilledImage.transform.DOScaleX((float)CurrentDestroyed / DestroyCounter.instance.DestroyedMax, 0);
    }
    public void OnWinMap()
    {
        ShowWinMapUI(true);
    }
    public void ShowWinMapUI(bool Is)
    {
        OnWinMapUI.SetActive(Is);
        CheckActiveUnlockCursorWindows();
    }
    public void ShowCurrentDestroyInterface(bool Is)
    {
        CurrentDestroyBarUI.SetActive(Is);
    }
    public void ShowBlockSlotsAndHideWeapons(bool Is)
    {
        BlockSlots.SetActive(Is);
        if (Is)
        {
            WeaponSlots.SetActive(false);
            //ShowWeaponSlotsAndHideBlocks(false);
        }
    }
    public void ShowWeaponSlotsAndHideBlocks(bool Is)
    {
        WeaponSlots.SetActive(Is);
        if (Is)
        {
            BlockSlots.SetActive(false);
        }
    }
    private void OnWinParkourMap()
    {
        ShowWinParkourUI(true);
    }
    #endregion
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Player.instance == null)
        {
            return;
        }
        if (Player.instance.AdWarningActive)
        {
            return;
        }
        if ( Player.instance.currentState == Player.PlayerState.Building)
        {
            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    ShowInAppShop(!InAppShopActive);
            //}
            if (Input.GetKeyDown(KeyCode.K))
            {
                ShowSaveMapUI(!SaveMapUIActive);
            }
        }
        if(Player.instance.InterfaceActive == false)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Cursor.lockState = CursorLockMode.None; 
                SceneManager.LoadScene(0);
            }
        }
    }
    private void Start()
    {
        ChangeCoinsText(Geekplay.Instance.PlayerData.Coins);
        DestroyCounter.instance.DestroyBlockCountChanged += DestroyCountChanged;
        DestroyCounter.instance.AllBlockDestroyed += OnWinMap;
        if (Geekplay.Instance.mobile)
        {
            CanvasMobileInterface.SetActive(true);
            CanvasPCInterface.SetActive(false);
            InteracteButton.gameObject.SetActive(true);
            ShowMobileIdleButtons(true);
        }
        else
        {
            CanvasMobileInterface.SetActive(false);
            CanvasPCInterface.SetActive(true);
            AppShopButton.SetActive(false);
        }
        Geekplay.Instance.PlayerData.CoinsChanged += ChangeCoinsText;
        Geekplay.Instance.LockCursorAfterAd += CheckActiveUnlockCursorWindows;

        if (SerializeBlockManager.instance.OnlyParkourMap)
        {
            ParkourUI.SetActive(true);
            ParkourWinZone.instance.WinParkour += OnWinParkourMap;
        }
        else
        {
            ParkourUI.SetActive(false);
        }

    }
    #region Mobile
    public void ChangeDoButtonImageToMode(bool Mode)
    {
        if (Mode)
        {
            DoButtonImageInIdle.gameObject.SetActive(false);
            DoButtonImageInMode.gameObject.SetActive(true);
        }
        else
        {
            DoButtonImageInIdle.gameObject.SetActive(true);
            DoButtonImageInMode.gameObject.SetActive(false);
        }
    }
    public void TurnYellowBuildingButton(bool Is)
    {
        if (Is)
        {
            BuildingButton.image.color = Color.yellow;
            BuildingButtonImage.color = Color.yellow;
        }
        else
        {
            BuildingButton.image.color = Color.white;
            BuildingButtonImage.color = Color.white;
        }
    }
    public void TurnYellowDeletingButton(bool Is)
    {
        if (Is)
        {
            DeletingButton.image.color = Color.yellow;
            DeletingButtonImage.color = Color.yellow;
        }
        else
        {
            DeletingButton.image.color = Color.white;
            DeletingButtonImage.color = Color.white;
        }
    }
    public void TurnYellowRotatingButton(bool Is)
    {
        if (Is)
        {
            RotatingButton.image.color = Color.yellow;
            RotatingButtonImage.color = Color.yellow;
        }
        else
        {
            RotatingButton.image.color = Color.white;
            RotatingButtonImage.color = Color.white;
        }
    }
    private void ShowMobileIdleButtons(bool Is)
    {
        LeftButtonsZone.SetActive(Is);
        RightButtonsZone.SetActive(Is);
    }
    private void ShowWeaponSlotsUI(bool Is)
    {
        WeaponSlotsUI.SetActive(Is);
    }

    public void ShowCurrentInteracteButton(int ButtonIndex)
    {
        for (int i = 0; i < InteracteSymbolInButton.Length; i++)
        {
            InteracteSymbolInButton[i].gameObject.SetActive(false);
        }
        InteracteSymbolInButton[ButtonIndex].gameObject.SetActive(true);

    }
    public void ShowAllUI(bool Is)
    {
        if (Geekplay.Instance.mobile)
        {
            CanvasMobileInterface.SetActive(Is);
        }
        else
        {
            CanvasPCInterface.SetActive(Is);
        }
        MultiplatformUI.SetActive(Is);
    }
    public void ShowSaveMapUI(bool Is)
    {
        SaveMapUIActive = Is;
        SaveMapUI.SetActive(Is);
        if (Is)
        {
            Player.instance.examplePlayer.LockCursor(false);
        }
        else
        {
            CheckActiveUnlockCursorWindows();
        }
    }
    public void ShowHelicopterMobileInstruction(bool Is)
    {
        HelicopterMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    public void ShowCarMobileInstruction(bool Is)
    {
        CarMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    public void ShowPlaneMobileInstruction(bool Is)
    {
        PlaneMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    #endregion
    private void OnDisable()
    {
        Geekplay.Instance.PlayerData.CoinsChanged -= ChangeCoinsText;
        Geekplay.Instance.LockCursorAfterAd -= CheckActiveUnlockCursorWindows;
    }
    public void CheckActiveUnlockCursorWindows()
    {
        if(cursorLocker != null)
        {
            StopCoroutine(cursorLocker);
        }
        cursorLocker = DelayDeactivateInterface(false);
        StartCoroutine(cursorLocker);

        if (Geekplay.Instance.mobile == true)
        {
            Cursor.lockState = CursorLockMode.None;

            return;
        }
        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < UnlockCursorWindows.Count; i++)
        {
            if (UnlockCursorWindows[i].activeInHierarchy == true)
            {
                Cursor.lockState = CursorLockMode.None;
                if (cursorLocker != null)
                {
                    StopCoroutine(cursorLocker);
                }
                cursorLocker = DelayDeactivateInterface(true);
                StartCoroutine(cursorLocker);
            }
        }
    }
    private IEnumerator DelayDeactivateInterface(bool Is)
    {
        yield return new WaitForSeconds(0.1f);
        Player.instance.InterfaceActive = Is;
        if (Is)
        {
            Player.instance.examplePlayer.LockCursor(false);
            Debug.Log("Lock cursor false");
        }
    }
    private void ChangeCoinsText(int NewValue)
    {
        CoinsText.text = NewValue.ToString();
    }
    public void ShowHelicopterInstruction(bool Is)
    {
        _helicopterInstruction.SetActive(Is);
    }
    public void ShowTransportEnterInstruction(bool Is)
    {
        _EnterTransportInstruction.SetActive(Is);
        if (Is)
        {
            ShowObjectInteructInstruction(false);
            ShowCitizenEnterInstruction(false);
        }
    }
    public void ShowControlCarInstruction(bool Is, bool IsTank)
    {
        _ControlCarInstruction.SetActive(Is);
        CarShootingText.SetActive(IsTank);
    }

    public void ShowInAppShop(bool Is)
    {
        InAppShopActive = Is;
        InAppShop.SetActive(Is);
        if (Is)
        {
            Player.instance.examplePlayer.LockCursor(false);
        }
        else
        {
            CheckActiveUnlockCursorWindows();
        }
    }
    public void ShowCitizenEnterInstruction(bool Is)
    {
        _EnterCitizenInstruction.SetActive(Is);
        if (Is)
        {
            ShowObjectInteructInstruction(false);
            ShowTransportEnterInstruction(false);
        }
    }
    public void ShowPlaneInstruction(bool Is)
    {
        _planeInstruction.SetActive(Is);
    }
    public void ShowBuildingMenu(bool Is)
    {
        BuildingMenu.SetActive(Is);
        CheckActiveUnlockCursorWindows();
        if (Is)
        {
            Player.instance.SwitchPlayerState(Player.PlayerState.InBuildingMenu);
        }
        else
        {
            Player.instance.SwitchPlayerState(Player.PlayerState.Building);
        }

    }
    public void ShowObjectInteructInstruction(bool Is)
    {
        _objectInteractionInstruction.SetActive(Is);
        if (Is)
        {
            ShowTransportEnterInstruction(false);
            ShowCitizenEnterInstruction(false);
        }

    }
    public void ShowRotatingModeInstruction(bool Is)
    {
        _rotatingModeInstruction.SetActive(Is);
    }
    public void ShowDeletingModeInstruction(bool Is)
    {
        _deletingModeInstruction.SetActive(Is);
        ShowCitizenEnterInstruction(false);
        ShowTransportEnterInstruction(false);
    }
    public void ShowBuildingModeInstruction(bool Is)
    {
        _buildingModeInstruction.SetActive(Is);
    }
    public void ShowIdleInstruction(bool Is)
    {
        _idleInstruction.SetActive(Is);
        ShowWeaponSlotsUI(Is);
    }
}
