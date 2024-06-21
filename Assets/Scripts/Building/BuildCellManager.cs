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
    [SerializeField] GameObject ParkourWinBlock;

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
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 26, AbleToBuildMask))
        {

            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject.GetComponent<BuildCellSide>();
        }
        else
        {
            if (currentCell != null)
            {
                CrosshairWorldPosition = ray.GetPoint(19);
            }
            currentCell = null;
        }
        if (currentCell != null)
        {
            if (currentCell.GetComponent<BuildCellSide>() != null)
            {
                if (player.CurrentBlock != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 pos = currentCell.GetPositionToPlace();
                        Block newBlock = Instantiate(player.CurrentBlock, pos, Quaternion.identity);
                        newBlock.AddBlockToSaveList();
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (currentCell.parentBlock.CompareTag("Undestructable") == false)
                    {
                        currentCell.parentBlock.DeleteBlock();
                    }
                }
#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Vector3 pos = currentCell.GetPositionToPlace();
                    GameObject WinParkourBlock = Instantiate(ParkourWinBlock, pos, Quaternion.identity);
                    WinParkourBlock.GetComponent<Block>().AddBlockToSaveList();
                }

#endif

            }
        }
    }
}
