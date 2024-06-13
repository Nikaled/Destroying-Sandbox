using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SerializeBlockManager : MonoBehaviour
{
    public List<Block> BlocksOnScene = new();
    public static SerializeBlockManager instance;
    public List<SaveBlockData> BlocksData;
    public ChooseBlockCell CellPrefab;
    public GameObject CellParent;
    public Block[] BlocksPrefab;
    public ContentGridManager CurrentManagerForSetPrefabs;
    private void Awake()
    {
        instance = this;
    }
    [ContextMenu("Set Names")]

    public void SetNames()
    {
        for (int i = 0; i < BlocksPrefab.Length; i++)
        {
            BlocksPrefab[i].NameToSerializing = BlocksPrefab[i].name;
        }
    }
    [ContextMenu("Set Prefabs to Cells")]
    public void SetPrefabsToCells()
    {
        CurrentManagerForSetPrefabs.Cells[2].blockPrefab = BlocksPrefab[0];
        //CurrentManagerForSetPrefabs.Cells = new ChooseBlockCell[47];
        //for (int i = 0; i < CurrentManagerForSetPrefabs.Cells.Length; i++)
        //{
        //    ChooseBlockCell prefab = Instantiate(CellPrefab);
        //    prefab.transform.parent = CellParent.transform;
        //    CurrentManagerForSetPrefabs.Cells[i] = prefab;
        //    CurrentManagerForSetPrefabs.Cells[i].blockPrefab = BlocksPrefab[i];
        //}
        SceneView.RepaintAll();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveBlocks();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadBlocks();
        }
    }
    public void SaveBlocks()
    {
        BlocksData = new();
        for (int i = 0; i < BlocksOnScene.Count; i++)
        {
            BlocksData.Add(BlocksOnScene[i].SaveBlock());
        }
        Geekplay.Instance.PlayerData.SavedBlocks = new();
        Geekplay.Instance.PlayerData.SavedBlocks = BlocksData;
        Debug.Log("Geekplay.Instance.PlayerData.SavedBlocks:" + Geekplay.Instance.PlayerData.SavedBlocks.Count);
        Geekplay.Instance.Save();
    }
    public void LoadBlocks()
    {
        BlocksData = new();
        BlocksData = Geekplay.Instance.PlayerData.SavedBlocks;
        Debug.Log("Geekplay.Instance.PlayerData.SavedBlocks:" + Geekplay.Instance.PlayerData.SavedBlocks.Count);
        for (int i = 0; i < BlocksData.Count; i++)
        {
            Block LoadedBlock = Instantiate(BlocksPrefab[BlocksData[i].PrefabIndex]);
            LoadedBlock.transform.position = BlocksData[i].Position;
            BlocksOnScene.Add(LoadedBlock);
        }
    }
    public void AddBlockToSaveList(Block blockToSave)
    {
        BlocksOnScene.Add(blockToSave);
    }
    public int FindIndex(Block blockToSave)
    {
        for (int i = 0; i < BlocksPrefab.Length; i++)
        {
            if(blockToSave.NameToSerializing == BlocksPrefab[i].NameToSerializing)
            {
                return i;
            }
        }
        Debug.Log("Индекс объекта не найден:" + blockToSave);
        return -1;
    }
}
