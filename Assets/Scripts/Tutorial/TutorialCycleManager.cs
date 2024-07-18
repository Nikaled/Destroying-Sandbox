using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCycleManager : CycleManager
{
    protected override void Update()
    {
        if (Player.instance.InterfaceActive || Player.instance.AdWarningActive)
        {
            return;
        }
        if (TutorialManager.instance.AbleToChangeMode)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (Player.instance.currentState == Player.PlayerState.Idle || Player.instance.currentState == Player.PlayerState.Building)
                {
                    SwitchPhase();                   
                }
            }
        }
    }
    public override void SwitchPhase()
    {
        if (currentPhase == Phase.Destroying)
        {
            ActivateBuildingPhase();
        }
        else if (currentPhase == Phase.Building)
        {
            ActivateDestroyingPhase();
        }
        TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName6);
    }
    public override void ActivateDestroyingPhase()
    {
        Debug.Log("Destroy Phase Activated");

        currentPhase = Phase.Destroying;
        SerializeBlockManager.instance.SaveBlocks();
        Player.instance.OnDestroyingPhaseActivated();
        DestroyingPhaseStarted?.Invoke();
        if (Geekplay.Instance.mobile)
        {
            ChangePhaseButtonFunc(Phase.Destroying);
            CanvasManager.instance.ChangePhaseButtonIcon(0);
            CanvasManager.instance.ChangeDoButtonImageToMode(false);
        }
    }
    public override void ActivateBuildingPhase()
    {
        Debug.Log("Building Phase Activated");
        currentPhase = Phase.Building;
        SerializeBlockManager.instance.LoadBlocks();
        BuildingPhaseStarted?.Invoke();
        Player.instance.OnBuildingPhaseActivated();
        if (Geekplay.Instance.mobile)
        {
            ChangePhaseButtonFunc(Phase.Building);
            CanvasManager.instance.ChangePhaseButtonIcon(1);
            CanvasManager.instance.ChangeDoButtonImageToMode(true);
        }
    }
}
