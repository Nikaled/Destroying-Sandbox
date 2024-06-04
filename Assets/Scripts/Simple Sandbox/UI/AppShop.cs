using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AppShop : MonoBehaviour
{
    public AppShopCell RewardCell;
    public AppShopCell[] ShopCells;
    public TextMeshProUGUI RewardText;
    public GameObject ConfirmUI;
    public static AppShop instance;

    private void Start()
    {
        instance = this;
        for (int i = 0; i < ShopCells.Length; i++)
        {
            ShopCells[i].SubscribeOnPurchase();
        }

        RewardCell.SubscribeOnReward();

    }
    public void ShowConfirmRewardWindow(int GoldCount)
    {
        RewardText.text = GoldCount.ToString();
        ConfirmUI.SetActive(true);
    }
}
