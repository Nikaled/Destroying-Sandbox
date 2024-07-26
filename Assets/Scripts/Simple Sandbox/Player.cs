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
    public  Action<int> SwitchedBlock;
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

    public KeyCode DeletingModeButton = KeyCode.N;
    private KeyCode BuildingModeButton = KeyCode.I;
    public KeyCode RotatingModeButton = KeyCode.M;
    public static Player instance;
    public bool IsFirstView;
    public bool AdWarningActive;

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
    private void SwitchView()
    {
        IsFirstView = !IsFirstView;
        GoToNormalCamera();
    }
    public void SwitchCamera()
    {
        examplePlayer.SwitchCamera();
        SwitchView();
    }
    private void GoToNormalCamera()
    {
        if (IsFirstView == false)
        {
            normalCamera.FollowPointFraming = new Vector2(1.8f, 1.8f);
            normalCamera.Camera.fieldOfView = 55;
        }
        else
        {
            normalCamera.FollowPointFraming = new Vector2(0, 0);
            normalCamera.Camera.fieldOfView = 40;
        }
        //playerShooting.lineRenderer.enabled = false;

    }
    private void Awake()
    {
        instance = this;
        CurrentBlock = BlocksInSlots[0];
        CurrentBlockIndex = 0;
        CurrentWeaponIndex = 1;
    }
    protected virtual void Start()
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
          
        }
        if (SerializeBlockManager.instance.OnlyDestroyingMap)
        {
            //SwitchPlayerState(PlayerState.Idle);
        }
        SetFlyMode();
    }
    public void MapSetup()
    {
        SwitchPlayerState(PlayerState.Building);
        if (SerializeBlockManager.instance.OnlyDestroyingMap)
        {
            SwitchPlayerState(PlayerState.Idle);
            if (SerializeBlockManager.instance.OnlyDestroyingMap)
            {
                CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(false);
            }
        }
        else if (SerializeBlockManager.instance.OnlyParkourMap)
        {

            ParkourManager.instance.StartParkourOnLoad();
            SwitchPlayerState(PlayerState.Parkour);
            parkourStartPosition = transform.position;
            parkourStartRotation = transform.rotation;
            CycleManager.instance.ParkourPhaseStarted += OnParkourPhaseStarted;
            if (Geekplay.Instance.PlayerData.IsParkourSpeedUpForReward)
            {
                Geekplay.Instance.PlayerData.IsParkourSpeedUpForReward = false;
                SpeedUpPlayer();
            }
        }
    }
    private void SpeedUpPlayer()
    {
        characterController.MaxStableMoveSpeed = 20;
        characterController.MaxAirMoveSpeed = 30;
        //characterController.JumpUpSpeed = 20;
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
    public virtual void SwitchPlayerState(PlayerState newPlayerState, float Delay = 0.1f)
    {
        switch (newPlayerState)
        {
            case PlayerState.Idle:
                
                animator.SetBool("PistolAiming", false);
                CanvasManager.instance.ShowBuildingInstruction(false);
                break;
            case PlayerState.Building:
                HideAllWeapons();
                if (Geekplay.Instance.mobile)
                {
                    BuildCellManager.instance.SetButtonsToBuildMode();
                }
                else
                {
                    CanvasManager.instance.ShowCurrentWeaponInstruction(0, HideAll: true);
                    CanvasManager.instance.ShowBuildingInstruction(true);
                }
                break;
            case PlayerState.Parkour:
                CanvasManager.instance.ShowBlockSlotsAndHideWeapons(false);
                CanvasManager.instance.ShowWeaponSlotsAndHideBlocks(false);
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.DoButton.gameObject.SetActive(false);
                    CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
                    CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(false);
                }
                else
                {
                    CanvasManager.instance.ShowBuildingInstruction(false);
                    CanvasManager.instance.ShowCurrentWeaponInstruction(0, HideAll: true);
                }
                break;

        }
        if (currentState == PlayerState.Building && newPlayerState == PlayerState.Idle)
        {
            CanvasManager.instance.ShowWeaponSlotsAndHideBlocks(true);
        }
        if (currentState == PlayerState.Idle && newPlayerState == PlayerState.Building)
        {
            CanvasManager.instance.ShowBlockSlotsAndHideWeapons(true);
            WeaponSelector.instance.HideAllWeapons();
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
        BuildCellManager.instance.DisableBlockOutline();
        currentState = newPlayerState;
        Debug.Log("Player State:" + currentState);
    }
    private IEnumerator DelaySwitchState(Player.PlayerState newPlayerState, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        AfterSwitchState(newPlayerState);
    }
    public  virtual void SwitchActiveBlockSlot(int PressedNumber)
    {
        CurrentBlockIndex = PressedNumber - 1;
        CurrentBlock = BlocksInSlots[CurrentBlockIndex];
        SwitchedBlock?.Invoke(PressedNumber);
    }
    public void SwitchActiveWeaponSlot(int PressedNumber)
    {
        if (InterfaceActive)
        {
            return;
        }
        SwitchedWeapon?.Invoke(PressedNumber);
        SwitchWeapon(PressedNumber);
    }
    public void OnDestroyingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Idle, 0);
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { MobileFireInput(); });
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        }
        SwitchWeapon(CurrentWeaponIndex);
    }
    public void OnBuildingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Building, 0);
    }
    private void FixedUpdate()
    {
        if (/*animationPlayer.IsMoving == false &&*/  IsFirstView)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
        if (currentState == PlayerState.AimingGrenade)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
    }
    protected virtual void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchView();
        }
