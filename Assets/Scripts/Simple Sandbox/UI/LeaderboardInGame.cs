using UnityEngine;
using TMPro;
using System;

public class LeaderboardInGame : MonoBehaviour
{
    private string leadersText;
    [SerializeField] private TextMeshProUGUI leadersText1Bottom;

    [SerializeField] private TextMeshProUGUI[] linesTexts;
    [SerializeField] private TextMeshProUGUI[] linesPointsTexts;

    [SerializeField] private int leaderboardNumber;


    private float timeFlag = 0;

    void Start()
    {
        if (leaderboardNumber == 1)
            Geekplay.Instance.leaderboardInGame = this;
        else if (leaderboardNumber == 2)
            Geekplay.Instance.leaderboardInGame2 = this;
        else if (leaderboardNumber == 3)
            Geekplay.Instance.leaderboardInGame3 = this;

        int time = Convert.ToInt32(Geekplay.Instance.remainingTimeUntilUpdateLeaderboard);

        if (Geekplay.Instance.language == "en")
        {
            leadersText1Bottom.text = $"Table will be updated in: {time}";
        }
        else if (Geekplay.Instance.language == "ru")
        {
            leadersText1Bottom.text = $"Таблица обновится через: {time}";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            leadersText1Bottom.text = $"Su yolla güncellendi: {time}";
        }

        //if (Geekplay.Instance.remainingTimeUntilUpdateLeaderboard <= 0)
        UpdateLeaderBoard();
    }


    private void Update()
    {
        if (Geekplay.Instance.remainingTimeUntilUpdateLeaderboard <= 0)
        {
            UpdateLeaderBoard();
        }
        timeFlag += Time.deltaTime;

        if (timeFlag < 1f) return;

        timeFlag = 0;
        int time = Convert.ToInt32(Geekplay.Instance.remainingTimeUntilUpdateLeaderboard);


        if (Geekplay.Instance.language == "en")
        {
            leadersText1Bottom.text = $"Table will be updated in: {time}";
        }
        else if (Geekplay.Instance.language == "ru")
        {
            leadersText1Bottom.text = $"Таблица обновится через: {time}";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            leadersText1Bottom.text = $"Su yolla güncellendi: {time}";
        }

    }

    public void SetText()
    {
        leadersText = "";
        Geekplay.Instance.lastLeaderText = "";
        if (leaderboardNumber == 1)
        {
            for (int i = 0; i < Geekplay.Instance.l.Length; i++)
            {
                if (Geekplay.Instance.l[i] != null && Geekplay.Instance.lN[i] != null)
                {
                    Geekplay.Instance.lastLeaderText += $"{i + 1}. {Geekplay.Instance.lN[i]} : {Geekplay.Instance.l[i]}\n";
                    leadersText = Geekplay.Instance.lastLeaderText;
                    ToLines();
                }
            }
        }
        if (leaderboardNumber == 2)
        {
            for (int i = 0; i < Geekplay.Instance.l2.Length; i++)
            {
                if (Geekplay.Instance.l2[i] != null && Geekplay.Instance.lN2[i] != null)
                {
                    Geekplay.Instance.lastLeaderText2 += $"{i + 1}. {Geekplay.Instance.lN2[i]} : {Geekplay.Instance.l2[i]}\n";
                    leadersText = Geekplay.Instance.lastLeaderText2;
                    ToLines();
                }
            }
        }
        if (leaderboardNumber == 3)
        {
            for (int i = 0; i < Geekplay.Instance.l3.Length; i++)
            {
                if (Geekplay.Instance.l3[i] != null && Geekplay.Instance.lN3[i] != null)
                {
                    Geekplay.Instance.lastLeaderText3 += $"{i + 1}. {Geekplay.Instance.lN3[i]} : {Geekplay.Instance.l3[i]}\n";
                    leadersText = Geekplay.Instance.lastLeaderText3;
                    ToLines();
                }
            }
        }
    }

    public void UpdateLeaderBoard()
    {
        Geekplay.Instance.remainingTimeUntilUpdateLeaderboard = Geekplay.Instance.timeToUpdateLeaderboard;

        if (leaderboardNumber == 1)
        {
            Utils.GetLeaderboard("score", 0, "Buildings");
            Utils.GetLeaderboard("name", 0, "Buildings");
            Geekplay.Instance.leaderNumber = 0;
            Geekplay.Instance.leaderNumberN = 0;
        }

        if (leaderboardNumber == 2)
        {
            Utils.GetLeaderboard2("score", 0, "Destroy");
            Utils.GetLeaderboard2("name", 0, "Destroy");
            Geekplay.Instance.leaderNumber2 = 0;
            Geekplay.Instance.leaderNumberN2 = 0;

            Debug.Log("SECOND LEADERBOARD");
        }

        if (leaderboardNumber == 3)
        {
            Utils.GetLeaderboard3("score", 0, "Donat");
            Utils.GetLeaderboard3("name", 0, "Donat");
            Geekplay.Instance.leaderNumber3 = 0;
            Geekplay.Instance.leaderNumberN3 = 0;

            Debug.Log("THIRD LEADERBOARD");
        }
    }



    void ToLines()
    {
        int index = 0;

        if (leaderboardNumber == 1)
        {
            for (int i = 0; i < Geekplay.Instance.lN.Length; i++)
            {
                linesTexts[i].text = Geekplay.Instance.lN[i];
                linesPointsTexts[i].text = Geekplay.Instance.l[i];
            }
        }
        else if (leaderboardNumber == 2)
        {
            for (int i = 0; i < Geekplay.Instance.lN2.Length; i++)
            {
                linesTexts[i].text = Geekplay.Instance.lN2[i];
                linesPointsTexts[i].text = Geekplay.Instance.l2[i];
            }
        }
        else if (leaderboardNumber == 3)
        {
            for (int i = 0; i < Geekplay.Instance.lN3.Length; i++)
            {
                linesTexts[i].text = Geekplay.Instance.lN3[i];
                linesPointsTexts[i].text = Geekplay.Instance.l3[i];
            }
        }
    }
}