using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CarEnterController : EnterController
{
    [SerializeField] private Transform FirstViewCameraTransform;
    [SerializeField] VehicleControl vehicleControl;
    [Header("Tank Only")]
    [SerializeField] TankShooting tankShooting;
    public bool IsTank;
    private void Start()
    {
        ActivateTransport();
    }
    protected void Update()
    {
        //base.Update();
        //if (Input.GetKeyDown(KeyCode.Q) && IsPlayerIn && Player.instance.AdWarningActive == false)
        //{
        //    TransportCamera.transform.position = FirstViewCameraTransform.position;
        //}
    }
    protected  void ActivateTransport()
    {
        CanvasManager.instance.ShowControlCarInstruction(true, IsTank);
        vehicleControl.gameObject.transform.position = new Vector3(vehicleControl.gameObject.transform.position.x, vehicleControl.gameObject.transform.position.y + 1f, vehicleControl.gameObject.transform.position.z);
        vehicleControl.enabled = true;
        vehicleControl.GetComponent<VehicleControl>().activeControl = true;
        vehicleControl.OnCarEnter();
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ShowCarMobileInstruction(true);
            vehicleControl.MyInitializeButtons();
            CarButtons.instance.ShootButton.gameObject.SetActive(false);
        }
        if (tankShooting != null)
        {
            tankShooting.enabled = true;
            tankShooting.ActivateTankShooting(true);
            if (Geekplay.Instance.mobile)
            {
                CarButtons.instance.ShootButton.onClick.AddListener(delegate { tankShooting.Fire(); });
                CarButtons.instance.ShootButton.gameObject.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        CanvasManager.instance.ShowControlCarInstruction(false, IsTank);
        if (Geekplay.Instance.mobile)
        {
            vehicleControl.MyClearButtons();
            CanvasManager.instance.ShowCarMobileInstruction(false);
            CarButtons.instance.ShootButton.onClick.RemoveAllListeners();
            CarButtons.instance.ShootButton.gameObject.SetActive(false);
        }
    }
    protected  void DeactivateTransport()
    {
        CanvasManager.instance.ShowControlCarInstruction(false, IsTank);
        vehicleControl.carSetting.brakePower = float.MaxValue;
        vehicleControl.GetComponent<VehicleControl>().activeControl = false;
        vehicleControl.GetComponent<VehicleControl>().OnCarQuit();
        if (Geekplay.Instance.mobile)
        {
            vehicleControl.MyClearButtons();
            CanvasManager.instance.ShowCarMobileInstruction(false);
            CarButtons.instance.ShootButton.gameObject.SetActive(false);
        }
        if (tankShooting != null)
        {
            tankShooting.ActivateTankShooting(false);
            tankShooting.enabled = false;
            if (Geekplay.Instance.mobile)
            {
                CarButtons.instance.ShootButton.onClick.RemoveAllListeners();
            }
        }
    }
}