#endif
        if (AdWarningActive)
        {
            return;
        }
        if (currentState == PlayerState.Parkour)
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
                    ChangeActiveBlockInput();
                    return;
                }
                BuildCellManager.instance.BuildUpdate();
                break;
        }
        if (InterfaceActive)
        {
            ChangeWeaponInput();
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
        if (currentState == PlayerState.Building || currentState == PlayerState.InBuildingMenu)
        {
            ChangeActiveBlockInput();
        }
        if (currentState == PlayerState.Building)
        {
            if (Input.GetKeyDown(BuildingModeButton))
            {
                ActivateBuildingMenu(true);
            }
        }

    }
    public virtual void ChangeWeaponInput()
    {
        if(currentState != PlayerState.Idle)
        {
            return;
        }
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
        DestroyLimiter.ResetCurrentDestroyed();
        DestroyLimiter.IsWeaponWithLimit = false;
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
            CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = false;
        }
        else
        {
            CanvasManager.instance.ShowCurrentWeaponInstruction(PressedNumber - 1);
        }
        CanvasManager.instance.ShowUnlockWeaponSlotUI(false);

        if (WeaponSelector.instance.IsWeaponAvailable(PressedNumber) == false)
        {
            SwitchedWeapon?.Invoke(PressedNumber);
            return;
        }
        else
        {
            if (InterfaceActive == false)
            {
                examplePlayer.LockCursor(true);
            }
        }
        HideAllWeapons();

        WeaponSelector.instance.HideAllWeapons();

        if (CurrentWeapon == WeaponType.Grenade)
        {
            animator.SetBool("AimingGrenade", false);
            grenadeLauncher.ClearTrajectory();
        }
        if (CurrentWeapon == WeaponType.Plane)
        {
            if(InterfaceActive == false)
            {
            examplePlayer.LockCursor(true);
            }
        }
        if(CurrentWeapon == WeaponType.FlameThrower)
        {
            Flamethrower.instance.IsFiring = false;
            examplePlayer.MyLockOnShoot = false;
            playerShooting.EndFire(WeaponType.FlameThrower);
        }
        switch (PressedNumber)
        {

            case 1:
                CurrentWeapon = WeaponType.Pistol;
                PistolModel.SetActive(true);
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.DoButton.onClick.AddListener(delegate { MobileFireInput(); });
                }
                break;
            case 2:
                CurrentWeapon = WeaponType.Grenade;
                GrenadeModel.SetActive(true);
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
                }
                break;
            case 3:
                CurrentWeapon = WeaponType.FlameThrower;
                FlameThrowerModel.SetActive(true);
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
                }
                break;
            case 4:
                CurrentWeapon = WeaponType.Plane;
                WeaponSelector.instance.SelectWeapon(5);
                break;
            case 5:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(0);
                DestroyLimiter.IsWeaponWithLimit = true;
                break;
            case 6:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(1);
                DestroyLimiter.IsWeaponWithLimit = true;
                break;
            case 7:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(4);

                break;
            case 8:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(2);
                DestroyLimiter.IsWeaponWithLimit = true;
                break;
            case 9:
                CurrentWeapon = WeaponType.None;
                WeaponSelector.instance.SelectWeapon(6);
                DestroyLimiter.IsWeaponWithLimit = true;
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
        if (currentState != PlayerState.Idle)
        {
            SwitchPlayerState(PlayerState.Idle);
        }
    }
    public  virtual void ActivateBuildingMenu(bool Is)
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
        if (CurrentWeapon == WeaponType.Gun)
        {
            animator.SetTrigger("GunFire");
            playerShooting.Fire(CurrentWeapon);
            examplePlayer.MyLockOnShoot = true;
            motor.SetPosition(transform.position);
        }
        if (CurrentWeapon == WeaponType.FlameThrower)
        {
            if (motor.GroundingStatus.IsStableOnGround)
            {
                motor.SetPosition(transform.position);
            }
        }
        //if (CurrentWeapon == WeaponType.Grenade)
        //{
        //    AimingGrenadeOnMobile();
        //}
    } // Used by Fire Button
    public void FireFlameThrower()
    {
        examplePlayer.MyLockOnShoot = true;
        animator.SetTrigger("GunFire");
        RotatePlayerOnShoot(playerShooting.AimDirection);
    }
#endregion
    private void FireInput()
    {
        if (CurrentWeapon == WeaponType.FlameThrower)
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
        Debug.Log("Aim direction:" + aimDirection);
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        Quaternion OnlyY = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
        Debug.Log("Rotate char to:" + OnlyY);
        motor.RotateCharacter(OnlyY);
    }
}
