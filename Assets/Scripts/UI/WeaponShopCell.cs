using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponShopCell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Button BuyButton;
    [SerializeField] Button InAppBuyButton;
    [SerializeField] Image CoinsImage;
    [SerializeField] Image BoughtMarkImage;
    [SerializeField] string InAppName;
    private int _price;
    private int WeaponSlotIndex;
    private string BoughtText;
    private bool IsBought;
    [SerializeField] int WeaponPrice;
    private void LocalizationBought()
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
    private void OnEnable()
    {
        LocalizationBought();
        if (IsBought == false)
        {
            PriceText.text = _price.ToString();
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
            InAppBuyButton.gameObject.SetActive(false);
        }
    }
    public void LoadBuyStatusPriceAndIndex(int price, bool IsBought, int WeaponIndex)
    {
        this.IsBought = IsBought;
        _price = WeaponPrice;
        if(IsBought== false)
        {
            PriceText.text = _price.ToString();
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
            InAppBuyButton.gameObject.SetActive(false);
        }
        WeaponSlotIndex = WeaponIndex;
    }

    public void BuyItem()
    {
        WeaponShop.instance.UnlockWeapon(_price, WeaponSlotIndex);
    }
    public void BuyItemInApp()
    {
        Geekplay.Instance.RealBuyItem(InAppName);
    }
}
