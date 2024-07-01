using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData 
{
    public string MapName;
    public int MapIndex;
  [HideInInspector]  public string SaveDate;
    public int RewardForComplete;
    public List<SaveBlockData> SavedBlocks;
}
