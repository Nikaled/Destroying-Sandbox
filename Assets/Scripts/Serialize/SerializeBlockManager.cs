using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SerializeBlockManager : MonoBehaviour
{
    public DestructionMapData destructionMapData;
    public List<Block> BlocksOnScene = new();
    public static SerializeBlockManager instance;
   [SerializeField] public List<SaveBlockData> BlocksData;
    //public ChooseBlockCell CellPrefab;
    //public GameObject CellParent;
    //public ContentGridManager CurrentManagerForSetPrefabs;
    public Block[] BlocksPrefab;
    private void Awake()
    {
        //Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
        //Geekplay.Instance.PlayerData.CurrentDestructionMapName = "AmethystWall";
        instance = this;
        if (Geekplay.Instance.PlayerData.IsLoadingDestructionMap)
        {
            Geekplay.Instance.PlayerData.IsLoadingDestructionMap = false;
            for (int i = 0; i < destructionMapData.DestructionMaps.Count; i++)
            {
                if(Geekplay.Instance.PlayerData.CurrentDestructionMapName == destructionMapData.DestructionMaps[i].MapName)
                {
                    BlocksData = destructionMapData.DestructionMaps[i].SavedBlocks;
                    LoadBlocks();
                    break;
                }
            }
        }
    }
    private void Start()
    {
        if (Geekplay.Instance.PlayerData.IsLoadingMapFromSlot == true)
        {
            Geekplay.Instance.PlayerData.IsLoadingMapFromSlot = false;
            LoadBlocksFromSlot(Geekplay.Instance.PlayerData.MapSlotToLoad);
            Geekplay.Instance.Save();
        }
    }

    private string WriteDate()
    {
        DateTime date1 = DateTime.Now;
       string MapDate = date1.ToString("HH:mm  dd.MM.yyyy");
        return MapDate;
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
    public void SaveBlocksInSlot(int SlotIndex)
    {   
        SaveBlocks();
        int MapDataSlotIndex = SlotIndex;
        if (Geekplay.Instance.PlayerData.MapDataArray == null)
        {
            Geekplay.Instance.PlayerData.MapDataArray = new MapData[4];
        }
        if (Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex] == null)
        {
        Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex] = new();
        }
        Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex].SavedBlocks = new();
        Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex].SavedBlocks = BlocksData;
        Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex].SaveDate = WriteDate();
       Debug.Log($"Geekplay.Instance.PlayerData.MapaDataList[{MapDataSlotIndex}].SavedBlocks:" + Geekplay.Instance.PlayerData.MapDataArray[MapDataSlotIndex].SavedBlocks.Count);
        Geekplay.Instance.Save();
    }
    private int FindMapSlotIndexByName(string SlotName)
    {
        for (int i = 0; i < Geekplay.Instance.PlayerData.MapDataArray.Length; i++)
        {
           if(Geekplay.Instance.PlayerData.MapDataArray[i].MapName == SlotName)
            {
                return i;
            }
        }
        Debug.Log("Не найден слот по имени:" + SlotName);
        return -1;
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
    public void LoadBlocksFromSlot(int index)
    {
        BlocksData = new();
        BlocksData = Geekplay.Instance.PlayerData.MapDataArray[index].SavedBlocks;
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
    //[ContextMenu("Set Prefabs to Cells")]
    //public void SetPrefabsToCells()
    //{
    //    CurrentManagerForSetPrefabs.Cells = new ChooseBlockCell[47];
    //    for (int i = 0; i < CurrentManagerForSetPrefabs.Cells.Length; i++)
    //    {
    //        ChooseBlockCell prefab = Instantiate(CellPrefab);
    //        prefab.transform.parent = CellParent.transform;
    //        ConvertToPrefabInstanceSettings convertToPrefabInstanceSettings = new ConvertToPrefabInstanceSettings();
    //        convertToPrefabInstanceSettings.recordPropertyOverridesOfMatches = true;
    //        CurrentManagerForSetPrefabs.Cells[i] = prefab;
    //        CurrentManagerForSetPrefabs.Cells[i].blockPrefab = BlocksPrefab[i];
    //    }
    //    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    //    //SceneView.RepaintAll();
    //}
    [ContextMenu("Set Names")]

    public void SetNames()
    {
        for (int i = 0; i < BlocksPrefab.Length; i++)
        {
            BlocksPrefab[i].NameToSerializing = BlocksPrefab[i].name;
        }
    }
}
