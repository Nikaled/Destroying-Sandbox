using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LeaderboardManager : MonoBehaviour
{
    private float timeFlag = 0;
  [SerializeField]  private LeaderboardInGame[] Leaderboards;
    private int currentLeaderboardIndex = 0;

    private void Start()
    {
        Geekplay.Instance.LeaderboardValuesReady += GetDataToNextLeaderBoard;
        Leaderboards[0].UpdateLeaderBoard();
    }

    private void GetDataToNextLeaderBoard()
    {
        StartCoroutine(GetDataAfterDelay());
        

    }
    private IEnumerator GetDataAfterDelay()
    {
        yield return new WaitForSeconds(10);
        Leaderboards[currentLeaderboardIndex].SetText();
        currentLeaderboardIndex++;
        if (currentLeaderboardIndex == Leaderboards.Length)
        {
            currentLeaderboardIndex = 0;
            yield break;
        }
        Leaderboards[currentLeaderboardIndex].UpdateLeaderBoard();
    }
    private void Update()
    {
        if (Geekplay.Instance.remainingTimeUntilUpdateLeaderboard <= 0)
        {
            Leaderboards[0].UpdateLeaderBoard();
        }
        timeFlag += Time.deltaTime;

        if (timeFlag < 1f) return;

        timeFlag = 0;
        int time = Convert.ToInt32(Geekplay.Instance.remainingTimeUntilUpdateLeaderboard);


        //if (Geekplay.Instance.language == "en")
        //{
        //    leadersText1Bottom.text = $"Table will be updated in: {time}";
        //}
        //else if (Geekplay.Instance.language == "ru")
        //{
        //    leadersText1Bottom.text = $"Таблица обновится через: {time}";
        //}
        //else if (Geekplay.Instance.language == "tr")
        //{
        //    leadersText1Bottom.text = $"Su yolla güncellendi: {time}";
        //}

    }
}
