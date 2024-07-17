using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlockDestroySystem : DestroySystem
{
    protected override void PlusCountOnObjectDestroyed()
    {
        return;
    }
    protected override bool CheckPhaseNotDestroying()
    {
        return false;
    }
}
