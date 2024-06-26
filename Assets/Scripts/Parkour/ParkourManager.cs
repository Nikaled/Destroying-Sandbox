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
    private bool CountDown;
    [SerializeField] GameObject CountDownOnStartPanel;
    [SerializeField] TextMeshProUGUI CountDownTimer;
    private void Start()
    {
        instance = this;
        CountDown = true;
        if (SerializeBlockManager.instance.OnlyParkourMap)
        {
            StartParkour();
            ParkourWinZone.instance.WinParkour += OnWinParkour;
        }
    }
    public void StartParkour()
    {
        CountDown = true;
        Player.instance.examplePlayer.LockCursor(true);
        Player.instance.InterfaceActive = true;
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
        StartCoroutine(CountDownOnStart());
    }
    private IEnumerator CountDownOnStart()
    {
        CountDownOnStartPanel.SetActive(true);

        int Timer = 4;
        while (Timer != 1)
        {
            Timer--;
            CountDownTimer.text = Timer.ToString();
            yield return new WaitForSeconds(1f);
        }
        Geekplay.Instance.Save();
        CountDownOnStartPanel.SetActive(false);
        Player.instance.InterfaceActive = false;
        StartTime = Time.time;
        WinMap = false;
        CountDown = false;
    }
    public float GetTimeInSeconds()
    {
        return minutes * 60 + seconds + milliseconds / 1000;
    }
    public void TrySerializeTime(ParkourMapsPlayerData currentParkourMapSlot)
    {
        float currentRecord = GetTimeInSeconds();
        if (currentParkourMapSlot.timeInSeconds > currentRecord || currentParkourMapSlot.timeInSeconds == 0)
        {
            currentParkourMapSlot.timeInSeconds = currentRecord;
        }
    }
    private void OnWinParkour()
    {
        WinMap = true;
        SerializeBlockManager.instance.TryGetRewardForParkourMap();
        Debug.Log("Win parkour");
    }
    private void Update()
    {
        if (WinMap || CountDown)
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
