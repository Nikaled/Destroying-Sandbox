using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ParkourMapCell : DestroyingMapCell
{
    private readonly string StartedParkourMap = "StartedParkourMapAtNumber_";
    public override void Awake()
    {
        base.Awake();
    }
    public void LoadMapLogic()
    {
        Geekplay.Instance.PlayerData.IsLoadingParkourMap = true;
        Geekplay.Instance.PlayerData.CurrentParkourMapName = MapNameForScripts;
        Geekplay.Instance.ShowInterstitialAd();
        string forEvent = StartedParkourMap + IndexOfMap;
        Geekplay.Instance.PlayerData.CurrentParkourMapIndex = IndexOfMap;
        Analytics.instance.SendEvent(forEvent);
        SceneManager.LoadScene(1);
    }
    public void LoadParkourMap()
    {
        ParkourRewardManager.instance.currentPressedCell = this;
        ParkourRewardManager.instance.TryShowRewardWindow();

    }
    public void SetTimeToSlot(float TimeInSeconds)
    {
        if (TimeInSeconds > 0)
        {
            int minutes = (int)TimeInSeconds / 60;
            int seconds = (int)TimeInSeconds - minutes * 60;
            float milliseconds = (TimeInSeconds - (int)TimeInSeconds) * 1000;
            MapReward.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            CoinsImage.enabled = false;
        }
    }
}
