using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] WeaponSlot[] WeaponSlots;
    private void Awake()
    {

    }
    private void Start()
    {
        OnWeaponSwitched(1);
        Player.instance.SwitchedBlock += OnWeaponSwitched;
    }
    private void OnEnable()
    {
        if (Player.instance != null)
        {
            Player.instance.SwitchedBlock += OnWeaponSwitched;
        }

    }
    private void OnDisable()
    {
        Player.instance.SwitchedBlock -= OnWeaponSwitched;
    }
    private void OnWeaponSwitched(int PressedNumber)
    {
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i].WeaponIsInactive();
        }
        int ActiveWeaponIndex = PressedNumber - 1;
        WeaponSlots[ActiveWeaponIndex].WeaponIsActive();
    }
}