using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppShopCell : MonoBehaviour
{
    public string PurName;
    public Button BuyGoldButton;
    [SerializeField] TextMeshProUGUI GoldCount;
    [Header("Only Reward")]
    [SerializeField] GameObject RewardBlocker;
    [SerializeField] TextMeshProUGUI RewardTimerText;
    [SerializeField] bool IsWeaponPack;
    [SerializeField] bool IsWeaponAll;
    private void OnEnable()
    {
        CheckIsCellActual();

        if (IsWeaponPack)
        {
            Rewarder.instance.WeaponPackBought += CheckIsCellActual;
        }
        if (IsWeaponAll)
        {
            Rewarder.instance.WeaponAllBought += CheckIsCellActual;
        }


        int GetGoldCount = Rewarder.instance.GetGoldCountByName(PurName);
        if(GetGoldCount != -1)
        {
        GoldCount.text = GetGoldCount.ToString();
        }
        else
        {
            LastSlotLocalization();
        }

        if (GetGoldCount == -2)
        {
            WeaponPackLoc();
        }
       
        if (RewardBlocker != null && RewardTimerText !=null)
        {
            Geekplay.Instance.RewardLockTimeUpdate += SetNewTimerTextAndCheckEnd;
            if (Geekplay.Instance.RewardLockTimer > 0)
            {
                RewardBlocker.SetActive(true);
                RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
            }
        }
        if(Rewarder.instance != null)
        {
            Rewarder.instance.RewardShowed += RewardOperation;
        }
    }


    private void CheckIsCellActual()
    {
        if (IsWeaponPack)
        {
           if(Geekplay.Instance.PlayerData.WeaponOpenedArray != null)
            {
                if(Geekplay.Instance.PlayerData.WeaponOpenedArray.Length > 1)
                {
                    if(Geekplay.Instance.PlayerData.WeaponOpenedArray[5] && Geekplay.Instance.PlayerData.WeaponOpenedArray[7])
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        if (IsWeaponAll)
        {
            if (Geekplay.Instance.PlayerData.WeaponOpenedArray != null)
            {
                if (Geekplay.Instance.PlayerData.WeaponOpenedArray.Length > 1)
                {
                    bool AllWeaponUnlocked = true;
                    for (int i = 0; i < Geekplay.Instance.PlayerData.WeaponOpenedArray.Length; i++)
                    {
                        if(Geekplay.Instance.PlayerData.WeaponOpenedArray[i] == false)
                        {
                            AllWeaponUnlocked = false;
                            break;
                        }
                       
                    }
                    gameObject.SetActive(!AllWeaponUnlocked);
                }
            }
        }
    }
    private void LastSlotLocalization()
    {
        if(Geekplay.Instance.language == "ru")
        {
            GoldCount.text = "Все оружие";
        }
        if (Geekplay.Instance.language == "en")
        {
            GoldCount.text = "All weapons";
        }
        if (Geekplay.Instance.language == "tr")
        {
            GoldCount.text = "Tüm silahlar";
        }
    }
    private void WeaponPackLoc()
    {
        if (Geekplay.Instance.language == "ru")
        {
            GoldCount.text = "1000 + оружие";
        }
        if (Geekplay.Instance.language == "en")
        {
            GoldCount.text = "1000 + weapons";
        }
        if (Geekplay.Instance.language == "tr")
        {
            GoldCount.text = "1000 + silahlar";
        }
    }
    private void Start()
    {
        SubscribeOnPurchase();
    }
    private void OnDisable()
    {
        if(RewardBlocker != null && RewardTimerText != null)
        {
        Geekplay.Instance.RewardLockTimeUpdate -= SetNewTimerTextAndCheckEnd;
        }
        if (IsWeaponPack)
        {
            Rewarder.instance.WeaponPackBought -= CheckIsCellActual;
        }
        if (IsWeaponAll)
        {
            Rewarder.instance.WeaponAllBought -= CheckIsCellActual;
        }
    }
    public void SubscribeOnPurchase()
    {
        BuyGoldButton.onClick.AddListener(delegate { InAppOperation(); });
    }
    public void SubscribeOnReward()
    {
        BuyGoldButton.onClick.AddListener(delegate { RewardOperation(); });
    }
    private void InAppOperation()
    {
        Geekplay.Instance.RealBuyItem(PurName);
        CheckIsCellActual();
    }
    private void RewardOperation()
    {
        Geekplay.Instance.RunBlockRewardCoroutine();
        Geekplay.Instance.ShowRewardedAd(PurName);
        RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
        RewardBlocker.SetActive(true);
        BuyGoldButton.enabled = false;
    }
    private void SetNewTimerTextAndCheckEnd(int Timer)
    {
        if (RewardTimerText == null)
        {
            return;
        }
        if(Timer < 60)
        {
        RewardTimerText.text = string.Format("{0:00}:{1:00}", 00, Timer);
        }
        else
        {
           int TimerSeconds = Timer - 60;
            RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, TimerSeconds);
        }
        if (Timer <= 0)
        {
            RewardBlocker.SetActive(false);
            BuyGoldButton.enabled = true;
        }
    }
}
