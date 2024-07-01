using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingMainMenuGrid : MonoBehaviour
{
    [SerializeField] DestroyingMapCell[] Cells;
    private List<MapsPlayerData> completedMaps;
    private int GridIndex =0;
    private int MapsInPage = 15;
    private void Start()
    {
        completedMaps = Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList;
        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i].IndexOfMap = i + (MapsInPage * GridIndex) + 1;
            for (int j = 0; j < completedMaps.Count; j++)
            {
            if(Cells[i].MapNameForScripts == completedMaps[j].MapName)
                {
                    Cells[i].SetMapRewardTextOnCompleted(completedMaps[j].IsCompleted);
                }
            }
        }
    }
}
