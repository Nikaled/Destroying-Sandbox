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
    public event Action<int, Sprite> SwitchedWeaponFromShop;
    [SerializeField] public Animator animator;

    [SerializeField] public ExamplePlayer examplePlayer;
    [SerializeField] ExampleCharacterCamera normalCamera;

    [SerializeField] GameObject GunModel;
    [SerializeField] GameObject PistolModel;
    [SerializeField] GameObject KnifeModel;
    [SerializeField] GameObject GrenadeModel;
    [SerializeField] GameObject CharacterModel;
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
    public Weapon[] WeaponsInSlots;
    //public Weapon CurrentWeapon;
    public int CurrentWeaponIndex;
    public bool InterfaceActive;
    public enum PlayerState
    {
        InTransport,
        Aiming,
        Shooting,
        Idle,
        Sitting,
        Building,
        DeletingBuilding,
        RotatingBuilding,
        AimingGrenade,
        InBuildingMenu
    }
    float startTime;
    public enum WeaponType
    {
        Pistol,
        Gun,
        Knife,
        Hand,
        Grenade
    }

    private void Awake()
    {
        instance = this;
        CurrentBlock = BlocksInSlots[0];
        CurrentBlockIndex = 0;
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
            case PlayerState.Aiming:
                if (currentState != PlayerState.Aiming)
                {
                    animator.SetBool("PistolAiming", true);
                    GoToAimCamera();
                }
                break;
            case PlayerState.Idle:
                animator.SetBool("PistolAiming", false);
                break;

        }
        if (currentState == PlayerState.Aiming && newPlayerState != PlayerState.Aiming)
        {
            GoToNormalCamera();
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
    public void OnDestroyingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Idle);
    }
    public void OnBuildingPhaseActivated()
    {
        SwitchPlayerState(PlayerState.Building);
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
        if (currentState == PlayerState.Idle || currentState == PlayerState.Aiming)
        {
            if (Geekplay.Instance.mobile == false)
            {
                FireInput();
                ChangeActiveBlockInput();
                ChangeWeaponInput();
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    SwitchView();
                }
            }
        }
        if(currentState == PlayerState.Building || currentState == PlayerState.InBuildingMenu)
        {
            ChangeActiveBlockInput();
        }
     
        if (currentState == PlayerState.Aiming)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
            playerShooting.LockPlayerMovement(0.05f);
        }
        if(currentState == PlayerState.Idle || currentState == PlayerState.Building)
        {
            if (Input.GetKeyDown(BuildingModeButton))
            {
                ActivateBuildingMenu(true);
            }
        }
      
    }
    private void ChangeWeaponInput()
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
    }


    public void SwitchWeapon(int PressedNumber)
    {
        GunModel.SetActive(false);
        PistolModel.SetActive(false);
        KnifeModel.SetActive(false);
        GrenadeModel.SetActive(false);

        if (CurrentWeapon == WeaponType.Grenade)
        {
            animator.SetBool("AimingGrenade", false);
            grenadeLauncher.ClearTrajectory();
        }

        switch (PressedNumber)
        {
            case 4:
                CurrentWeapon = WeaponType.Gun;
                GunModel.SetActive(true);
                CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;

                break;
            case 3:
                CurrentWeapon = WeaponType.Pistol;
                PistolModel.SetActive(true);
                break;
            case 2:
                CurrentWeapon = WeaponType.Knife;
                KnifeModel.SetActive(true);
                break;
            case 1:
                CurrentWeapon = WeaponType.Hand;
                break;
            case 5:
                CurrentWeapon = WeaponType.Grenade;
                GrenadeModel.SetActive(true);
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
                break;

        }
        SwitchedWeapon?.Invoke(PressedNumber);
        SwitchPlayerState(PlayerState.Idle);
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
        if (CurrentWeapon == WeaponType.Knife)
        {

            animator.SetTrigger("Stab");
            playerShooting.HandAttack(WeaponType.Knife);
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
    public void MobileAiming()// Used by Aim Button
    {
        if (currentState != PlayerState.Aiming)
        {
            if (CurrentWeapon == WeaponType.Pistol || CurrentWeapon == WeaponType.Gun)
            {
                SwitchPlayerState(PlayerState.Aiming, 0);
                return;
            }
        }
        if (currentState == PlayerState.Aiming)
        {
            Debug.Log("Stop Aiming");
            SwitchPlayerState(PlayerState.Idle, 0);
        }
    }
    #endregion
    private void FireInput()
    {
        if (CurrentWeapon == WeaponType.Pistol)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("PistolFire");
                motor.SetPosition(transform.position);
                playerShooting.Fire(CurrentWeapon);
            }
        }
        if (CurrentWeapon == WeaponType.Knife)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Stab");

                playerShooting.HandAttack(WeaponType.Knife);
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
        if (currentState != PlayerState.Aiming)
        {
            if (CurrentWeapon == WeaponType.Pistol || CurrentWeapon == WeaponType.Gun)
            {
                if (Input.GetMouseButton(1))
                {

                    SwitchPlayerState(PlayerState.Aiming, 0);
                }
            }
        }
        if (currentState == PlayerState.Aiming)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("Stop Aiming");
                SwitchPlayerState(PlayerState.Idle, 0);
            }
        }

    }
    public void RotatePlayerOnShoot(Vector3 aimDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        Quaternion OnlyY = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
        motor.RotateCharacter(OnlyY);
    }
    private void GoToAimCamera()
    {
        if (IsFirstView)
        {
            normalCamera.Camera.fieldOfView = 20;
            normalCamera.FollowPointFraming = new Vector2(0f, 0f);
        }
        else
        {
            normalCamera.Camera.fieldOfView = 30;
        }
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
            normalCamera.FollowPointFraming = new Vector2(-0.2f, 0.69f);
            normalCamera.Camera.fieldOfView = 40;
        }
    }
}
