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
}
