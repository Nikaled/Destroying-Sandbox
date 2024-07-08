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
    [SerializeField] GameObject ParkourWinBlock;
    [SerializeField] AvailablePlaceBlockChecker placeChecker;
    [SerializeField] AudioSource SoundSource;
    [SerializeField] AudioClip PlaceBlockSound;
    [SerializeField] AudioClip DeleteBlockSound;

    private Player player;
    public static BuildCellManager instance;

    private readonly string FirstBlock = "FirstBlockPlaced";

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = Player.instance;
    }
    public void SetButtonsToBuildMode()
    {
        CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
        CanvasManager.instance.DoButton.onClick.AddListener(delegate { PlaceBlock(); });
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { DeleteBlock(); });
    }
    private void PlaceBlock()
    {
        if(currentCell != null)
        {
            Vector3 pos = currentCell.GetPositionToPlace();
            StartCoroutine(waitPlaceCheckerCallback());
          
        }
        IEnumerator waitPlaceCheckerCallback()
        {
            Vector3 pos = currentCell.GetPositionToPlace();
            var Checker =  Instantiate(placeChecker, pos, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
            if (Checker.PlayerInCell == false && Checker.BlockInCell == false)
            {
                PlaceLogic();
            }
            else
            {
                Debug.Log("Игрок или другой блок в клетке для установки!");
            }
            Destroy(Checker.gameObject);
            void PlaceLogic()
            {
                PlayPlaceBlockSound();
                Block newBlock = Instantiate(player.CurrentBlock, pos, Quaternion.identity);
                newBlock.AddBlockToSaveList();
                
                if (Geekplay.Instance.PlayerData.IsFirstBlockPlaced == false)
                {
                    Geekplay.Instance.PlayerData.IsFirstBlockPlaced = true;
                    Geekplay.Instance.Save();
                    Analytics.instance.SendEvent(FirstBlock);
                }
                    OnBlockPlaced();
            }
        }
    }

    protected virtual  void OnBlockPlaced()
    {

    }
public void PlayDeleteBlockSound()
    {
        SoundSource.clip = DeleteBlockSound;
        SoundSource.Play();
    }
    public void PlayPlaceBlockSound()
    {
        SoundSource.clip = PlaceBlockSound;
        SoundSource.Play();
    }
    private void DeleteBlock()
    {
        if(currentCell != null)
        {
            if (currentCell.parentBlock.CompareTag("Undestructable") == false)
            {
                PlayDeleteBlockSound();
                currentCell.parentBlock.DeleteBlock();
            }
        }
    }
    public void BuildUpdate()
    {
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 26, AbleToBuildMask))
        {
            if(currentCell != null)
            {
                currentCell.ShowCellMesh(false);
            }
            CrosshairWorldPosition = raycastHit.point;
            currentCell = raycastHit.collider.gameObject.GetComponent<BuildCellSide>();
            if(currentCell != null)
            {
            currentCell.ShowCellMesh(true);
            }
        }
        else
        {
            if (currentCell != null)
            {
                CrosshairWorldPosition = ray.GetPoint(19);
                currentCell.ShowCellMesh(false);
            }
            currentCell = null;
        }
        if (currentCell != null)
        {
            if (currentCell.GetComponent<BuildCellSide>() != null)
            {
                if (player.CurrentBlock != null)
                {
                    if(Geekplay.Instance.mobile == false)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            PlaceBlock();
                        }
                    }
                 
                }
                if (Geekplay.Instance.mobile == false)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteBlock();
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

    public void DisableBlockOutline()
    {
        if(currentCell != null)
        {
            currentCell.ShowCellMesh(false);
        }
    }
}
