using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] GameObject PlanePrefab;
    GameObject currentPlane;
    private void OnEnable()
    {
        Vector3 PlaneUp = new Vector3(0, 50, 0);
        currentPlane = Instantiate(PlanePrefab, PlaneUp, Quaternion.identity);
        Player.instance.gameObject.SetActive(false);
        currentPlane.GetComponent<Plane>().planeManager = this;
    }
    private void OnDisable()
    {
        if(currentPlane != null)
        {
        Destroy(currentPlane);
        }
        Player.instance.gameObject.SetActive(true);
        //Player.instance.examplePlayer.LockCursor(true);
    }
    public void OnPlaneDestroyed()
    {
        Player.instance.SwitchWeapon(1);
    }
    void Update()
    {
        Player.instance.ChangeWeaponInput();
    }
}
