using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public static CycleManager instance;
    public Action DestroyingPhaseStarted;
    public Action BuildingPhaseStarted;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateDestroyingPhase();
        }
    }
    public void ActivateDestroyingPhase()
    {
        SerializeBlockManager.instance.SaveBlocks();
        CitizenNavMeshManager.instance.BuildNavMesh();
       DestroyingPhaseStarted?.Invoke();
    }
    public void ActivateBuildingPhase()
    {
        BuildingPhaseStarted?.Invoke();
    }
}
