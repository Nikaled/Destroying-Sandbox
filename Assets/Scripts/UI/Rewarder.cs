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
    [SerializeField] string AppForGold4 = "AppForGold4";
    [SerializeField] string AppForGold5 = "AppForGold5";
    public int RewardForGoldGold = 50;
    public int PurchaseForGoldGold1 = 100;
    public int PurchaseForGoldGold2 = 200;
    public int PurchaseForGoldGold3 = 500;
    public int PurchaseForGoldGold4 = 1000;
    public int PurchaseForGoldGold5 = 2000;
    public Action RewardShowed;
     Dictionary<string, int> OperationNameAndReward = new();
    private void Awake()
    {
        OperationNameAndReward.Add(RewardForGold, RewardForGoldGold);
        OperationNameAndReward.Add(AppForGold1, PurchaseForGoldGold1);
        OperationNameAndReward.Add(AppForGold2, PurchaseForGoldGold2);
        OperationNameAndReward.Add(AppForGold3, PurchaseForGoldGold3);
        OperationNameAndReward.Add(AppForGold4, PurchaseForGoldGold4);
        OperationNameAndReward.Add(AppForGold5, PurchaseForGoldGold5);
        instance = this;
    }
    void Start()
    {
        Geekplay.Instance.SubscribeOnReward(RewardForGold, GetGoldReward);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold1, GetGoldPur1);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold2, GetGoldPur2);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold3, GetGoldPur3);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold4, GetGoldPur4);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold5, GetGoldPur5);
    }
    public int GetGoldCountByName(string Name)
    {
        try
        {
       return OperationNameAndReward[Name];
        }
        catch
        {
            Debug.Log("Õ≈¬≈–ÕŒ≈ »Ãﬂ ƒÀﬂ PURCHASE");
            return -1;
        }
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
    private void GetGoldPur4()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold4;
        Geekplay.Instance.PlayerData.DonatCount += PurchaseForGoldGold4;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
    private void GetGoldPur5()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold5;
        Geekplay.Instance.PlayerData.DonatCount += PurchaseForGoldGold5;
        Geekplay.Instance.Leaderboard("Donat", Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }
}
