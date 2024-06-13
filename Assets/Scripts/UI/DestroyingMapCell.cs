using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingMapCell : MonoBehaviour
{
    public string MapName;

    public void LoadDestroyingMap()
    {
        Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
        Geekplay.Instance.PlayerData.CurrentDestructionMapName = "AmethystWall";
    }
}
