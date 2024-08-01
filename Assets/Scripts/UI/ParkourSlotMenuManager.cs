using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkourSlotMenuManager : MonoBehaviour
{
    [SerializeField] ParkourMapCell[] parkourCells;
    List<ParkourMapsPlayerData> playerMapData;
    private int GridIndex = 0;
    private int MapsInPage = 15;
    [SerializeField] MapDataSO Data;
    private void Start()
    {

        playerMapData = Geekplay.Instance.PlayerData.parkourMapPlayerDataList;
        if(playerMapData != null)
        {
            if(playerMapData.Count > 0)
            {
                for (int i = 0; i < parkourCells.Length; i++)
                {

                    parkourCells[i].LoadDataFromSO(Data.ParkourMaps[i]);
                    parkourCells[i].SetTimeToSlot(0);
                    parkourCells[i].IndexOfMap = i+ (MapsInPage* GridIndex) + 1;
                    for (int j = 0; j < playerMapData.Count; j++)
                    {
                        if(parkourCells[i].MapNameForScripts == playerMapData[j].MapName)
                        {
                            parkourCells[i].SetTimeToSlot(playerMapData[j].timeInSeconds);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < parkourCells.Length; i++)
                {
                    parkourCells[i].SetTimeToSlot(0);
                    parkourCells[i].LoadDataFromSO(Data.ParkourMaps[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < parkourCells.Length; i++)
            {
                parkourCells[i].SetTimeToSlot(0);
                parkourCells[i].LoadDataFromSO(Data.ParkourMaps[i]);
            }
        }
    }
}
