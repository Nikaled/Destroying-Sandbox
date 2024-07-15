using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveBlockData 
{
   [SerializeField] public  int PrefabIndex;
    [SerializeField] public Vector3Int Position;
    public SaveBlockData(int PrefabIndex, Vector3Int Position)
    {
        this.PrefabIndex = PrefabIndex;
        this.Position = Position;
    }
}
