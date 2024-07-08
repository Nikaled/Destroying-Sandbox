using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestroyCounter : DestroyCounter
{
    public override void ObjectDestroyed()
    {
        TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName5);
        return;
    }
}
