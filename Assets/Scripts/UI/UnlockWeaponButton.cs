using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UnlockWeaponButton : MonoBehaviour
{
    public Button UnlockButton;
    public bool IsReward;
    [Header("Only Reward")]
    [SerializeField] GameObject RewardBlocker;
    [SerializeField] TextMeshProUGUI RewardTimerText;
    private void Start()
    {
        if (IsReward)
        {
            SubscribeOnReward();
        }
        else
        {
            SubscribeOnPurchase();
        }
    }
    private void OnEnable()
    {
      
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate += SetNewTimerTextAndCheckEnd;
            if (Geekplay.Instance.RewardLockTimer > 0)
            {
                RewardBlocker.SetActive(true);
                UnlockButton.enabled = false;
                RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
            }
            else
            {
                UnlockButton.enabled = true;
                RewardBlocker.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate -= SetNewTimerTextAndCheckEnd;
        }
    }
    private void UnlockWeapon()
    {
        WeaponSelector.instance.UnlockWeapon();
    }
    private void UnlockWeaponOneTime()
    {
        WeaponSelector.instance.UnlockWeaponOneTime();
    }
    public void SubscribeOnPurchase()
    {
        Geekplay.Instance.SubscribeOnPurchase("UnlockWeapon", UnlockWeapon);
        UnlockButton.onClick.AddListener(delegate { InAppOperation(); });
    }
    public void SubscribeOnReward()
    {
        Geekplay.Instance.SubscribeOnReward("UnlockWeapon", UnlockWeaponOneTime);
        UnlockButton.onClick.AddListener(delegate { RewardOperation(); });
    }
    private void InAppOperation()
    {
        Geekplay.Instance.RealBuyItem("UnlockWeapon");
    }
    private void RewardOperation()
    {
        Geekplay.Instance.ShowRewardedAd("UnlockWeapon");
        Geekplay.Instance.RunBlockRewardCoroutine();
        //WeaponSelector.instance.UnlockWeaponOneTime();
        RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
        RewardBlocker.SetActive(true);
        UnlockButton.enabled = false;
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
            UnlockButton.enabled = true;
        }
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
