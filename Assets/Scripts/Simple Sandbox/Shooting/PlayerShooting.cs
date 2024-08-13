using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shoot parametres")]
    [SerializeField] private float normalSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform PistolProjectileSpawnPoint;
    [SerializeField] private Transform GunProjectileSpawnPoint;
    [SerializeField] private Transform RotatePlayerStartPoint;
    [SerializeField] ShootingProjectile projectile;
    [SerializeField] public Image Crosshair;
    [SerializeField] Player player;
    [SerializeField] MeleeAttackHitbox handHitbox;
    [SerializeField] Flamethrower flameThrower;
    public Vector3 AimDirection;
    public Vector3 RotateDirection;
    [HideInInspector] public Vector3 CrosshairWorldPosition;
    [HideInInspector] public Vector3 MouseWorldPosition;
    float GunTimer;
    float GunShootInterval = 0.05f;
    public static PlayerShooting instance;
    public IEnumerator HoldingCoroutine;
    //[SerializeField] public LineRenderer lineRenderer;
    public AudioSource FireAudioSource;
    [SerializeField] AudioClip PistolSound;
    [SerializeField] AudioClip FlamethrowSound;
    [SerializeField] public AudioClip GrenadeThrowSound;
    private void Start()
    {
        instance = this;
    }
    public bool Reloading { get; private set; }

    void Update()
    {
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
        {
            CrosshairWorldPosition = raycastHit.point;
        }
        else
        {
            CrosshairWorldPosition = ray.GetPoint(1998);
        }

        AimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;
        RotateDirection = (CrosshairWorldPosition - RotatePlayerStartPoint.position).normalized; 
    }
    public void Fire(Player.WeaponType currentWeapon)
    {
        if (currentWeapon == Player.WeaponType.Pistol)
        {
            Vector3 aimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;
            Vector3 rotateDirection = (CrosshairWorldPosition - RotatePlayerStartPoint.position).normalized;
            player.RotatePlayerOnShoot(aimDirection);
            ShootingProjectile proj = Instantiate(projectile, PistolProjectileSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            LockPlayerMovement(0.7f);
            FireAudioSource.clip = PistolSound;
            FireAudioSource.Play();
        }

        if (currentWeapon == Player.WeaponType.Gun)
        {
            FireGun();
            LockPlayerMovement(0.4f);
        }
        if (currentWeapon == Player.WeaponType.FlameThrower)
        {
            flameThrower.StartFire();
            FireFlameThrowerSound();
        }
    }
    public void FireFlameThrowerSound()
    {
        FireAudioSource.clip = FlamethrowSound;
        FireAudioSource.timeSamples = 100;
        FireAudioSource.Play();
        FireAudioSource.loop = true;
    }
    public void EndFire(Player.WeaponType currentWeapon)
    {
        if (currentWeapon == Player.WeaponType.FlameThrower)
        {
            flameThrower.EndFire();
            player.examplePlayer.MyLockOnShoot = false;
            FireAudioSource.Pause();
            FireAudioSource.clip = null;
            FireAudioSource.loop = false;
        }
    }
    public void LockPlayerMovement(float HoldingTime = 1f)
    {
        if (HoldingCoroutine != null)
        {
            StopCoroutine(HoldingCoroutine);
        }
        HoldingCoroutine = null;
        HoldingCoroutine = player.LockPositionOnShoot(HoldingTime);
        StartCoroutine(HoldingCoroutine);
    }
    public void FireGun()
    {
        Vector3 aimDirection = (CrosshairWorldPosition - GunProjectileSpawnPoint.position).normalized;
        player.RotatePlayerOnShoot(aimDirection);
        if (Time.time - GunTimer > GunShootInterval)
        {
            Reloading = false;
        }
        if (Reloading == false)
        {
            Reloading = true;

            ShootingProjectile proj = Instantiate(projectile, GunProjectileSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            GunTimer = Time.time;
            FireAudioSource.Play();
        }

    }
    public void HandAttack(Player.WeaponType meleeWeapon)
    {
        Vector3 aimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;
        player.RotatePlayerOnShoot(aimDirection);
        //LockPlayerMovement();
        List<HpSystemCollision> targets = new();
        if (handHitbox.GetEnemies() != null)
        {
            targets.AddRange(handHitbox.GetEnemies());
        }
        int meleeDamage = 1;
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(meleeDamage);
            }
            FireAudioSource.Play();
        }
        else
        {

            FireAudioSource.Play();

        }

        handHitbox.EndAttack();
    }
}
