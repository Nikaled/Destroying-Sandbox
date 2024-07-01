using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetGoldButton : MonoBehaviour
{
    public string PurName;
    //public int PurGold;
    public Button BuyGoldButton;
    [SerializeField] GameObject RewardBlocker;
    [SerializeField] TextMeshProUGUI RewardTimerText;
    [SerializeField] Pulse ObjectPulse;
    [SerializeField] GameObject HideParent;
    private void Start()
    {
        SubscribeOnReward();
    }
    private void OnEnable()
    {
      
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate += SetNewTimerTextAndCheckEnd;
            if (Geekplay.Instance.RewardLockTimer > 0)
            {
                RewardBlocker.SetActive(true);
                RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);

                ShowButton(false);
                StopPulsing();
            }
            else
            {
                ShowButton(true);
                StartPulsing();
            }
        }
    }

    public void ShowButton(bool Is)
    {
        BuyGoldButton.image.enabled = Is;
        HideParent.SetActive(Is);
    }
    private void StartPulsing()
    {
        //if (ObjectPulse != null)
        //{
        //    ObjectPulse.IsUnlocked = true;
        //    ObjectPulse.StartPulsingIfUnlocked();
        //}
    }
    private void StopPulsing()
    {
        //if (ObjectPulse != null)
        //{
        //    ObjectPulse.StopPulsing();
        //    ObjectPulse.IsUnlocked = false;
        //}
    }
    private void OnDisable()
    {
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate -= SetNewTimerTextAndCheckEnd;
            StopPulsing();
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
        Geekplay.Instance.ShowRewardedAd(PurName);
        Geekplay.Instance.RunBlockRewardCoroutine();

        RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
        RewardBlocker.SetActive(true);
        BuyGoldButton.enabled = false;
        ShowButton(false);
    }
    private void SetNewTimerTextAndCheckEnd(int Timer)
    {
        if (RewardTimerText == null)
        {
            return;
        }
        if (Timer < 60)
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
            ShowButton(true);
            StartPulsing();
        }
    }
    private void GetGold()
    {
        //Geekplay.Instance.PlayerData.Coins += PurGold;
        //Geekplay.Instance.Save();
    }
    private IEnumerator BlockRewardOnTimeByGeekplay()
    {
        Geekplay.Instance.RewardLockTimer = 90;
        while (Geekplay.Instance.RewardLockTimer > 0)
        {
            yield return new WaitForSeconds(1);
            Geekplay.Instance.RewardLockTimer--;
            Geekplay.Instance.RewardLockTimeUpdate?.Invoke(Geekplay.Instance.RewardLockTimer);
        }
    }
}
