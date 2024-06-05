using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCellSide : MonoBehaviour
{
    public bool IsOccupied;
    public Block parentGrid;
    public Vector3 offsetForNewBlock;
    public Vector3 GetPositionToPlace(float BuildingSize = 2)
    {
        //GameObject NewCube = Instantiate(prefab, parentGrid.transform.position + offsetForNewBlock*BuildingSize, Quaternion.identity);
        return parentGrid.transform.position + offsetForNewBlock * BuildingSize;
    }
}
