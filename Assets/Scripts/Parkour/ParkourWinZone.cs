using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParkourWinZone : MonoBehaviour
{
    public static ParkourWinZone instance;
    public Action WinParkour;
    private bool IsWinAlready;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        IsWinAlready = false;
        CycleManager.instance.ParkourPhaseStarted += OnRestart;
    }
    private void OnDisable()
    {
        CycleManager.instance.ParkourPhaseStarted -= OnRestart;
    }
    private void OnRestart()
    {
        IsWinAlready = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(IsWinAlready == false)
        {
            if (other.CompareTag("Player"))
            {
                IsWinAlready = true;
                WinParkour?.Invoke();
            }
        }
        
    }
}
