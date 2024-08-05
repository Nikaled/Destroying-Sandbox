using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMapView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] Dates;
    [SerializeField] TextMeshProUGUI MapName1;
    [SerializeField] TextMeshProUGUI MapName2;
    [SerializeField] TextMeshProUGUI MapName3;
    [SerializeField] TextMeshProUGUI MapName4;
    private List<MapData> mapData;
    string EmptySlotText;
    [SerializeField] Button[] LoadSlots;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (LoadSlots != null)
            {
                if (LoadSlots.Length > 0)
                {
                    for (int i = 0; i < LoadSlots.Length; i++)
                    {
                        LoadSlots[i].enabled = false;
                    }
                }
            }
        }
       
        LocalizateNames();
       
        SetDateText();
    }
    private void LocalizateNames()
    {
        if (Geekplay.Instance.language == "ru")
        {
            EmptySlotText = "Пустой слот";
            MapName1.text = "Карта 1";
            MapName2.text = "Карта 2";
            MapName3.text = "Карта 3";
            MapName4.text = "Карта 4";
        }
        if (Geekplay.Instance.language == "en")
        {
            EmptySlotText = "Empty slot";
            MapName1.text = "Map 1";
            MapName2.text = "Map 2";
            MapName3.text = "Map 3";
            MapName4.text = "Map 4";
        }
        
        if (Geekplay.Instance.language == "tr")
        {
            EmptySlotText = "boş yuva";
            MapName1.text = "Harita 1";
            MapName2.text = "Harita 2";
            MapName3.text = "Harita 3";
            MapName4.text = "Harita 4";
        }
    }
    public void SetDateText()
    {
        mapData = new();
        if (Geekplay.Instance.PlayerData.MapDataArray != null)
        {
            if(Geekplay.Instance.PlayerData.MapDataArray.Length > 0)
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
                    if(LoadSlots != null)
                    {
                        if(LoadSlots.Length > i)
                        {
                            LoadSlots[i].enabled = true;
                        }
                    }
                   
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
