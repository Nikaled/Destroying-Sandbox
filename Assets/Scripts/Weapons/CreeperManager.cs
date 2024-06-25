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
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { ExplodeCreeper(); });
        }
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            ExplodeCreeper();
        }
    }
    private void ExplodeCreeper()
    {
        Creeper.instance.StartExplosion();
    }
}
