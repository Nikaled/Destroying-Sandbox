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
    public int RewardForGoldGold = 50;
    public int PurchaseForGoldGold1 = 100;
    public int PurchaseForGoldGold2 = 200;
    public int PurchaseForGoldGold3 = 500;
    public Action RewardShowed;
     Dictionary<string, int> OperationNameAndReward = new();
    private void Awake()
    {
        OperationNameAndReward.Add(RewardForGold, RewardForGoldGold);
        OperationNameAndReward.Add(AppForGold1, PurchaseForGoldGold1);
        OperationNameAndReward.Add(AppForGold2, PurchaseForGoldGold2);
        OperationNameAndReward.Add(AppForGold3, PurchaseForGoldGold3);
        instance = this;
    }
    void Start()
    {
        Geekplay.Instance.SubscribeOnReward(RewardForGold, GetGoldReward);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold1, GetGoldPur1);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold2, GetGoldPur2);
        Geekplay.Instance.SubscribeOnPurchase(AppForGold3, GetGoldPur3);
    }

    private void GetGoldReward()
    {
        Geekplay.Instance.PlayerData.Coins += RewardForGoldGold;
        RewardShowed?.Invoke();
    }
    private void GetGoldPur1()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold1;
    }
    private void GetGoldPur2()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold2;
    }
    private void GetGoldPur3()
    {
        Geekplay.Instance.PlayerData.Coins += PurchaseForGoldGold3;
    }
}
