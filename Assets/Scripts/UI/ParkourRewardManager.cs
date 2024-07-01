using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkourRewardManager : MonoBehaviour
{
    public string ParkourRewardName;
    public Button RewardButton;
    public Button DeclineButton;
    public GameObject RewardWindow;
    public static ParkourRewardManager instance;
   [HideInInspector] public ParkourMapCell currentPressedCell;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SubscribeOnReward();
    }
    public void TryShowRewardWindow()
    {
        RewardWindow.SetActive(true);
        RewardButton.onClick.RemoveAllListeners();
        RewardButton.onClick.AddListener(delegate { RewardOperation(); });
        //RewardButton.onClick.AddListener(delegate { currentPressedCell.LoadMapLogic(); });
        DeclineButton.onClick.RemoveAllListeners();
        DeclineButton.onClick.AddListener(delegate { currentPressedCell.LoadMapLogic(); });
    }
    public void SubscribeOnReward()
    {
        Geekplay.Instance.SubscribeOnReward(ParkourRewardName, GetSpeed);
        RewardButton.onClick.AddListener(delegate { RewardOperation(); });
    }
    private void RewardOperation()
    {
        Geekplay.Instance.ShowRewardedAd(ParkourRewardName);
        Geekplay.Instance.RunBlockRewardCoroutine();
    }
    private void GetSpeed()
    {
        Geekplay.Instance.PlayerData.IsParkourSpeedUpForReward = true;
        currentPressedCell.LoadMapLogic();
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
