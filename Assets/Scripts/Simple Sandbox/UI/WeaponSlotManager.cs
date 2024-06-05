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
        OnBlockSwitched(1);
        Player.instance.SwitchedBlock += OnBlockSwitched;
    }
    private void OnEnable()
    {
        if (Geekplay.Instance != null)
            Player.instance.SwitchedBlock += OnBlockSwitched;
    }

    private void OnDisable()
    {
        Player.instance.SwitchedBlock -= OnBlockSwitched;
    }
    private void OnBlockSwitched(int PressedNumber)
    {
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i].BlockIsInactive();
        }
        int ActiveWeaponIndex = PressedNumber - 1;
        WeaponSlots[ActiveWeaponIndex].BlockIsActive();
    }
}
