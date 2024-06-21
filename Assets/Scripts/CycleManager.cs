using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public static CycleManager instance;
    public Action DestroyingPhaseStarted;
    public Action BuildingPhaseStarted;
    public Action ParkourPhaseStarted;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Player.instance.currentState == Player.PlayerState.Building)
                ActivateDestroyingPhase();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (Player.instance.currentState == Player.PlayerState.Idle)
                ActivateBuildingPhase();
        }
    }
    public void ActivateDestroyingPhase()
    {
        DestroyCounter.instance.DestroyPhaseStarted();
        SerializeBlockManager.instance.SaveBlocks();
        CitizenNavMeshManager.instance.BuildNavMesh();
        Player.instance.OnDestroyingPhaseActivated();
        CanvasManager.instance.ShowCurrentDestroyInterface(true);
        DestroyingPhaseStarted?.Invoke();
    }


    public void ActivateParkourPhase()  // if player taked restart
    {
        CanvasManager.instance.ShowWinParkourUI(false);
        ParkourManager.instance.StartParkour();
        ParkourPhaseStarted?.Invoke();
    }
    public void ActivateBuildingPhase()
    {
        if (SerializeBlockManager.instance.OnlyDestroyingMap)
        {
            return;
        }
        SerializeBlockManager.instance.LoadBlocks();
        Player.instance.OnBuildingPhaseActivated();
        CanvasManager.instance.ShowWinMapUI(false);
        CanvasManager.instance.ShowCurrentDestroyInterface(false);
        BuildingPhaseStarted?.Invoke();
    }
}
