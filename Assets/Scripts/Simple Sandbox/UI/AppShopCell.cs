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
    private void OnEnable()
    {
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
