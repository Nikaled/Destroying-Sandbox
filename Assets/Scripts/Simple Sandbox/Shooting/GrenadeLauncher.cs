using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrenadeLauncher : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject Projectile;
    public float LaunchSpeed = 111f;

    [Header("Trajectory Display")]
    public LineRenderer lineRenderer;
    public int linePoints = 500;
    public float timeIntervalinPoints = 0.01f;

    private Vector3 crossPosition;
    private Vector3 aimDirection;

    private Vector3 EndAimingVelocity;
    public float GrenadeMass = 1;
    [SerializeField] Camera playerCamera;
    public void GrenadeInput()
    {
        crossPosition = PlayerShooting.instance.CrosshairWorldPosition;
        aimDirection = (crossPosition - LaunchPoint.position).normalized;

        Debug.Log("crossPosition:" + crossPosition);
        if (lineRenderer != null)
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        Player.instance.RotatePlayerOnShoot(aimDirection);
    }
    private void Update()
    {
        if(Player.instance.currentState == Player.PlayerState.AimingGrenade)
        {
        DrawTrajectory();
        }
    }
    public void LaunchGrenade()
    {
        Debug.Log("Launch grenade");
        var _projectile = Instantiate(Projectile, LaunchPoint.transform.position, LaunchPoint.transform.rotation);
        //_projectile.GetComponent<Rigidbody>().velocity = LaunchSpeed * LaunchPoint.up;
        //_projectile.GetComponent<Rigidbody>().velocity = LaunchSpeed * playerCamera.transform.forward + (Vector3.up * 10);
        _projectile.GetComponent<Rigidbody>().velocity = EndAimingVelocity;
        _projectile.GetComponent<CapsuleCollider>().enabled = true;
        _projectile.GetComponent<Rigidbody>().useGravity = true;
        if (_projectile.GetComponent<Grenade>() != null)
        {
        _projectile.GetComponent<Grenade>().OnLaunch();
        }
        lineRenderer.enabled = false;
        Player.instance.RotatePlayerOnShoot(aimDirection);
        PlayerShooting.instance.FireAudioSource.clip = PlayerShooting.instance.GrenadeThrowSound;
        PlayerShooting.instance.FireAudioSource.Play();
    }
    public void ClearTrajectory()
    {
        lineRenderer.enabled = false;
    }
    public void DrawTrajectory()
    {
        aimDirection = PlayerShooting.instance.AimDirection;
        lineRenderer.positionCount = 0;
        Vector3 origin = LaunchPoint.position;
        //Vector3 startVelocity = LaunchSpeed * Camera.main.transform.forward;
        //Vector3 startVelocity = LaunchSpeed * Camera.main.transform.forward + (Vector3.up * 10);
        Vector3 startVelocity = Vector3.forward;
        if (Geekplay.Instance.mobile)
        {
           startVelocity = LaunchSpeed * aimDirection+(Vector3)SwipeDetector.instance.swipeDelta -new Vector3(0, 0.5f, 0) /*+ (Vector3.up * 10)*/;
        }
        else
        {
            startVelocity = LaunchSpeed * Camera.main.transform.forward + (Vector3.up * 10);
        }
        EndAimingVelocity = startVelocity;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var z = (startVelocity.z * time) + (Physics.gravity.z / 2 * time * time);
            Vector3 point = new Vector3(x, y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
