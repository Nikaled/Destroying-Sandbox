using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveParkourBlockData
{
    public Quaternion rotation;
    public Vector3 Scale;
    public int BlockOnSceneIndex;
    public float Speed;
    public bool InvertMoveCycle; // for Roaming block
    public SaveParkourBlockData(Quaternion rotation, Vector3 Scale)
    {
        this.rotation = rotation;
        this.Scale = Scale;
    }
    public SaveParkourBlockData() { }
}
