using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponShopCell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Button BuyButton;
    [SerializeField] Image CoinsImage;
    [SerializeField] Image BoughtMarkImage;
    private int _price;
    private int WeaponSlotIndex;
    private string BoughtText;
    private void Awake()
    {
           
        if(Geekplay.Instance.language == "ru")
        {
            BoughtText = "Куплено";
        }
        if(Geekplay.Instance.language == "en")
        {
            BoughtText = "Bought";
        }
        if (Geekplay.Instance.language == "tr")
        {
            BoughtText = "Satın Alındı";
        }
    }
    public void LoadBuyStatusPriceAndIndex(int price, bool IsBought, int WeaponIndex)
    {
        _price = price;
        if(IsBought== false)
        {
            PriceText.text = price.ToString();
            BuyButton.enabled = true;
            CoinsImage.gameObject.SetActive(true);
            BoughtMarkImage.gameObject.SetActive(false);
        }
        else
        {
            PriceText.text = BoughtText;
            BuyButton.enabled = false;
            CoinsImage.gameObject.SetActive(false);
            BoughtMarkImage.gameObject.SetActive(true);
        }
        WeaponSlotIndex = WeaponIndex;
    }
    public void BuyItem()
    {
        WeaponShop.instance.UnlockWeapon(_price, WeaponSlotIndex);
    }
}
