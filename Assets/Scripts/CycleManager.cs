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
    public void ActivateDestroyingPhase()
    {
       DestroyingPhaseStarted?.Invoke();
    }
    public void ActivateBuildingPhase()
    {
        BuildingPhaseStarted?.Invoke();
    }
}