using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
     Dictionary<string, int> OperationNameAndReward = new();
    private void Awake()
    {
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
            if(Name == AppUnlockWeapon_Pack)
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
    }
    private void UnlockWeapon_Pack()
    {
        WeaponShop.instance.UnlockWeaponByInApp(5);
        WeaponShop.instance.UnlockWeaponByInApp(7);
    }
    private void UnlockWeapon_Plane()
    {
        WeaponShop.instance.UnlockWeaponByInApp(4);
    }
    private void UnlockWeapon_Car()
    {
        WeaponShop.instance.UnlockWeaponByInApp(5);
    }
    private void UnlockWeapon_Press()
    {
        WeaponShop.instance.UnlockWeaponByInApp(6);
    }
    private void UnlockWeapon_Dynamite()
    {
        WeaponShop.instance.UnlockWeaponByInApp(7);
    }
    private void UnlockWeapon_Meteor()
    {
        WeaponShop.instance.UnlockWeaponByInApp(8);
    }
    private void UnlockWeapon_Lightning()
    {
        WeaponShop.instance.UnlockWeaponByInApp(9);
    }
    private void GetGoldReward()
    {
        Geekplay.Instance.PlayerData.Coins += RewardForGoldGold;
        RewardShowed?.Invoke();
    }
    private void GetGoldPur1()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold1;
        Geekplay.Instance.PlayerData.DonatCount += PurchaseForGoldGold1;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
    private void GetGoldPur2()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold2;
        Geekplay.Instance.PlayerData.DonatCount += PurchaseForGoldGold2;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
    private void GetGoldPur3()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold3;
        Geekplay.Instance.PlayerData.DonatCount += PurchaseForGoldGold3;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
}
