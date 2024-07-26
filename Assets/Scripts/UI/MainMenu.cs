using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private readonly string AnalyticsFirstPlay = "MainMenuFirstLoad";
    private readonly string AnalyticsAnyModePressed = "AnyModeMenuButtonPressed";
    private readonly string AnalyticsBuildModePressed = "BuildModeMenuButtonPressed";
    private readonly string AnalyticsDestroyingModePressed = "DestroyingModeMenuButtonPressed";
    private readonly string AnalyticsParkourModePressed = "ParkourModeMenuButtonPressed";

    [SerializeField] GameObject[] Windows;
    [SerializeField] TextMeshProUGUI CoinsTextInPromo;
    [SerializeField] TextMeshProUGUI CoinsTextInOurGames;
    [SerializeField] GameObject TutorialWindow;
    private void Start()
    {
        Geekplay.Instance.ShowInterstitialAd();
        if (Geekplay.Instance.PlayerData.IsNotFirstPlay == false)
        {
            Analytics.instance.SendEvent(AnalyticsFirstPlay);
            Geekplay.Instance.PlayerData.IsNotFirstPlay = true;
            Geekplay.Instance.Save();
            TutorialWindow.SetActive(true);
        }
        Geekplay.Instance.PlayerData.CoinsChanged += SetCoinsInPromo;
        CoinsTextInPromo.text = Geekplay.Instance.PlayerData.Coins.ToString();
        CoinsTextInOurGames.text = Geekplay.Instance.PlayerData.Coins.ToString();

        
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Geekplay.Instance.PlayerData.Coins += 500;
            }
        }
    }
    private void OnDisable()
    {
        Geekplay.Instance.PlayerData.CoinsChanged -= SetCoinsInPromo;

    }
    private void SetCoinsInPromo(int NewCoins)
    {
        CoinsTextInPromo.text = NewCoins.ToString();
        CoinsTextInOurGames.text = Geekplay.Instance.PlayerData.Coins.ToString();
    }
    public void HideAllWindows() //On Buttons
    {
        for (int i = 0; i < Windows.Length; i++)
        {
            Windows[i].SetActive(false);
        }
    }
    public void ShowAdOnButtonClick()
    {
        Geekplay.Instance.ShowInterstitialAd();
    }
    public void A_PressedAnyModeButton()
    {
        Analytics.instance.SendEvent(AnalyticsAnyModePressed);
    }
    public void A_PressedBuildingModeButton()
    {
        Analytics.instance.SendEvent(AnalyticsBuildModePressed);
    }
    public void A_PressedDestroyingModeButton()
    {
        Analytics.instance.SendEvent(AnalyticsDestroyingModePressed);
    }
    public void A_PressedParkourModeButton()
    {
        Analytics.instance.SendEvent(AnalyticsParkourModePressed);
    }

    public void LoadMapFromSlot(int SlotIndex)
    {
        if (Geekplay.Instance.PlayerData.MapDataArray != null)
        {
            if (Geekplay.Instance.PlayerData.MapDataArray.Length > SlotIndex)
            {
                if (Geekplay.Instance.PlayerData.MapDataArray[SlotIndex] != null)
                {
                    if (string.IsNullOrEmpty(Geekplay.Instance.PlayerData.MapDataArray[SlotIndex].SaveDate) == false)
                    {
                        Geekplay.Instance.PlayerData.IsLoadingMapFromSlot = true;
                        Geekplay.Instance.PlayerData.MapSlotToLoad = SlotIndex;
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
    }
}
