using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ForwardBlockCollider : MonoBehaviour
{
    [SerializeField] UnitMovement movement;
    [SerializeField] UperBlockChecker upChecker;
    private IEnumerator colDetect;
    List<GameObject> collidersWithTagUnit = new();
    private void OnTriggerEnter(Collider other)
    {
        if (movement.AwaitForGroundAfterPunch)
        {
            return;
        }
        //movement.RotateUnit();
        if (other.CompareTag("Unit"))
        {
            collidersWithTagUnit.Add(other.gameObject);
            if (movement.CurrentEnemy == null)
            {
            movement.RotateUnit();
            }
            return;
        }
        for (int i = 0; i < collidersWithTagUnit.Count; i++)
        {
            if(collidersWithTagUnit[i] != null)
            {
                return;
            }
        }
        collidersWithTagUnit.Clear();
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
    private void OnTriggerExit(Collider other)
    {
        collidersWithTagUnit.Remove(other.gameObject);
    }
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
