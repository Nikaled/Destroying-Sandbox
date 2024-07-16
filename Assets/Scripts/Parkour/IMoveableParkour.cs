using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableParkour
{
    public bool IsFrozen { get; set; }
    public void Freeze(bool Is);
}
