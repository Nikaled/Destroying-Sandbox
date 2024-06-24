using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] Transform[] BulletSpawnPoints;
    [SerializeField] ShootingProjectile bullet;
    float GunTimer;
    float GunShootInterval = 0.05f;
    private bool Reloading;
    public float Speed = 10;
    public float RotatingSpeed;
    [SerializeField] Transform rotor;
    public bool IsExploding;
    [SerializeField] DestroyEffect ExplosionAnimation;
    [SerializeField] GameObject ParentObject;
    [SerializeField] DamageArea DestroyArea;
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion = 0.6f;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer[] Meshes;
    public PlaneManager planeManager;
    public Joystick joystick;
    private void OnEnable()
    {
        LockCursor(true);
        if (Geekplay.Instance.mobile)
        {
        joystick = Player.instance.examplePlayer.FixedJoystick;
            CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
            CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().OnHolding += Fire;
        }
    }
    private void OnDisable()
    {
        LockCursor(false);
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().OnHolding -= Fire;
        }
    }
    private void FixedUpdate()
    {
        if (Player.instance.InterfaceActive || Player.instance.AdWarningActive)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = transform.forward * Speed;
    }
    private void Update()
    {
        if(Player.instance.InterfaceActive || Player.instance.AdWarningActive)
        {
            return;
        }
        if (IsExploding)
        {
            return;
        }
        float YInput = 0;
        float XInput = 0;
        if (Geekplay.Instance.mobile == false)
        {
             YInput = -Input.GetAxis("Mouse Y");
             XInput = Input.GetAxis("Mouse X");
        }
        else
        {
            YInput = -joystick.Vertical;
            XInput = joystick.Horizontal;
        }
        //transform.Translate(Vector3.forward*Time.deltaTime* Speed);
       
        transform.Rotate(new Vector3(YInput, XInput, 0) * Time.deltaTime * RotatingSpeed);
        //rb.rotation = transform.rotation;
        if(Geekplay.Instance.mobile == false)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
            }
        }
       
        RotateRotors();
    }
    public void Fire()
    {
       
        if (Time.time - GunTimer > GunShootInterval)
        {
            Reloading = false;
        }
        if (Reloading == false)
        {
            Reloading = true;

            for (int i = 0; i < BulletSpawnPoints.Length; i++)
            {
                Instantiate(bullet, BulletSpawnPoints[i].transform.position, BulletSpawnPoints[i].transform.rotation);
            }
            GunTimer = Time.time;
            //FireAudioSource.clip = GunSound;
            //FireAudioSource.Play();
        }
    }
    private void LockCursor(bool Is)
    {
        if (Geekplay.Instance != null)
        {
            if (Geekplay.Instance.mobile)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
        }
        if (Is)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
    private void RotateRotors()
    {
        if (rotor != null)
            rotor.Rotate(Vector3.forward * 10);
    }
    private IEnumerator Explosion()
    {
        IsExploding = true;
        DestroyArea.GetComponent<SphereCollider>().enabled = true;
        explosionForceChecker.transform.parent = null;
        yield return new WaitForSeconds(DelayBeforeExplosion);
        //Source.PlayExplosionSound();
        ExplosionAnimation.enabled = true;
        ExplosionAnimation.ShowEffectAndDestroyAfterDelay();

        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        targetsInExplosion = DestroyArea.targetsInExplosion;
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
            {
                targetsInExplosion[i].TakeDamage(targetsInExplosion[i].transform.position + new Vector3(0, 2, 0));
            }
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(explosionForceChecker.gameObject);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < Meshes.Length; i++)
        {
            Meshes[i].enabled = false;
        }
        yield return new WaitForSeconds(1);
        planeManager.OnPlaneDestroyed();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(IsExploding == false)
        StartCoroutine(Explosion());
        //var DestrCol = collision.gameObject.GetComponent<DestroyCollision>();
        //if (DestrCol != null)
        //{
        //    DestrCol.TakeDamage(transform.position);
        //    StartCoroutine(Explosion());
        //}
    }
}
