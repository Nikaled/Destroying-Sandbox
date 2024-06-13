using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadMapView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] Dates;
    private List<MapData> mapData;
    string EmptySlotText;
    private void Start()
    {
        EmptySlotText = "Пустой слот";
        SetDateText();
    }
    public void SetDateText()
    {
        mapData = new();
        if (Geekplay.Instance.PlayerData.MapDataArray != null)
        {
            mapData.AddRange(Geekplay.Instance.PlayerData.MapDataArray);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                mapData.Add(null);
            }
        }
        for (int i = 0; i < Dates.Length; i++)
        {
            Dates[i].text = EmptySlotText;
            if (mapData[i] != null)
            {
                if (string.IsNullOrEmpty(mapData[i].SaveDate) == false)
                {
                    Dates[i].text = mapData[i].SaveDate;
                }
            }
        }
    }

    public void LoadMap(int index)
    {
        Geekplay.Instance.PlayerData.MapSlotToLoad = index;
        Geekplay.Instance.PlayerData.IsLoadingMapFromSlot = true;
        Geekplay.Instance.Save();
        SceneManager.LoadScene(1);
    }
}
