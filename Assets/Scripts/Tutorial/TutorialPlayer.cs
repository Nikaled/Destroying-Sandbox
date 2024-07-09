using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : Player
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void Start()
    {
        base.Start();
        WeaponSelector.instance.UnlockAllWeaponForTutorial();
    }
    public override void ChangeWeaponInput()
    {
        base.ChangeWeaponInput();
    }
    public override void SwitchActiveBlockSlot(int PressedNumber)
    {
        base.SwitchActiveBlockSlot(PressedNumber);
        TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName2);
    }
    public override void ActivateBuildingMenu(bool Is)
    {
        if(TutorialManager.instance.AnimalZoneReached == false)
        {
            return;
        }
        base.ActivateBuildingMenu(Is);
    }
    public override void SwitchPlayerState(PlayerState newPlayerState, float Delay = 0.1f)
    {
        base.SwitchPlayerState(newPlayerState, Delay);
        if (Geekplay.Instance.mobile)
        {
            if(TutorialManager.instance.AbleToChangeMode == false)
            {
                CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(false);
            }
            else
            {
                CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(true);
            }
            CanvasManager.instance.BuildingMenuButton.SetActive(false);
            CanvasManager.instance.SaveButton.gameObject.SetActive(false);
        }
    }

}
