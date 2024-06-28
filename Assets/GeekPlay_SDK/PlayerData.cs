using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int Coins { 
        get
        {
            return _coinsDontUse;
        }
        set
        {
            _coinsDontUse = value;
            CoinsChanged?.Invoke(_coinsDontUse);
        }
    }

    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;
    public bool IsGeometryDashRewardTaked;

    public event Action<int> CoinsChanged;
    public int _coinsDontUse;

    public bool IsFirstPlay;
    public bool IsFirstGameplay;
    public bool IsFirstMovement;
    public bool IsFirstBlockPlaced;

    public bool IsLoadingMapFromSlot;
    public int MapSlotToLoad;
    public bool IsLoadingDestructionMap;
    public bool IsLoadingParkourMap;
    public string CurrentDestructionMapName;
    public string CurrentParkourMapName;
    public int CurrentParkourMapIndex;
    public int CurrentDestructionMapIndex;
    public List<SaveBlockData> SavedBlocks;
    public MapData[] MapDataArray;
    public List<ParkourMapPlayerData> parkourMapPlayerDataList;
    public List<DestroyingMapsPlayerData> DestroyingMapPlayerDataList;
    public int DestroyCount;
    public bool IsParkourSpeedUpForReward;
    public bool[] WeaponOpenedArray;




    public List<int> Codes;
    /////InApps//////
    public string lastBuy;

    public int downloadsCount2;
}