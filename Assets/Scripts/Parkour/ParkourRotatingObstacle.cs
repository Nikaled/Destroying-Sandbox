using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourRotatingObstacle : MonoBehaviour, IMoveableParkour
{
    public Vector3 rotatingOrientationAndSpeed;
    public float Speed = 1;

    public bool IsFrozen { get ; set ; }

    public void Freeze(bool Is)
    {
    }

    public SaveParkourBlockData GetData()
    {
        var newData = new SaveParkourBlockData();
        newData.Speed = Speed;
        return newData;
    }

    public void SetData(float Speed, bool InvertMoveCycle = false)
    {
        this.Speed = Speed;
        if (this.Speed == 0)
        {
            this.Speed = 1;
        }
    }

    private void FixedUpdate()
    {
        if(!IsFrozen)
        transform.Rotate(rotatingOrientationAndSpeed* Speed);
    }
}
