using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCellManager : MonoBehaviour
{
    private Vector3 CrosshairWorldPosition;
    public Image Crosshair;
    public LayerMask AbleToBuildMask;
    private BuildCellSide currentCell;
    [SerializeField] private GameObject BlockPrefab;
    private Player player;
    public static BuildCellManager instance;
    private void Start()
    {
        player = Player.instance;
        instance = this;
    }
    public void BuildUpdate()
    {
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, AbleToBuildMask))
        {
            if(currentCell != null)
            {
                currentCell.parentBlock.HideBlockOutline();
            }
            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject.GetComponent<BuildCellSide>();
            if(currentCell != null)
            {
            currentCell.parentBlock.ShowBlockOutline(); 
            }
            Debug.Log(currentCell.name);
        }
        else
        {
            if (currentCell != null)
            {
                currentCell.parentBlock.HideBlockOutline();
                CrosshairWorldPosition = ray.GetPoint(1998);
                currentCell = null;
            }
        }
        if (currentCell != null)
        {
            if (currentCell.GetComponent<BuildCellSide>() != null)
            {
                if (player.CurrentBlock != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 pos = currentCell.GetComponent<BuildCellSide>().GetPositionToPlace();
                        Instantiate(player.CurrentBlock, pos, Quaternion.identity);
                    }
                }
               
            }
        }
    }
    void Update()
    {
        
    }

}
