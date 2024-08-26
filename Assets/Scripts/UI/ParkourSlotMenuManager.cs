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
    [SerializeField] ParkourMapNamesSO _mapNames;
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
                    parkourCells[i].IndexOfMap = i+ (MapsInPage* GridIndex);
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
#if UNITY_EDITOR
    [ContextMenu("SetNamesToSO")]
    public void SetNamesToSO()
    {
        _mapNames.RusNames = new string[parkourCells.Length];
        _mapNames.EngNames = new string[parkourCells.Length];
        _mapNames.TrNames = new string[parkourCells.Length];
        for (int i = 0; i < parkourCells.Length; i++)
        {
            _mapNames.RusNames[i] = parkourCells[i].MapNameRu;
            _mapNames.EngNames[i] = parkourCells[i].MapNameEn;
            _mapNames.TrNames[i] = parkourCells[i].MapNameTr;
        }
        UnityEditor.EditorUtility.SetDirty(_mapNames);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif
}
