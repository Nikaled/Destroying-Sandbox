using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ParkourManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float CurrentTime;
    private float StartTime;
    float minutes;
    float seconds;
    float milliseconds;
    public static ParkourManager instance;
    private bool WinMap;
    private readonly string EndedParkourMap = "EndedParkourMapAtNumber_";
    private void Start()
    {
        instance = this;
        if (SerializeBlockManager.instance.OnlyParkourMap)
        {
            StartParkour();
            ParkourWinZone.instance.WinParkour += OnWinParkour;
        }
    }
    public void StartParkour()
    {
        StartTime = Time.time;
        WinMap = false;
    }
    private void TrySerializeTimeValue()
    {
        string forAnalytics = EndedParkourMap + Geekplay.Instance.PlayerData.CurrentParkourMapIndex;
        Analytics.instance.SendEvent(forAnalytics);
        bool MapFound = false;
        var parkourPlayerData = new ParkourMapPlayerData();
        parkourPlayerData.MapName = Geekplay.Instance.PlayerData.CurrentParkourMapName;
        parkourPlayerData.IsCompleted = true;
        parkourPlayerData.timeInSeconds = minutes * 60 + seconds + milliseconds / 1000;
        if (Geekplay.Instance.PlayerData.parkourMapPlayerDataList != null)
        {
            for (int i = 0; i < Geekplay.Instance.PlayerData.parkourMapPlayerDataList.Count; i++)
            {
                if (Geekplay.Instance.PlayerData.parkourMapPlayerDataList[i].MapName == parkourPlayerData.MapName)
                {
                    MapFound = true;
                    if (Geekplay.Instance.PlayerData.parkourMapPlayerDataList[i].timeInSeconds > parkourPlayerData.timeInSeconds)
                    {
                        Geekplay.Instance.PlayerData.parkourMapPlayerDataList[i] = parkourPlayerData;
                    }
                }
            }

        }
        else
        {
            Geekplay.Instance.PlayerData.parkourMapPlayerDataList = new();
        }
        if (MapFound == false)
        {
            {
                Geekplay.Instance.PlayerData.parkourMapPlayerDataList.Add(parkourPlayerData);
            }
        }
        Geekplay.Instance.Save();
    }
    private void OnWinParkour()
    {
        WinMap = true;
        TrySerializeTimeValue();
        Debug.Log("Win parkour");
    }
    private void Update()
    {
        if (WinMap)
        {
            return;
        }
        CurrentTime = Time.time - StartTime;
        minutes = Mathf.Floor(CurrentTime / 60F);
        seconds = Mathf.RoundToInt(CurrentTime % 60);
        milliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
