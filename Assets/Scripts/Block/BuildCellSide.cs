using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCellSide : MonoBehaviour
{
    public bool IsOccupied;
    public Block parentBlock;
    public Vector3 offsetForNewBlock;
    [SerializeField] MeshRenderer CellSideMesh;
    public Vector3 GetPositionToPlace(float BuildingSize = 2)
    {
        offsetForNewBlock = (this.transform.position-parentBlock.transform.position).normalized;
        Debug.Log("OffsetIs:"+offsetForNewBlock);
        return parentBlock.transform.position + offsetForNewBlock * BuildingSize;
    }
    public void ShowCellMesh(bool Is)
    {
        if(CellSideMesh!=null)
        CellSideMesh.enabled = Is;
    }
}
