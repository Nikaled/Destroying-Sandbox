using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperManager : MonoBehaviour
{
    public static CreeperManager instance;
    private Vector3 CashedPlayerPos;
    private void OnEnable()
    {
        Player.instance.CharacterModel.SetActive(false);
        Player.instance.CreeperModel.SetActive(true);
        CashedPlayerPos = Player.instance.transform.position;
    }
    private void OnDisable()
    {
        Player.instance.CreeperModel.SetActive(false);
        Player.instance.CharacterModel.SetActive(true);
        Player.instance.motor.SetPosition(CashedPlayerPos);
        Creeper.instance.DisableCollidersOnSwitch();
    }
    public static void OnCreeperExploded()
    {
        Player.instance.examplePlayer.MyLockOnShoot = false;
        Player.instance.SwitchWeapon(1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Creeper.instance.StartExplosion();
        }
    }
}
