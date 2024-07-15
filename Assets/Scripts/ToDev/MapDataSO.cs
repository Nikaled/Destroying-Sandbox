using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 53)]
public class MapDataSO : ScriptableObject
{
   public List<MapData> DestructionMaps;
    public List<ParkourMapData> ParkourMaps;
}
