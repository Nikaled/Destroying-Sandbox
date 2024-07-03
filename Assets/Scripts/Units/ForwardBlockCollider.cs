using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ForwardBlockCollider : MonoBehaviour
{
    [SerializeField] UnitMovement movement;
    [SerializeField] UperBlockChecker upChecker;
    private IEnumerator colDetect;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Впереди блок");
        //movement.RotateUnit();
        if (other.CompareTag("Unit"))
        {
            movement.RotateUnit();
            return;
        }
        if (colDetect != null)
        {
            StopCoroutine(colDetect);
            upChecker.DeactivateCollider();
        }
        colDetect = waitForDetectCollision();
        StartCoroutine(colDetect);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Впереди блок");
    //    //movement.RotateUnit();
    //}
    private IEnumerator waitForDetectCollision()
    {
        upChecker.ActivateCollider();
        yield return new WaitForSeconds(0.05f);
        upChecker.DeactivateCollider();
        if(upChecker.collisions.Count == 0)
        {
            Debug.Log("Up on block");
            movement.UpOnBlock();
        }
        else
        {
            Debug.Log("Rotate unit");
            movement.RotateUnit();
        }
    }
}
