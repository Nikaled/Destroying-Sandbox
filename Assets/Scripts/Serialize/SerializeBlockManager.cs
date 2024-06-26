using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SerializeBlockManager : MonoBehaviour
{
    public DestructionMapData destructionMapData;
    public ParkourMapsData parkourMapsData;
    public List<Block> BlocksOnScene = new();
    public static SerializeBlockManager instance;
   [SerializeField] public List<SaveBlockData> BlocksData;
    //public ChooseBlockCell CellPrefab;
    //public GameObject CellParent;
    //public ContentGridManager CurrentManagerForSetPrefabs;
    public Block[] BlocksPrefab;
    public bool OnlyDestroyingMap;
    public bool OnlyParkourMap;
    public string CurrentParkourMapName;
    private MapData currentMapData;
    private readonly string EndedDestroyMap = "EndedDestroyMapAtNumber_";

    private void Awake()
    {

        instance = this;
        if (Geekplay.Instance.PlayerData.IsLoadingDestructionMap)
        {
            Geekplay.Instance.PlayerData.IsLoadingDestructionMap = false;
            for (int i = 0; i < destructionMapData.DestructionMaps.Count; i++)
            {
                if(Geekplay.Instance.PlayerData.CurrentDestructionMapName == destructionMapData.DestructionMaps[i].MapName)
                {
                    currentMapData = destructionMapData.DestructionMaps[i];
                    Geekplay.Instance.PlayerData.SavedBlocks = destructionMapData.DestructionMaps[i].SavedBlocks;
                    OnlyDestroyingMap = true;
                    LoadBlocks();
                    break;
                }
            }
           
        }
        else
        {
            if (Geekplay.Instance.PlayerData.IsLoadingParkourMap)
            {
                Geekplay.Instance.PlayerData.IsLoadingParkourMap = false;
                for (int i = 0; i < parkourMapsData.ParkourMaps.Count; i++)
                {
                    if (Geekplay.Instance.PlayerData.CurrentParkourMapName == parkourMapsData.ParkourMaps[i].MapName)
                    {
                        currentMapData = parkourMapsData.ParkourMaps[i];
                        Geekplay.Instance.PlayerData.SavedBlocks = parkourMapsData.ParkourMaps[i].SavedBlocks;
                        OnlyParkourMap = true;
                        LoadBlocks();
                        break;
                    }
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
            CycleManager.instance.ActivateDestroyingPhase();
        }
        if (OnlyDestroyingMap)
        {
            CycleManager.instance.ActivateDestroyingPhase();
            DestroyCounter.instance.AllBlockDestroyed += TryGetRewardForDestroyingMap;
        }
    }

    private void TryGetRewardForDestroyingMap()
    {
        string forAnalytics = EndedDestroyMap + Geekplay.Instance.PlayerData.CurrentDestructionMapIndex;
        Analytics.instance.SendEvent(forAnalytics);
        var DList =  Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList;
        var DMapName = Geekplay.Instance.PlayerData.CurrentDestructionMapName;
        bool MapFounded = false;
        for (int i = 0; i < DList.Count; i++)
        {
            if(DList[i] != null)
            {
                if (DList[i].MapName == DMapName)
                {
                    if (DList[i].IsRewardTaked == false)
                    {
                        MapFounded = true;
                        DList[i].IsRewardTaked = true;
                        Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
                        Geekplay.Instance.Save();
                    }
                }
            }
           
        }
        if(MapFounded == false)
        {
            var dmData = new DestroyingMapsPlayerData();
            dmData.MapName = currentMapData.MapName;
            dmData.IsRewardTaked = true;
            Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList.Add(dmData);
            Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
            Geekplay.Instance.Save();
        }
           
    }
    private string WriteDate()
    {
        DateTime date1 = DateTime.Now;
       string MapDate = date1.ToString("HH:mm  dd.MM.yyyy");
        return MapDate;
    }

#if UNITY_EDITOR
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
#endif
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
        else if(Geekplay.Instance.PlayerData.MapDataArray.Length < 4)
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
    public void LoadBlocks()
    {
        if (BlocksOnScene.Count > 0)
        {
            for (int i = 0; i < BlocksOnScene.Count; i++)
            {
                if(BlocksOnScene[i] != null)
                {
                    Destroy(BlocksOnScene[i].gameObject);
                    Debug.Log("Удален объект:" + BlocksOnScene[i].name);
                }
            }
            BlocksOnScene = new();
        }
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
    //[ContextMenu("Set Sprites to Cells")]
    //public void SetPrefabsToCells()
    //{
    //    for (int i = 0; i < CurrentManagerForSetPrefabs.Cells.Length; i++)
    //    {
    //        CurrentManagerForSetPrefabs.Cells[i].blockSprite.sprite = SpritesToCells[i];
    //        GameObject c = CurrentManagerForSetPrefabs.Cells[i].gameObject;
    //        UnityEditor.EditorUtility.SetDirty(CurrentManagerForSetPrefabs.Cells[i].blockSprite.sprite);
    //    }
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
