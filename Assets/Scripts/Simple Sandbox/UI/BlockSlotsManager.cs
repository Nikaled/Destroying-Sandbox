using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlotsManager : MonoBehaviour
{
    [SerializeField] BlockSlot[] BlockSlots;
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
        BlockSlots[Index].ChangeSpriteInSlot(blockSprite);
    }
    private void OnDisable()
    {
        if (Player.instance != null)
        {
            Player.instance.SwitchedBlock -= OnBlockSwitched;
            Player.instance.SwitchedBlockFromShop -= ChangeBlockInSlotSprite;
        }
        
    }
    private void OnBlockSwitched(int PressedNumber)
    {
        for (int i = 0; i < BlockSlots.Length; i++)
        {
            BlockSlots[i].BlockIsInactive();
        }
        int ActiveWeaponIndex = PressedNumber - 1;
        BlockSlots[ActiveWeaponIndex].BlockIsActive();
    }
}
