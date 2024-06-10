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

    public event Action<int> CoinsChanged;
    public int _coinsDontUse;

    public bool IsFirstPlay;

    public List<SaveBlockData> SavedBlocks;
    public int DestroyCount;

    public List<int> Codes;
    /////InApps//////
    public string lastBuy;

    public int downloadsCount2;
    internal bool IsGeometryDashRewardTaked;
}