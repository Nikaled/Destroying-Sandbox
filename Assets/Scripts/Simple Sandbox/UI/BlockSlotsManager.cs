using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlotsManager : MonoBehaviour
{
    [SerializeField] BlockSlot[] WeaponSlots;
    private void Awake()
    {
        
    }
    private void Start()
    {
        OnBlockSwitched(1);
        Player.instance.SwitchedBlock += OnBlockSwitched;
        Player.instance.SwitchedBlockFromShop += ChangeBlockInSlotSprite;
    }
    private void OnEnable()
    {
        if (Player.instance != null)
        {
            Player.instance.SwitchedBlock += OnBlockSwitched;
            Player.instance.SwitchedBlockFromShop += ChangeBlockInSlotSprite;
        }

    }
    private void ChangeBlockInSlotSprite(int Index, Sprite blockSprite)
    {
        WeaponSlots[Index].ChangeSpriteInSlot(blockSprite);
    }
    private void OnDisable()
    {
        Player.instance.SwitchedBlock -= OnBlockSwitched;
        Player.instance.SwitchedBlockFromShop -= ChangeBlockInSlotSprite;
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
