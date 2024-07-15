using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourDevHelper : MonoBehaviour
{
    [SerializeField] Block[] ParkourBlockPrefabs;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < Player.instance.BlocksInSlots.Length; i++)
            {
                Player.instance.ChooseNewCurrentBlockFromShop(ParkourBlockPrefabs[i], null);
                Player.instance.SwitchActiveBlockSlot(i + 1);
            }
        }
    }
}
