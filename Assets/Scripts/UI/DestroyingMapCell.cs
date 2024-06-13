using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyingMapCell : MonoBehaviour
{
    public string MapName;

    public void LoadDestroyingMap()
    {
        Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
        Geekplay.Instance.PlayerData.CurrentDestructionMapName = MapName;
        Geekplay.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(1);
    }
}
