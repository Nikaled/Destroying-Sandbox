using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerializeBlockManager : MonoBehaviour
{
    public DestructionMapData destructionMapData;
    public ParkourMapsData parkourMapsData;
    /* [HideInInspector]*/
    public List<Block> BlocksOnScene = new();
    public static SerializeBlockManager instance;
    [SerializeField] public List<SaveBlockData> BlocksData;
    [SerializeField] public List<SaveParkourBlockData> ParkourBlocksData;
    //public ChooseBlockCell CellPrefab;
    //public GameObject CellParent;
    //public ContentGridManager CurrentManagerForSetPrefabs;
    public Block[] BlocksPrefab;
    public bool OnlyDestroyingMap;
    public bool OnlyParkourMap;
    public string CurrentParkourMapName;
    public MapData currentMapData;
    private readonly string EndedDestroyMap = "EndedDestroyMapAtNumber_";
    private readonly string EndedParkourMap = "EndedParkourMapAtNumber_";
    public bool IsCurrentMapLast;
    public MapDataSO MapDataSaver;
    [HideInInspector] int CurrentReward;
    private void Awake()
    {
        destructionMapData.DestructionMaps = MapDataSaver.DestructionMaps;
        parkourMapsData.ParkourMaps = MapDataSaver.ParkourMaps;
        instance = this;
    }
    [ContextMenu("GenerateBlocks")]
    private void Generate()
    {
        BlocksOnScene = new();
        Vector3 StartPoint = new Vector3(50, 2, 2);
        int width = 6;
        int lenght = 5;
        int height = 26;
        int Sdvig = 2;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                for (int y = 0; y < height; y++)
                {
                    if ((i > 0 && i < lenght - 1) && (j > 0 && j < width - 2) && (y != 0 && y%4 !=0)/*y != height - 1)*/)
                    {
                        continue;
                    }
                    var newBlock = Instantiate(BlocksPrefab[43], StartPoint + new Vector3(Sdvig * j, Sdvig * y, Sdvig * i), Quaternion.identity);
                    BlocksOnScene.Add(newBlock);
                }
            }
        }
        //for (int i = 0; i < lenght; i++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        var newBlock = Instantiate(BlocksPrefab[39], StartPoint + new Vector3(0, Sdvig * y, Sdvig * i), Quaternion.identity);
        //        BlocksOnScene.Add(newBlock);
        //    }
        //}


    }
    [ContextMenu("SaveDestructionMap")]
    private void SaveDestrMap()
    {
        if (MapDataSaver.DestructionMaps == null)
        {
            MapDataSaver.DestructionMaps = new();
        }
        MapData destrMapData = new();
        destrMapData.SavedBlocks = new();
        destrMapData.SavedBlocks = BlocksData;
        destrMapData.MapName = "Map_" + MapDataSaver.DestructionMaps.Count + 1;
        MapDataSaver.DestructionMaps.Add(destrMapData);
    }
    [ContextMenu("SaveParkourMap")]
    private void SaveParkourMap()
    {
        if (MapDataSaver.ParkourMaps == null)
        {
            MapDataSaver.ParkourMaps = new();
        }
        ParkourMapData parMapData = new();
        parMapData.SavedBlocks = new();
        parMapData.SavedBlocks = BlocksData;
        parMapData.MapName = $"P_Map_{MapDataSaver.ParkourMaps.Count + 1}";
        parMapData.ParkourBlocksSaveData = ParkourBlocksData;
        MapDataSaver.ParkourMaps.Add(parMapData);
    }
    [ContextMenu("LoadLastParkourMapForBuilding")]
    private void LoadLastParkourMapForBuilding()
    {
        Geekplay.Instance.PlayerData.CurrentParkourMapName = MapDataSaver.ParkourMaps[^1].MapName;
        parkourMapsData.ParkourMaps = MapDataSaver.ParkourMaps;
        //Geekplay.Instance.PlayerData.CurrentParkourMapIndex = 2;
        Geekplay.Instance.PlayerData.IsLoadingParkourMap = true;
        TryLoadMap();
        OnlyParkourMap = false;
    }
    private void TryLoadMap()
    {
        //Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
        //Geekplay.Instance.PlayerData.CurrentDestructionMapIndex = MapDataSaver.DestructionMaps[^1].MapIndex;
        //Geekplay.Instance.PlayerData.CurrentDestructionMapName = null;

        //Geekplay.Instance.PlayerData.IsLoadingParkourMap = true;
        //Geekplay.Instance.PlayerData.CurrentParkourMapName = MapDataSaver.ParkourMaps[^1].MapName;

        if (Geekplay.Instance.PlayerData.IsLoadingDestructionMap)
        {
            Geekplay.Instance.PlayerData.IsLoadingDestructionMap = false;
            if (Geekplay.Instance.PlayerData.CurrentDestructionMapName == null)
            {
                for (int i = 0; i < destructionMapData.DestructionMaps.Count; i++)
                {
                    if (Geekplay.Instance.PlayerData.CurrentDestructionMapIndex == destructionMapData.DestructionMaps[i].MapIndex)
                    {
                        currentMapData = destructionMapData.DestructionMaps[i];
                        Geekplay.Instance.PlayerData.CurrentDestructionMapName = currentMapData.MapName;
                        Geekplay.Instance.PlayerData.SavedBlocks = destructionMapData.DestructionMaps[i].SavedBlocks;
                        OnlyDestroyingMap = true;
                        CheckMapIsLastInLevelList(currentMapData.MapIndex, destructionMapData.DestructionMaps.Count);
                        LoadBlocks();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < destructionMapData.DestructionMaps.Count; i++)
                {
                    if (Geekplay.Instance.PlayerData.CurrentDestructionMapName == destructionMapData.DestructionMaps[i].MapName)
                    {
                        currentMapData = destructionMapData.DestructionMaps[i];
                        Geekplay.Instance.PlayerData.SavedBlocks = destructionMapData.DestructionMaps[i].SavedBlocks;
                        OnlyDestroyingMap = true;
                        CheckMapIsLastInLevelList(currentMapData.MapIndex, destructionMapData.DestructionMaps.Count);
                        LoadBlocks();
                        break;
                    }
                }
            }
        }
        else
        {
            if (Geekplay.Instance.PlayerData.IsLoadingParkourMap)
            {
                Geekplay.Instance.PlayerData.IsLoadingParkourMap = false;
                if (Geekplay.Instance.PlayerData.CurrentParkourMapName == null)
                {
                    for (int i = 0; i < parkourMapsData.ParkourMaps.Count; i++)
                    {
                        if (Geekplay.Instance.PlayerData.CurrentParkourMapIndex == parkourMapsData.ParkourMaps[i].MapIndex)
                        {
                            currentMapData = parkourMapsData.ParkourMaps[i];
                            ParkourBlocksData = new();
                            ParkourBlocksData = parkourMapsData.ParkourMaps[i].ParkourBlocksSaveData;
                            Geekplay.Instance.PlayerData.CurrentParkourMapName = currentMapData.MapName;
                            Geekplay.Instance.PlayerData.SavedBlocks = parkourMapsData.ParkourMaps[i].SavedBlocks;
                            OnlyParkourMap = true;
                            CheckMapIsLastInLevelList(currentMapData.MapIndex, parkourMapsData.ParkourMaps.Count);
                            LoadBlocks();

                            break;
                        }
                    }
                }
                else
                    for (int i = 0; i < parkourMapsData.ParkourMaps.Count; i++)
                    {
                        if (Geekplay.Instance.PlayerData.CurrentParkourMapName == parkourMapsData.ParkourMaps[i].MapName)
                        {
                            currentMapData = parkourMapsData.ParkourMaps[i];
                            ParkourBlocksData = new();
                            ParkourBlocksData = parkourMapsData.ParkourMaps[i].ParkourBlocksSaveData;
                            Geekplay.Instance.PlayerData.SavedBlocks = parkourMapsData.ParkourMaps[i].SavedBlocks;
                            OnlyParkourMap = true;
                            CheckMapIsLastInLevelList(currentMapData.MapIndex, parkourMapsData.ParkourMaps.Count);
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
        }
        else
        {
            TryLoadMap();
        }
        CanvasManager.instance.MapModeUISetup();
        Player.instance.MapSetup();
        if (OnlyDestroyingMap)
        {
            CycleManager.instance.ActivateDestroyingPhase();
            DestroyCounter.instance.AllBlockDestroyed += TryGetRewardForDestroyingMap;
            GameplayLocalization.instance.SetupReloadInstructionsOnOnlyDestroyed(true);
        }
        else
        {
            GameplayLocalization.instance.SetupReloadInstructionsOnOnlyDestroyed(false);
        }
        AdWarning.instance.SetupAdByModeAndStartTimer();
    }
    private void CheckMapIsLastInLevelList(int CurrentMapIndex, int ListCount)
    {
        if (CurrentMapIndex == ListCount - 1)
        {
            IsCurrentMapLast = true;
        }
        else
        {
            IsCurrentMapLast = false;

        }
    }
    public void LoadNextLevel()
    {
        int NewLevelIndex = currentMapData.MapIndex + 1;
        if (OnlyParkourMap)
        {
            Geekplay.Instance.PlayerData.IsLoadingParkourMap = true;
            Geekplay.Instance.PlayerData.CurrentParkourMapIndex = NewLevelIndex;
            Geekplay.Instance.PlayerData.CurrentParkourMapName = null;
        }
        else if (OnlyDestroyingMap)
        {
            Geekplay.Instance.PlayerData.IsLoadingDestructionMap = true;
            Geekplay.Instance.PlayerData.CurrentDestructionMapIndex = NewLevelIndex;
            Geekplay.Instance.PlayerData.CurrentDestructionMapName = null;
        }
        SceneManager.LoadScene(1);
    }
    private void TryGetRewardForDestroyingMap()
    {
        CurrentReward = 0;
        CanvasManager.instance.ShowRewardAndSetRewardText(true, 0);
        string forAnalytics = EndedDestroyMap + Geekplay.Instance.PlayerData.CurrentDestructionMapIndex;
        Analytics.instance.SendEvent(forAnalytics);
        var DList = Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList;
        var DMapName = Geekplay.Instance.PlayerData.CurrentDestructionMapName;
        bool MapFounded = false;
        if (DList != null)
        {
            for (int i = 0; i < DList.Count; i++)
            {
                if (DList[i] != null)
                {
                    if (DList[i].MapName == DMapName)
                    {
                        MapFounded = true;
                        if (DList[i].IsCompleted == false)
                        {
                            DList[i].IsCompleted = true;
                            Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
                            CurrentReward = currentMapData.RewardForComplete;
                            CanvasManager.instance.ShowRewardAndSetRewardText(true, currentMapData.RewardForComplete);
                            Geekplay.Instance.Save();
                        }
                    }
                }

            }
        }
        else
        {
            Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList = new();
        }

        if (MapFounded == false)
        {
            var dmData = new MapsPlayerData();
            dmData.MapName = currentMapData.MapName;
            dmData.IsCompleted = true;
            Geekplay.Instance.PlayerData.DestroyingMapPlayerDataList.Add(dmData);
            Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
            CurrentReward = currentMapData.RewardForComplete;
            CanvasManager.instance.ShowRewardAndSetRewardText(true, currentMapData.RewardForComplete);
            Geekplay.Instance.Save();
        }

    }
    public void TryGetRewardForParkourMap()
    {
        CanvasManager.instance.ShowRewardAndSetRewardText(true, 0);
        string forAnalytics = EndedParkourMap + Geekplay.Instance.PlayerData.CurrentParkourMapIndex;
        Analytics.instance.SendEvent(forAnalytics);
        var DList = Geekplay.Instance.PlayerData.parkourMapPlayerDataList;
        var DMapName = Geekplay.Instance.PlayerData.CurrentParkourMapName;
        bool MapFounded = false;
        if (DList != null)
        {
            for (int i = 0; i < DList.Count; i++)
            {
                if (DList[i] != null)
                {
                    if (DList[i].MapName == DMapName)
                    {
                        MapFounded = true;
                        ParkourManager.instance.TrySerializeTime(DList[i]);
                        if (DList[i].IsCompleted == false)
                        {
                            DList[i].IsCompleted = true;
                            Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
                            CurrentReward = currentMapData.RewardForComplete;
                            CanvasManager.instance.ShowRewardAndSetRewardText(true, currentMapData.RewardForComplete);
                            Geekplay.Instance.Save();
                        }
                    }
                }

            }
        }
        else
        {
            Geekplay.Instance.PlayerData.parkourMapPlayerDataList = new();
        }
        if (MapFounded == false)
        {
            var dmData = new ParkourMapsPlayerData();
            dmData.MapName = currentMapData.MapName;
            dmData.IsCompleted = true;
            ParkourManager.instance.TrySerializeTime(dmData);
            Geekplay.Instance.PlayerData.parkourMapPlayerDataList.Add(dmData);
            Geekplay.Instance.PlayerData.Coins += currentMapData.RewardForComplete;
            CurrentReward = currentMapData.RewardForComplete;
            CanvasManager.instance.ShowRewardAndSetRewardText(true, currentMapData.RewardForComplete);
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < BlocksOnScene.Count; i++)
            {
                BlocksOnScene[i].transform.position += new Vector3(0, 0, -2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < BlocksOnScene.Count; i++)
            {
                BlocksOnScene[i].transform.position += new Vector3(0, 0, -2);
            }
        }
    }
#endif
    public void SaveBlocks()
    {
        BlocksData = new();
        ParkourBlocksData = new();
        for (int i = 0; i < BlocksOnScene.Count; i++)
        {
            BlocksOnScene[i].SaveBlock();
            if (BlocksOnScene[i].GetComponent<ParkourBlock>() != null)
            {
                var ParkourBlockScript = BlocksOnScene[i].GetComponent<ParkourBlock>();
                var ParkData = ParkourBlockScript.GetParkourBlockData();
                ParkData.BlockOnSceneIndex = i;
                ParkourBlocksData.Add(ParkData);
                Debug.Log("Added Parkour Data");
            }

            BlocksData.Add(BlocksOnScene[i].saveBlockData);
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
        else if (Geekplay.Instance.PlayerData.MapDataArray.Length < 4)
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
    [ContextMenu("LoadBlocksAndChangeBlocks")]
    public void LoadBlocksAndChangeBlocks()
    {
        int blockToChangeIndex = 39;
        int blockWantChangeIndex = 11;
        if (BlocksOnScene.Count > 0)
        {
            for (int i = 0; i < BlocksOnScene.Count; i++)
            {
                if (BlocksOnScene[i] != null)
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
            if (BlocksData[i].PrefabIndex == blockToChangeIndex)
            {
                BlocksData[i].PrefabIndex = blockWantChangeIndex;
            }
            Block LoadedBlock = Instantiate(BlocksPrefab[BlocksData[i].PrefabIndex]);
            LoadedBlock.transform.position = BlocksData[i].Position;
            BlocksOnScene.Add(LoadedBlock);
        }
        LoadParkourBlocksSettings();

    }
    public void LoadBlocks()
    {
        if (BlocksOnScene.Count > 0)
        {
            for (int i = 0; i < BlocksOnScene.Count; i++)
            {
                if (BlocksOnScene[i] != null)
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
        LoadParkourBlocksSettings();

    }
    private void LoadParkourBlocksSettings()
    {
        for (int i = 0; i < ParkourBlocksData.Count; i++)
        {
            int BlockIndex = ParkourBlocksData[i].BlockOnSceneIndex;
            BlocksOnScene[BlockIndex].transform.rotation = ParkourBlocksData[i].rotation;
            BlocksOnScene[BlockIndex].transform.localScale = ParkourBlocksData[i].Scale;
            BlocksOnScene[BlockIndex].GetComponent<ParkourBlock>().SetBlockPos();
            BlocksOnScene[BlockIndex].GetComponent<ParkourBlock>().saveParkourBlockData = ParkourBlocksData[i];
        }
    }
    [ContextMenu("LoadBlocksFromBlocksData")]
    private void LoadBlocksFromBlocksData()
    {
        for (int i = 0; i < BlocksData.Count; i++)
        {
            Block LoadedBlock = Instantiate(BlocksPrefab[BlocksData[i].PrefabIndex]);
            LoadedBlock.transform.position = BlocksData[i].Position;
            BlocksOnScene.Add(LoadedBlock);
        }
    } // to Dev only
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
            if (blockToSave.NameToSerializing == BlocksPrefab[i].NameToSerializing)
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
    //[ContextMenu("Set Names")]

    //public void SetNames()
    //{
    //    for (int i = 0; i < BlocksPrefab.Length; i++)
    //    {

    //        BlocksPrefab[i].NameToSerializing = BlocksPrefab[i].name;
    //    }
    //}
}
