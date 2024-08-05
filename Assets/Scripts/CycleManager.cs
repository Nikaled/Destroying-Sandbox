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
    public Phase currentPhase;
    public enum Phase
    {
        Building,
        Destroying,
        Parkour
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //if(currentPhase != Phase.Destroying)
        //{
        //    currentPhase = Phase.Building;
        //}
        ChangePhaseButtonFunc(Phase.Building);
    }
    protected virtual void Update()
    {
        if (Player.instance.InterfaceActive || Player.instance.AdWarningActive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Player.instance.currentState == Player.PlayerState.Idle || Player.instance.currentState == Player.PlayerState.Building)
                SwitchPhase();
        }
    }
    public virtual void SwitchPhase()
    {
        if (currentPhase == Phase.Destroying)
        {
            ActivateBuildingPhase();
        }
        else if (currentPhase == Phase.Building)
        {
             ActivateDestroyingPhase();
        }
    }
    protected void ChangePhaseButtonFunc(Phase newPhase)
    {
        CanvasManager.instance.ChangePhaseButton.onClick.RemoveAllListeners();
        if (newPhase == Phase.Destroying)
        {
            //CanvasManager.instance.ChangePhaseButton.onClick.AddListener(delegate { ActivateBuildingPhase(); });
            CanvasManager.instance.ChangePhaseButton.onClick.AddListener(delegate { SwitchPhase(); });
        }
        else if (newPhase == Phase.Building)
        {
            //CanvasManager.instance.ChangePhaseButton.onClick.AddListener(delegate { ActivateDestroyingPhase(); });
            CanvasManager.instance.ChangePhaseButton.onClick.AddListener(delegate { SwitchPhase(); });
        }
    }
    public virtual void ActivateDestroyingPhase()
    {
        if(SerializeBlockManager.instance.BlocksOnScene.Count == 0)
        {
            return;
        }
        currentPhase = Phase.Destroying;
        DestroyCounter.instance.DestroyPhaseStarted();
        SerializeBlockManager.instance.SaveBlocks();
        Player.instance.OnDestroyingPhaseActivated();
        CanvasManager.instance.ShowCurrentDestroyInterface(true);
        DestroyingPhaseStarted?.Invoke();
        if (Geekplay.Instance.mobile)
        {
            ChangePhaseButtonFunc(Phase.Destroying);
            CanvasManager.instance.ChangePhaseButtonIcon(0);
            CanvasManager.instance.ChangeDoButtonImageToMode(false);
        }
    }

    public void ActivateParkourPhase()  // if player taked restart
    {
        currentPhase = Phase.Parkour;
        CanvasManager.instance.ShowWinParkourUI(false);
        ParkourManager.instance.StartParkour();
        ParkourPhaseStarted?.Invoke();

    }
    public virtual void ActivateBuildingPhase()
    {
        currentPhase = Phase.Building;
        SerializeBlockManager.instance.LoadBlocks();
        Player.instance.OnBuildingPhaseActivated();
        CanvasManager.instance.ShowWinMapUI(false);
        CanvasManager.instance.ShowCurrentDestroyInterface(false);
        BuildingPhaseStarted?.Invoke();
        if (Geekplay.Instance.mobile)
        {
            ChangePhaseButtonFunc(Phase.Building);
            CanvasManager.instance.ChangePhaseButtonIcon(1);
            CanvasManager.instance.ChangeDoButtonImageToMode(true);
        }
        if (SerializeBlockManager.instance.OnlyDestroyingMap)
        {
            ActivateDestroyingPhase();
            return;
        }
    }
}
