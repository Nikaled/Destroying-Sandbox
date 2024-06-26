using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private readonly string AnalyticsFirstPlay = "MainMenuFirstLoad";
    private readonly string AnalyticsAnyModePressed = "AnyModeMenuButtonPressed";
    private readonly string AnalyticsBuildModePressed = "BuildModeMenuButtonPressed";
    private readonly string AnalyticsDestroyingModePressed = "DestroyingModeMenuButtonPressed";
    private readonly string AnalyticsParkourModePressed = "ParkourModeMenuButtonPressed";
    private void Start()
    {
        Geekplay.Instance.ShowInterstitialAd();
        if (Geekplay.Instance.PlayerData.IsFirstPlay)
        {
            Analytics.instance.SendEvent(AnalyticsFirstPlay);
            Geekplay.Instance.PlayerData.IsFirstPlay = false;
            Geekplay.Instance.Save();
        }


        //Geekplay.Instance.PlayerData = new PlayerData();
        //Geekplay.Instance.Save();
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
}
