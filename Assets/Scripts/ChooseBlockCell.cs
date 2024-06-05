using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBlockCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;

    public void ChooseBlock()
    {
        Player.instance.ChooseNewCurrentBlock(blockPrefab);
    }
}
