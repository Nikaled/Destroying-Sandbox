using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string NameToSerializing;
   [HideInInspector] public SaveBlockData saveBlockData;
    public  virtual SaveBlockData SaveBlock()
    {
        int blockPrefabIndex = FindIndexInPrefabList();
        Vector3Int pos = Vector3Int.CeilToInt(transform.position);
        saveBlockData = new SaveBlockData(blockPrefabIndex, pos);
        return new SaveBlockData(blockPrefabIndex, pos);
    }
    public virtual void AddBlockToSaveList()
    {
        SerializeBlockManager.instance.AddBlockToSaveList(this);
    }
    protected int FindIndexInPrefabList()
    {
        return SerializeBlockManager.instance.FindIndex(this);
    }
    public void DeleteBlock()
    {
        SerializeBlockManager.instance.BlocksOnScene.Remove(this);
        Destroy(gameObject);
    }
}
