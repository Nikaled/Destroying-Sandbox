using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdWarning : MonoBehaviour
{
    public int TimeToShowWarning;
    private int CashedBaseTimeToShowWarning;
    public GameObject WarningPanel;
    public TextMeshProUGUI WarningText;
    public TextMeshProUGUI YouWillGetRewardText;
    [SerializeField] GameObject AddCoinsConfirmUI;
    public static AdWarning instance;
    private int CurrentTimeToShowWarning;
    private IEnumerator AwaitWarningCor;
    private void Awake()
    {
        instance = this;
    }
    public void ShowAdOnStart()
    {
        Geekplay.Instance.ShowInterstitialAd();
    }
    public void SetupAdByModeAndStartTimer()
    {
        if (SerializeBlockManager.instance.OnlyParkourMap)
        {
            return;
        }
        CashedBaseTimeToShowWarning = TimeToShowWarning;
        if (AwaitWarningCor != null)
        {
            StopCoroutine(AwaitWarningCor);
        }
        AwaitWarningCor = AwaitAndShowWarningPanel();
        StartCoroutine(AwaitWarningCor);
        LocalizateText(5);
        Geekplay.Instance.ShowedAdInEditor += ResumeTime;
        LocalizeReward();
    }
    private void OnEnable()
    {
        if(Geekplay.Instance !=null)
        Geekplay.Instance.ShowedAdInEditor += ResumeTime;
    }
    private void OnDisable()
    {
        Geekplay.Instance.ShowedAdInEditor -= ResumeTime;
    }
    public void AddTimeToShowWarning(int plusTime)
    {
        if (AwaitWarningCor != null)
        {
            StopCoroutine(AwaitWarningCor);
        }
        TimeToShowWarning = CurrentTimeToShowWarning + plusTime;
        AwaitWarningCor = AwaitAndShowWarningPanel();
        StartCoroutine(AwaitWarningCor);
    }
    private IEnumerator AwaitAndShowWarningPanel()
    {
        CurrentTimeToShowWarning = TimeToShowWarning;
        for (int i = 1; i <= TimeToShowWarning; i++)
        {
            yield return new WaitForSeconds(1);
            CurrentTimeToShowWarning = TimeToShowWarning - i;
        }
        TimeToShowWarning = CashedBaseTimeToShowWarning;
        WarningPanel.SetActive(true);
        StartCoroutine(StartTimer());
    }
    private void ResumeTime()
    {
        //Time.timeScale = 1;
    }
    private IEnumerator StartTimer()
    {
        Player.instance.AdWarningActive = true;
        bool InterfaceState = Player.instance.InterfaceActive;
        Player.instance.InterfaceActive = true;
        Cursor.lockState = CursorLockMode.None;
        Geekplay.Instance.IsAdWarningShowing = true;
        Time.timeScale = 0f;
        int Timer = 5;
        while (Timer != 0)
        {
            LocalizateText(Timer);
            Timer--;
            yield return new WaitForSecondsRealtime(1f);
        }
        Geekplay.Instance.ShowInterstitialAd();
        Geekplay.Instance.IsAdWarningShowing = false;
        Geekplay.Instance.PlayerData.Coins++;
        Geekplay.Instance.Save();
        WarningPanel.SetActive(false);
        AddCoinsConfirmUI.SetActive(true);
        Player.instance.AdWarningActive = false;
        //Player.instance.InterfaceActive = InterfaceState;
        Cursor.lockState = CursorLockMode.None;
        if (AwaitWarningCor != null)
        {
            StopCoroutine(AwaitWarningCor);
        }
        AwaitWarningCor = AwaitAndShowWarningPanel();
        StartCoroutine(AwaitWarningCor);
        //#if UNITY_EDITOR
        //        CanvasManager.instance.CheckActiveUnlockCursorWindows();
        //#endif
    }
    public void ConfirmCoinButton()
    {
        Time.timeScale = 1f;
    }
    private void LocalizeReward()
    {
        if (Geekplay.Instance.language == "ru")
            YouWillGetRewardText.text = "Вы получите:";
        if (Geekplay.Instance.language == "en")
            YouWillGetRewardText.text = "You will get:";
        if (Geekplay.Instance.language == "tr")
            YouWillGetRewardText.text = "Alacaksınız:";
    }
    private void LocalizateText(int Timer)
    {
        if (Geekplay.Instance.language == "ru")
            WarningText.text = $"реклама через: {Timer}";
        if (Geekplay.Instance.language == "en")
            WarningText.text = $"Advertisement in: {Timer}";
        if (Geekplay.Instance.language == "tr")
            WarningText.text = $"Saniye sonra reklam verin: {Timer}";

    }
}