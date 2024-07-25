using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableParkour
{
    public bool IsFrozen { get; set; }
    public void Freeze(bool Is);

    public void SetData(float Speed, bool InvertMoveCycle = false);

    public SaveParkourBlockData GetData();
}
