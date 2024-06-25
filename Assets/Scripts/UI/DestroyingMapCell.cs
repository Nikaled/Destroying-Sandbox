using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class DestroyingMapCell : MonoBehaviour
{
    public string MapNameForScripts;
    public int RewardForMap;
    public TextMeshProUGUI MapReward;
    public TextMeshProUGUI MapNameText;
    public string MapNameRu;
    public string MapNameEn;
    public string MapNameTr;
    string IsCompletedText;
    string RewardIsText;
    public Image CoinsImage;
    [HideInInspector] public int IndexOfMap;
    private readonly string StartedDestroyMap = "StartedDestroyMapAtNumber_";
    public virtual void Awake()
    {
        if (Geekplay.Instance.language == "ru")
        {
            RewardIsText = "Награда:";
            IsCompletedText = "Пройдено";
            MapNameText.text = MapNameRu;
        }
        else if (Geekplay.Instance.language == "en")
        {
            RewardIsText = "Reward:";
            IsCompletedText = "Completed";
            MapNameText.text = MapNameEn;
        }
        else if (Geekplay.Instance.language == "tr")
        {
            RewardIsText = "Ödül:";
            IsCompletedText = "Geçti";
            MapNameText.text = MapNameTr;

        }
        MapReward.text = RewardIsText + RewardForMap.ToString();
    }
    public virtual void LoadDestroyingMap()
    {
        Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
        Geekplay.Instance.PlayerData.CurrentDestructionMapName = MapNameForScripts;
        Geekplay.Instance.ShowInterstitialAd();
        string forEvent = StartedDestroyMap + IndexOfMap;
        Geekplay.Instance.PlayerData.CurrentDestructionMapIndex = IndexOfMap;
        Analytics.instance.SendEvent(forEvent);
        SceneManager.LoadScene(1);
    }
    public void SetMapRewardTextOnCompleted(bool IsCompleted)
    {
        if (IsCompleted)
        {
            MapReward.text = IsCompletedText;
            CoinsImage.enabled = false;
        }
    }
}
