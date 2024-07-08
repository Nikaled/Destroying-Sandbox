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
        if (TutorialManager.instance.AnimalPlaced)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (Player.instance.currentState == Player.PlayerState.Idle || Player.instance.currentState == Player.PlayerState.Building)
                {
                    SwitchPhase();
                    TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName7);
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
    }
    public override void ActivateDestroyingPhase()
    {
        Debug.Log("Destroy Phase Activated");

        currentPhase = Phase.Destroying;
        Player.instance.OnDestroyingPhaseActivated();
        DestroyingPhaseStarted?.Invoke();
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ChangePhaseButtonIcon(0);
            CanvasManager.instance.ChangeDoButtonImageToMode(false);
        }
    }
    public override void ActivateBuildingPhase()
    {
        Debug.Log("Building Phase Activated");
        currentPhase = Phase.Building;
        Player.instance.OnBuildingPhaseActivated();
        BuildingPhaseStarted?.Invoke();
        if (Geekplay.Instance.mobile)
        {
            ChangePhaseButtonFunc(Phase.Building);
            CanvasManager.instance.ChangePhaseButtonIcon(1);
            CanvasManager.instance.ChangeDoButtonImageToMode(true);
        }
    }
}
