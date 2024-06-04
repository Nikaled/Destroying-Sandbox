using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCellManager : MonoBehaviour
{
    private Vector3 CrosshairWorldPosition;
    public Image Crosshair;
    public LayerMask AbleToBuildMask;
    private GameObject currentCell;
    [SerializeField] private GameObject BlockPrefab;
    void Update()
    {
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, AbleToBuildMask))
        {
            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject;
            Debug.Log(currentCell.name);
        }
        else
        {
            CrosshairWorldPosition = ray.GetPoint(1998);
            currentCell = null;
        }
        if(currentCell != null)
        {
            if(currentCell.GetComponent<BuildCellSide>() != null)
            {
                if(Input.GetKeyDown(KeyCode.B))
                {
                    Vector3 pos = currentCell.GetComponent<BuildCellSide>().GetPositionToPlace(BlockPrefab);
                    //Instantiate(BlockPrefab, pos, Quaternion.identity);
                }
            }
        }
    }
}
