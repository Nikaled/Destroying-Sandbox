using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBlockCell : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;

    [SerializeField] private Image blockSprite;
    public void ChooseBlock()
    {
        Player.instance.ChooseNewCurrentBlockFromShop(blockPrefab, blockSprite.sprite);
    }
}
