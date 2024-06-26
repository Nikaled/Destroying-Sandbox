using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChooseBlockCell : MonoBehaviour
{
    [field:SerializeField] public Block blockPrefab;

    [SerializeField] public Image blockSprite;
    public void ChooseBlock()
    {
        Player.instance.ChooseNewCurrentBlockFromShop(blockPrefab, blockSprite.sprite);
    }
}
