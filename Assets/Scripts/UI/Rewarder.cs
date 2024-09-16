using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class Rewarder : MonoBehaviour
{

    public static Rewarder instance;
    [SerializeField] string RewardForGold = "GetGold";
    [SerializeField] string AppForGold1 = "AppForGold1";
    [SerializeField] string AppForGold2 = "AppForGold2";
    [SerializeField] string AppForGold3 = "AppForGold3";
    [SerializeField] string AppUnlockWeapon1 = "UnlockWeapon_Plane";
    [SerializeField] string AppUnlockWeapon2 = "UnlockWeapon_Car";
    [SerializeField] string AppUnlockWeapon3 = "UnlockWeapon_Press";
    [SerializeField] string AppUnlockWeapon4 = "UnlockWeapon_Dynamite";
    [SerializeField] string AppUnlockWeapon5 = "UnlockWeapon_Meteor";
    [SerializeField] string AppUnlockWeapon6 = "UnlockWeapon_Lightning";
    [SerializeField] string AppUnlockWeapon_Pack = "AppUnlockWeapon_Pack";
    [SerializeField] string AppUnlockWeapon_All = "AppUnlockWeapon_All";
    public int RewardForGoldGold = 50;
    public int PurchaseForGoldGold1 = 100;
    public int PurchaseForGoldGold2 = 200;
    public int PurchaseForGoldGold3 = 500;
    public int PurchaseForWeaponPack = 1000;
    [HideInInspector] public int currentUnlockWeaponIndex;
    public Action RewardShowed;
    public Action WeaponPackBought;
    public Action WeaponAllBought;
    Dictionary<string, int> OperationNameAndReward = new();
    public string[] WeaponInAppNames;
    public int[] WeaponGoldCost;
    public int[] WeaponYanCost;

    private void Awake()
    {
        WeaponInAppNames = new string[] { AppUnlockWeapon1, AppUnlockWeapon2, AppUnlockWeapon3, AppUnlockWeapon4, AppUnlockWeapon5, AppUnlockWeapon6 };
        WeaponGoldCost = new int[] { 500, 750, 1000, 1500, 2000, 3000 };
        WeaponYanCost = new int[] { 7, 11, 15, 20, 25, 35 };
        OperationNameAndReward.Add(RewardForGold, RewardForGoldGold);
        OperationNameAndReward.Add(AppForGold1, PurchaseForGoldGold1);
        OperationNameAndReward.Add(AppForGold2, PurchaseForGoldGold2);
        OperationNameAndReward.Add(AppForGold3, PurchaseForGoldGold3);
        OperationNameAndReward.Add(AppUnlockWeapon_Pack, PurchaseForWeaponPack);
        instance = this;
    }
    void Start()
    {
        Geekplay.Instance.SubscribeOnReward(RewardForGold, GetGoldReward);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold1, GetGoldPur1);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold2, GetGoldPur2);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold3, GetGoldPur3);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon1, UnlockWeapon_Plane);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon2, UnlockWeapon_Car);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon3, UnlockWeapon_Press);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon4, UnlockWeapon_Dynamite);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon5, UnlockWeapon_Meteor);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon6, UnlockWeapon_Lightning);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon_Pack, UnlockWeapon_Pack);
        Geekplay.Instance.SubscribeOnPurchase(AppUnlockWeapon_All, UnlockWeapon_All);
    }
    public int GetGoldCountByName(string Name)
    {
        try
        {
            if (Name == AppUnlockWeapon_Pack)
            {
                return -2;
            }
            return OperationNameAndReward[Name];
        }
        catch
        {
            Debug.Log("Õ≈¬≈–ÕŒ≈ »Ãﬂ ƒÀﬂ PURCHASE");
            return -1;
        }
    }

    private void UnlockWeapon_All()
    {
        WeaponShop.instance.UnlockAllWeapon();
        WeaponAllBought?.Invoke();
        SetupDonatCount(60);
    }
    private void UnlockWeapon_Pack()
    {
        WeaponShop.instance.UnlockWeaponByInApp(5);
        WeaponShop.instance.UnlockWeaponByInApp(7);
        WeaponPackBought?.Invoke();
        SetupDonatCount(40);
    }
    private void UnlockWeapon_Plane()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(4);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(4);
        }
        SetupDonatCount(7);
    }
    private void UnlockWeapon_Car()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(5);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(5);
        }
        SetupDonatCount(11);
    }
    private void UnlockWeapon_Press()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(6);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(6);
        }
        SetupDonatCount(15);
    }
    private void UnlockWeapon_Dynamite()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(7);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(7);
        }
        SetupDonatCount(20);
    }
    private void UnlockWeapon_Meteor()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(8);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(8);
        }
        SetupDonatCount(25);
    }
    private void UnlockWeapon_Lightning()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            WeaponShop.instance.UnlockWeaponByInApp(9);
        }
        else
        {
            WeaponSelector.instance.UnlockWeaponInApp(9);
        }
        SetupDonatCount(30);
    }
    private void GetGoldReward()
    {
        Geekplay.Instance.PlayerData.Coins += RewardForGoldGold;
        RewardShowed?.Invoke();
    }
    private void GetGoldPur1()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold1;
        SetupDonatCount(15);
    }
    private void GetGoldPur2()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold2;
        SetupDonatCount(25);
    }
    private void GetGoldPur3()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold3;
        SetupDonatCount(35);
    }
    private void SetupDonatCount(int DonatedYan)
    {
        Geekplay.Instance.PlayerData.DonatCount += DonatedYan;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
}
