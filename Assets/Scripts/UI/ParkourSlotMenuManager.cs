using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourSlotMenuManager : MonoBehaviour
{
    [SerializeField] ParkourMapCell[] parkourCells;
    List<ParkourMapPlayerData> playerMapData;

    private void Start()
    {
        playerMapData = Geekplay.Instance.PlayerData.parkourMapPlayerDataList;
        if(playerMapData != null)
        {
            if(playerMapData.Count > 0)
            {
                for (int i = 0; i < parkourCells.Length; i++)
                {
                    parkourCells[i].SetTimeToSlot(0);
                    for (int j = 0; j < playerMapData.Count; j++)
                    {
                        Debug.Log("parkourCells[i].name:"+ parkourCells[i].MapName);
                        Debug.Log(" playerMapData[j].MapName:" + playerMapData[j].MapName);
                        if(parkourCells[i].MapName == playerMapData[j].MapName)
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
                }
            }
        }
        else
        {
            for (int i = 0; i < parkourCells.Length; i++)
            {
                parkourCells[i].SetTimeToSlot(0);
            }
        }
    }
}
