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
    [SerializeField] GameObject CanvasPCInterface;
    [Header("Mobile Interfaces")]
    [SerializeField] GameObject CanvasMobileInterface;
    [SerializeField] GameObject LeftButtonsZone;
    [SerializeField] GameObject RightButtonsZone;
    [SerializeField] GameObject CarMobileInstruction;
    [SerializeField] GameObject AppShopButton;
    [SerializeField] GameObject UpLeftButtons;
    [SerializeField] public Button DoButton;
    [SerializeField] public Button InteracteButton;
    [SerializeField] public Button SaveButton;
    [SerializeField] Image[] InteracteSymbolInButton;
    [SerializeField] Image DoButtonImageInIdle;
    [SerializeField] Image DoButtonImageInMode;


    [Header("DestroyingSandbox")]
    [SerializeField] GameObject BuildingInstruction;
    [SerializeField] GameObject BlockSlots;
    [SerializeField] GameObject BuildingMenuButton;
    [SerializeField] GameObject WeaponSlots;
    [SerializeField] GameObject CurrentDestroyBarUI;
    [SerializeField] GameObject CurrentDestroyBarFilledImage;
    [SerializeField] GameObject OnWinMapUI;
    [SerializeField] GameObject[] PhaseButtonImages;
    [SerializeField] public Button WeaponSpecialInteracteButton;
    [SerializeField] public Button ChangePhaseButton;
    [SerializeField] public GameObject UnlockWeaponUI;
    [SerializeField] GameObject[] WeaponInstructions;
    [Header("Parkour")]
    [SerializeField] GameObject ParkourUI;
    [SerializeField] GameObject OnWinParkourMapUI;
    [SerializeField] TextMeshProUGUI CurrentDestroyedText;
    [SerializeField] GameObject RewardUI;
    [SerializeField] Button LoadNextLevelButton;
    [SerializeField] TextMeshProUGUI RewardText;
    [SerializeField] Button ReloadButtonOnWinPanel;
    private bool InAppShopActive;
    private bool SaveMapUIActive;
    private bool BuildingMenuActive;
    [Header("Unlock cursor Windows")]
    [SerializeField] private List<GameObject> UnlockCursorWindows;

    private IEnumerator cursorLocker;
    private readonly string FirstLoadedInGameplay = "FirstTimeLoadedInGameplay";
    private readonly string InventoryOpened = "InventoryOpened";

    #region DestroyingSandbox
    public void TryShowNextLevelButton()
    {
        if((SerializeBlockManager.instance.OnlyDestroyingMap || SerializeBlockManager.instance.OnlyParkourMap) && SerializeBlockManager.instance.IsCurrentMapLast == false)
        {
            LoadNextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            LoadNextLevelButton.gameObject.SetActive(false);
        }
    }
    public void ShowRewardAndSetRewardText(bool Is, int Reward)
    {
        RewardUI.SetActive(Is);
        RewardText.text = Reward.ToString();
    }
    public void ShowBuildingInstruction(bool Is)
    {
        BuildingInstruction.SetActive(Is);
    }
    public void ShowCurrentWeaponInstruction(int WeaponIndex, bool HideAll = false)
    {
        for (int i = 0; i < WeaponInstructions.Length; i++)
        {
            WeaponInstructions[i].SetActive(false);
        }
        if(HideAll == false)
        {
            WeaponInstructions[WeaponIndex].SetActive(true);
        }
    }
    public void ChangePhaseButtonIcon(int index)
    {
        for (int i = 0; i < PhaseButtonImages.Length; i++)
        {
            PhaseButtonImages[i].SetActive(false);
        }
        PhaseButtonImages[index].SetActive(true);
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
        ShowWinMapUI(Is);
        if (Is) // On Restart IsInterface controlled by ParkourManager
        {
        CheckActiveUnlockCursorWindows();
        }
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
        if (Is)
        {
        TryShowNextLevelButton();
        }
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
            BuildingMenuButton.SetActive(true);
            SaveButton.gameObject.SetActive(true);
            //ShowWeaponSlotsAndHideBlocks(false);
        }
    }
    public void ShowWeaponSlotsAndHideBlocks(bool Is)
    {
        WeaponSlots.SetActive(Is);
        if (Is)
        {
            BlockSlots.SetActive(false);
            BuildingMenuButton.SetActive(false);
            SaveButton.gameObject.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Cursor.lockState = CursorLockMode.None; 
                SceneManager.LoadScene(0);
            }
        }
    }
    private void Start()
    {
        Player.instance.examplePlayer.LockCursor(true);
        Geekplay.Instance.ShowInterstitialAd();
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
            ReloadButtonOnWinPanel.onClick.RemoveAllListeners();
            ReloadButtonOnWinPanel.onClick.AddListener(delegate { CycleManager.instance.ActivateParkourPhase(); });
        }
        else
        {
            ParkourUI.SetActive(false);
            ReloadButtonOnWinPanel.onClick.AddListener(delegate { CycleManager.instance.ActivateBuildingPhase(); });
        }
        if (Geekplay.Instance.PlayerData.IsFirstGameplay == false)
        {
            Geekplay.Instance.PlayerData.IsFirstGameplay = true;
            Analytics.instance.SendEvent(FirstLoadedInGameplay);
            Geekplay.Instance.Save();
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
    public void ShowCarMobileInstruction(bool Is)
    {
        CarMobileInstruction.SetActive(Is);
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
        Player.instance.examplePlayer.LockCursor(true);

        for (int i = 0; i < UnlockCursorWindows.Count; i++)
        {
            if (UnlockCursorWindows[i].activeInHierarchy == true)
            {
                //Cursor.lockState = CursorLockMode.None;
                Player.instance.examplePlayer.LockCursor(false);
                if (cursorLocker != null)
                {
                    StopCoroutine(cursorLocker);
                }
                cursorLocker = DelayDeactivateInterface(true);
                Player.instance.InterfaceActive = true;
                //StartCoroutine(cursorLocker);
                break;
            }
        }
    }
    private IEnumerator DelayDeactivateInterface(bool Is)
    {
        //yield return new WaitForSeconds(0.1f);
        yield return null;
        Player.instance.InterfaceActive = Is;
        //if (Is)
        //{
        //    Player.instance.examplePlayer.LockCursor(false);
        //    Debug.Log("Lock cursor false");
        //}
    }
    private void ChangeCoinsText(int NewValue)
    {
        CoinsText.text = NewValue.ToString();
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
    public void BuildingMenuButtonFunc()
    {
        BuildingMenuActive = !BuildingMenuActive;
        ShowBuildingMenu(BuildingMenuActive);
    }
    public void ShowBuildingMenu(bool Is)
    {
        BuildingMenu.SetActive(Is);
        BuildingMenuActive = Is;
        CheckActiveUnlockCursorWindows();
        if (Is)
        {
            Player.instance.SwitchPlayerState(Player.PlayerState.InBuildingMenu);
            Analytics.instance.SendEvent(InventoryOpened);
        }
        else
        {
            Player.instance.SwitchPlayerState(Player.PlayerState.Building);
        }

    }
}
