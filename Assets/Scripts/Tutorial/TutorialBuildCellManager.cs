using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBuildCellManager : BuildCellManager
{
    protected override void OnBlockPlaced()
    {
        base.OnBlockPlaced();
        TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName3);
        if (Player.instance.CurrentBlock.CompareTag("Unit"))
        {
            TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName6);
        }
    }
}
