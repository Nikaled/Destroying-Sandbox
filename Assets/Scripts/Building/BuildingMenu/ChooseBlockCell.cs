using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ChooseBlockCell : MonoBehaviour
{
    [field:SerializeField] public Block blockPrefab;

    [SerializeField] public Image blockSprite;
    //[SerializeField] TextMeshProUGUI PriceText;
    //[SerializeField] TextMeshProUGUI PriceImage; // Parent of Text
    //[SerializeField] int Price;
    //public bool IsNeedToBuy;
    public void ChooseBlock()
    {
        Player.instance.ChooseNewCurrentBlockFromShop(blockPrefab, blockSprite.sprite);
    }
    //private void ChangePriceUI()
    //{
    //    if (IsNeedToBuy)
    //    {
    //        PriceText.text = Price.ToString();
    //        PriceImage.enabled = true;
    //    }
    //    else
    //    {
    //        PriceImage.gameObject.SetActive(false);
    //    }
    //}
    //public void LoadStatus(bool NeedToBuyStatus)
    //{
    //    IsNeedToBuy = NeedToBuyStatus;
    //    ChangePriceUI();
    //}
    //public void TryBuyBlock()
    //{

    //}
}
