using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailablePlaceBlockChecker : MonoBehaviour
{
    public bool PlayerInCell;
    public bool BlockInCell;
    public bool InMapBorder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInCell = true;
        }
        else
        {
            PlayerInCell = false;
        }
        if(other.gameObject.layer == 6)
        {
            BlockInCell = true;
        }
        if(other.gameObject.layer == 14)
        {
            InMapBorder = true;
            Debug.Log("Block In Border");
        }
    }
}
