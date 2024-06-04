using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Analytics : MonoBehaviour
{
    public static Analytics instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void SendEvent(string eventStr)
    {

        if (Geekplay.Instance.Platform != Platform.Editor)
        {
            try
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, eventStr);
                //AppMetrica.Instance.ReportEvent(eventStr);
            }
            catch (Exception e)
            {

            }
        }
        else
        {
            Debug.Log("Отправлен ивент:" + eventStr);
        }
    }
}

