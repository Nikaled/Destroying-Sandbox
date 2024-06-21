using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourRotatingObstacle : MonoBehaviour
{
    public Vector3 rotatingOrientationAndSpeed;
    private void Update()
    {
        transform.Rotate(rotatingOrientationAndSpeed);
    }
}
