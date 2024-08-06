using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsRewardWinMap : MonoBehaviour
{
    public string DoubleCoinsOnWin = "DoubleCoinsOnWin";
    public Button RewardButton;
    public int CurrentReward;
    void Start()
    {
        Geekplay.Instance.SubscribeOnReward(DoubleCoinsOnWin, DoubleCoins);
        RewardButton.onClick.AddListener(delegate { RewardOperation(); });
    }
    public void CheckAvailableRewardAndShowButtons()
    {
        if (Geekplay.Instance.RewardLockTimer > 0 || CurrentReward == 0)
        {
            CanvasManager.instance.ShowWinButtonsWithDelay(false);
            RewardButton.gameObject.SetActive(false);
        }
        else
        {
            CanvasManager.instance.ShowWinButtonsWithDelay(true);
            RewardButton.gameObject.SetActive(true);
        }
    }
    public void SetReward(int Reward)
    {
        CurrentReward = Reward;
    }
    private void RewardOperation()
    {
        Geekplay.Instance.ShowRewardedAd(DoubleCoinsOnWin);
        Geekplay.Instance.RunBlockRewardCoroutine();
    }
    private void DoubleCoins()
    {
        Geekplay.Instance.PlayerData.Coins += CurrentReward;
        CanvasManager.instance.ShowRewardAndSetRewardText(true, CurrentReward * 2);
        RewardButton.gameObject.SetActive(false);
    }
}
