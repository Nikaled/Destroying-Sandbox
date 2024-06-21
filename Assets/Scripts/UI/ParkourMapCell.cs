using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ParkourMapCell : MonoBehaviour
{
    public string MapName;
    [SerializeField] TextMeshProUGUI MapTime;
    public void LoadParkourMap()
    {
        Geekplay.Instance.PlayerData.IsLoadingParkourMap = true;
        Geekplay.Instance.PlayerData.CurrentParkourMapName = MapName;
        Geekplay.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(1);
    }
    public void SetTimeToSlot(float TimeInSeconds)
    {
        if(TimeInSeconds == 0)
        {
            MapTime.text = "Нет рекорда";
            return;
        }
        int minutes =(int) TimeInSeconds / 60;
        int seconds = (int) TimeInSeconds - minutes*60;
        float milliseconds = (TimeInSeconds - (int)TimeInSeconds) *1000;
        MapTime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
