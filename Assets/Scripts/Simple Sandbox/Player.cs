using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;
using KinematicCharacterController;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject PlayerParent;
    [SerializeField] public KinematicCharacterMotor motor;
    [SerializeField] public ExampleCharacterController characterController;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] public AnimationPlayer animationPlayer;
    [SerializeField] GrenadeLauncher grenadeLauncher;
    public PlayerState currentState = PlayerState.Idle;
    public WeaponType CurrentWeapon;
    public event Action PistolFire;
    public event Action<int> SwitchedBlock;
    public event Action<int> SwitchedWeapon;
    public event Action<int, Sprite> SwitchedBlockFromShop;
    [SerializeField] public Animator animator;

    [SerializeField] public ExamplePlayer examplePlayer;
    [SerializeField] ExampleCharacterCamera normalCamera;

    [SerializeField] GameObject FlameThrowerModel;
    [SerializeField] GameObject PistolModel;
    [SerializeField] GameObject KnifeModel;
    [SerializeField] GameObject GrenadeModel;
    [SerializeField] public GameObject CharacterModel;
    [SerializeField] public GameObject CreeperModel;
    [SerializeField] public AudioSource SwapCitizenAudioSource;
    
    public KeyCode DeletingModeButton = KeyCode.N;
    public KeyCode BuildingModeButton = KeyCode.B;
    public KeyCode RotatingModeButton = KeyCode.M;
    public static Player instance;
    public bool IsFirstView;
    public bool AdWarningActive;

    public SkinnedMeshRenderer CurrentCitizenMesh;
    public SkinnedMeshRenderer[] PlayerMeshes;
    private IEnumerator lockOnShoot;
    public Block[] BlocksInSlots;
    public Block CurrentBlock;
    public int CurrentBlockIndex;
    public GameObject[] WeaponModelsInHand;
    public int CurrentWeaponIndex;
    public bool InterfaceActive;
    private Vector3 parkourStartPosition;
    private Quaternion parkourStartRotation;
    public enum PlayerState
    {
        InTransport,
        Shooting,
        Idle,
        Sitting,
        Building,
        AimingGrenade,
        InBuildingMenu,
        Parkour
    }
    float startTime;
    public enum WeaponType
    {
        Pistol,
        Gun,
        Plane,
        Hand,
        Grenade,
        FlameThrower,
        None
    }

    private void Awake()
    {
        instance = this;
        CurrentBlock = BlocksInSlots[0];
        CurrentBlockIndex = 0;
        CurrentWeaponIndex = 1;
    }
    private void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            examplePlayer.Mobile = true;
            examplePlayer.PC = false;
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { MobileFireInput(); });
            examplePlayer.LockCursor(false);
        }
        else
        {
            examplePlayer.Mobile = false;
            examplePlayer.PC = true;     
        }
        if (SerializeBlockManager.instance.OnlyParkourMap)
        {
            SwitchPlayerState(PlayerState.Parkour);
            parkourStartPosition = transform.position;
            parkourStartRotation = transform.rotation;
            CycleManager.instance.ParkourPhaseStarted += OnParkourPhaseStarted;
        }
        SetFlyMode();
    }
    private void OnParkourPhaseStarted()
    {
        motor.SetPositionAndRotation(parkourStartPosition, parkourStartRotation);
    }
    private void SetFlyMode()
    {
        //motor.AllowSteppingWithoutStableGrounding = true;
        //characterController.TransitionToState(CharacterState.Flying);
    }
    public void ChooseNewCurrentBlockFromShop(Block newBlockPrefab, Sprite BlockSprite)
    {
        CurrentBlock = newBlockPrefab;
        BlocksInSlots[CurrentBlockIndex] = CurrentBlock;
        SwitchedBlockFromShop?.Invoke(CurrentBlockIndex, BlockSprite);
    }
    public void SwitchPlayerState(PlayerState newPlayerState, float Delay = 0.1f)
    {
        switch (newPlayerState)
        {
            case PlayerState.Idle:
                animator.SetBool("PistolAiming", false);
                break;
            case PlayerState.Building:
                HideAllWeapons();
                break;
            case PlayerState.Parkour:
                CanvasManager.instance.ShowBlockSlotsAndHideWeapons(false);
                CanvasManager.instance.ShowWeaponSlotsAndHideBlocks(false);
                break;

        }
        if(currentState == PlayerState.Building && newPlayerState == PlayerState.Idle)
        {
            CanvasManager.instance.ShowWeaponSlotsAndHideBlocks(true);
        }
        if (currentState == PlayerState.Idle && newPlayerState == PlayerState.Building)
        {
            CanvasManager.instance.ShowBlockSlotsAndHideWeapons(true);
        }
        if (Delay > 0)
        {
        StartCoroutine(DelaySwitchState(newPlayerState, Delay));
        }
        else
        {
            AfterSwitchState(newPlayerState);
        }
    }
    private void AfterSwitchState(Player.PlayerState newPlayerState)
    {
        currentState = newPlayerState;
        Debug.Log("Player State:" + currentState);
    }
    private IEnumerator DelaySwitchState(Player.PlayerState newPlayerState, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        AfterSwitchState(newPlayerState);
    }
    public void SwitchActiveBlockSlot(int PressedNumber)
    {
        CurrentBlockIndex = PressedNumber - 1;
        CurrentBlock = BlocksInSlots[CurrentBlockIndex];
        SwitchedBlock?.Invoke(PressedNumber);
    }
    public void SwitchActiveWeaponSlot(int PressedNumber)
    {
        CurrentBlockIndex = PressedNumber - 1;
        CurrentBlock = BlocksInSlots[CurrentBlockIndex];
        SwitchedBlock?.Invoke(PressedNumber);
    }
    public void OnDestroyingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Idle);
        SwitchWeapon(CurrentWeaponIndex);
    }
    public void OnBuildingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Building,0);
    }
    private void FixedUpdate()
    {
        if (/*animationPlayer.IsMoving == false &&*/ currentState == PlayerState.Idle && IsFirstView)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
        if(currentState == PlayerState.AimingGrenade)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
    }
    private void Update()
    {
        if ( AdWarningActive)
        {
            return;
        }
        if(currentState == PlayerState.Parkour)
        {
            return;
        }
        switch (currentState)
        {
            case PlayerState.InBuildingMenu:
                ChangeActiveBlockInput();
                if (Input.GetKeyDown(BuildingModeButton))
                {
                    ActivateBuildingMenu(false);
                }
                break;
            case PlayerState.Building:
                if (InterfaceActive)
                {
                    return;
                }
                BuildCellManager.instance.BuildUpdate();
                break;
        }   
        if (InterfaceActive)
        {
            return;
        }
        if (currentState == PlayerState.Idle)
        {
            if (Geekplay.Instance.mobile == false)
            {
                FireInput();            
                ChangeWeaponInput();
            }
        }
        if(currentState == PlayerState.Building || currentState == PlayerState.InBuildingMenu)
        {
            ChangeActiveBlockInput();
        }
        if(currentState == PlayerState.Building)
        {
            if (Input.GetKeyDown(BuildingModeButton))
            {
                ActivateBuildingMenu(true);
            }
        }
      
    }
    public void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SwitchWeapon(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchWeapon(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SwitchWeapon(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchWeapon(10);
        }
    }

    private void HideAllWeapons()
    {
        for (int i = 0; i < WeaponModelsInHand.Length; i++)
        {
            WeaponModelsInHand[i].SetActive(false);
        }
    }
    public void SwitchWeapon(int PressedNumber)
    {
        HideAllWeapons();
        WeaponSelector.instance.HideAllWeapons();

        if (CurrentWeapon == WeaponType.Grenade)
        {
            animator.SetBool("AimingGrenade", false);
            grenadeLauncher.ClearTrajectory();
        }
        if(CurrentWeapon == WeaponType.Plane && InterfaceActive == false)
        {
            examplePlayer.LockCursor(true);
        }
        switch (PressedNumber)
        {

            case 1:
                CurrentWeapon = WeaponType.Pistol;
                PistolModel.SetActive(true);
                break;
            case 2:
                CurrentWeapon = WeaponType.Grenade;
                GrenadeModel.SetActive(true);
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;             
                break;
            case 3:
                CurrentWeapon = WeaponType.FlameThrower;
                FlameThrowerModel.SetActive(true);
                CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
                break;
            case 4:
                CurrentWeapon = WeaponType.Plane;
                WeaponSelector.instance.SelectWeapon(5);
                break;   
            case 5:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(0);
                break;
            case 6:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(1);
                break;
            case 7:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(4);
                break;
            case 8:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(2);
                break;
            case 9:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(6);
                break;
            case 10:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(3);
                break;
            default:
                CurrentWeapon = WeaponType.None;
                break;
        }

        CurrentWeaponIndex = PressedNumber;
        SwitchedWeapon?.Invoke(PressedNumber);
        if(currentState != PlayerState.Idle)
        {
        SwitchPlayerState(PlayerState.Idle);
        }
    }
        public void ActivateBuildingMenu(bool Is)
    {
        if (Is)
        {
            CanvasManager.instance.ShowBuildingMenu(true);
        }
        else
        {
            CanvasManager.instance.ShowBuildingMenu(false);
        }
    }
    private void ChangeActiveBlockInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveBlockSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveBlockSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveBlockSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchActiveBlockSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchActiveBlockSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchActiveBlockSlot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SwitchActiveBlockSlot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchActiveBlockSlot(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SwitchActiveBlockSlot(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchActiveBlockSlot(10);
        }
    }

    public IEnumerator LockPositionOnShoot(float HoldingTime = 1f)
    {
        examplePlayer.MyLockOnShoot = true;
        yield return new WaitForSeconds(HoldingTime);
        examplePlayer.MyLockOnShoot = false;
    }

    #region MobileFunctions
    public void AimingGrenadeOnMobile()
    {
        animator.SetBool("AimingGrenade", true);
        currentState = PlayerState.AimingGrenade;
        grenadeLauncher.GrenadeInput();
        examplePlayer.MyLockOnShoot = true;
    }
    public void LaunchGrenadeOnMobile()
    {
        animator.SetBool("AimingGrenade", false);
        grenadeLauncher.LaunchGrenade();
        currentState = PlayerState.Idle;
        examplePlayer.MyLockOnShoot = false;
    }
    public void MobileJump()
    {
        examplePlayer.JumpIsTrue();
    }
    public void MobileFireInput()
    {
        if (CurrentWeapon == WeaponType.Pistol)
        {

            animator.SetTrigger("PistolFire");
            motor.SetPosition(transform.position);
            playerShooting.Fire(CurrentWeapon);
        }
        if (CurrentWeapon == WeaponType.Hand)
        {
            animator.SetBool("IsRun", false);
            animator.SetTrigger("Punch");
            playerShooting.HandAttack(WeaponType.Hand);
        }
        if (CurrentWeapon == WeaponType.Gun)
        {
            animator.SetTrigger("GunFire");
            playerShooting.Fire(CurrentWeapon);
            examplePlayer.MyLockOnShoot = true;
            motor.SetPosition(transform.position);
        }
        //if (CurrentWeapon == WeaponType.Grenade)
        //{
        //    AimingGrenadeOnMobile();
        //}
    } // Used by Fire Button
    #endregion
    private void FireInput()
    {
        if(CurrentWeapon == WeaponType.FlameThrower)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerShooting.Fire(CurrentWeapon);
            }
            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("GunFire");
                if (motor.GroundingStatus.IsStableOnGround)
                {
                    motor.SetPosition(transform.position);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                playerShooting.EndFire(CurrentWeapon);
            }
        }
        if (CurrentWeapon == WeaponType.Pistol)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("PistolFire");
                motor.SetPosition(transform.position);
                playerShooting.Fire(CurrentWeapon);
            }
        }
        if (CurrentWeapon == WeaponType.Hand)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("IsRun", false);
                animator.SetTrigger("Punch");
                playerShooting.HandAttack(WeaponType.Hand);
            }
        }
        if (CurrentWeapon == WeaponType.Gun)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("GunFire");
                playerShooting.Fire(CurrentWeapon);
                if (motor.GroundingStatus.IsStableOnGround)
                {
                motor.SetPosition(transform.position);
                }
            }
        }
        if (CurrentWeapon == WeaponType.Grenade)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("AimingGrenade", true);
                //currentState = PlayerState.AimingGrenade;
            }
            if (Input.GetMouseButton(0))
            {
                grenadeLauncher.GrenadeInput();
                examplePlayer.MyLockOnShoot = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("AimingGrenade", false);
                grenadeLauncher.LaunchGrenade();
                currentState = PlayerState.Idle;
                examplePlayer.MyLockOnShoot = false;
            }
        }
    }
    public void RotatePlayerOnShoot(Vector3 aimDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        Quaternion OnlyY = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
        motor.RotateCharacter(OnlyY);
    }
}
