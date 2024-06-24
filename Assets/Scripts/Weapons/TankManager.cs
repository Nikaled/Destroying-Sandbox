using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField] GameObject TankPrefab;
    GameObject currentTank;
    private void OnEnable()
    {
        Vector3 TankUp = new Vector3(0, 2, 0);
        currentTank = Instantiate(TankPrefab, Player.instance.transform.position + TankUp, Player.instance.transform.rotation);
        Player.instance.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Destroy(currentTank);
        Player.instance.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Player.instance.AdWarningActive)
        {
            return;
        }
        Player.instance.ChangeWeaponInput();
    }
}
