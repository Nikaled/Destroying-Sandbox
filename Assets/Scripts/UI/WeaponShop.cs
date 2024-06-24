using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponShop : MonoBehaviour
{
    public bool[] OpenedWeapons; // 5-9 is buyable;
    public static WeaponShop instance;
    public WeaponShopCell[] shopCells;
    public TextMeshProUGUI CoinsText;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Geekplay.Instance.PlayerData.CoinsChanged += ChangeCoinsText;
        Geekplay.Instance.PlayerData.Coins = 500;
        LoadWeaponInfo();
        for (int i = 0; i < shopCells.Length; i++)
        {
            int WeaponIndexInArray = i + 4;
            shopCells[i].LoadBuyStatusPriceAndIndex(550, OpenedWeapons[WeaponIndexInArray], WeaponIndexInArray);
        }
    }
    private void ChangeCoinsText(int CurrentCoins)
    {
        CoinsText.text = CurrentCoins.ToString();
    }
    public void LoadWeaponInfo()
    {
        Geekplay.Instance.PlayerData.WeaponOpenedArray = null;



        if (Geekplay.Instance.PlayerData.WeaponOpenedArray == null)
        {
            Geekplay.Instance.PlayerData.WeaponOpenedArray = new bool[10];
            for (int i = 0; i < 4; i++)
            {
                Geekplay.Instance.PlayerData.WeaponOpenedArray[i] = true;
            }
            Geekplay.Instance.PlayerData.WeaponOpenedArray[9] = true;
            Geekplay.Instance.Save();
        }
        else if (Geekplay.Instance.PlayerData.WeaponOpenedArray.Length < 9)
        {
            Geekplay.Instance.PlayerData.WeaponOpenedArray = new bool[10];
            for (int i = 0; i < 5; i++)
            {
                Geekplay.Instance.PlayerData.WeaponOpenedArray[i] = true;
            }
            Geekplay.Instance.PlayerData.WeaponOpenedArray[9] = true;
            Geekplay.Instance.Save();
        }
        OpenedWeapons = Geekplay.Instance.PlayerData.WeaponOpenedArray;
    }
    public void UnlockWeapon(int price, int currentWeaponIndex)
    {
        if(Geekplay.Instance.PlayerData.Coins >= price)
        {
            Geekplay.Instance.PlayerData.Coins -= price;
            Geekplay.Instance.PlayerData.WeaponOpenedArray[currentWeaponIndex] = true;
            Geekplay.Instance.Save();
            int CellIndex = currentWeaponIndex - 4;
            shopCells[CellIndex].LoadBuyStatusPriceAndIndex(550, OpenedWeapons[CellIndex], CellIndex);
        }
    }
}
