using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string NameToSerializing;
    public SaveBlockData SaveBlock()
    {
        int blockPrefabIndex = FindIndexInPrefabList();
        Vector3Int pos = Vector3Int.CeilToInt(transform.position);
       return  new SaveBlockData(blockPrefabIndex, pos);
    }
    public void AddBlockToSaveList()
    {
        SerializeBlockManager.instance.AddBlockToSaveList(this);
    }
    private int FindIndexInPrefabList()
    {
        return SerializeBlockManager.instance.FindIndex(this);
    }
    public void DeleteBlock()
    {
        SerializeBlockManager.instance.BlocksOnScene.Remove(this);
        Destroy(gameObject);
    }
}
