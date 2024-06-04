using UnityEngine;
using TMPro;
using System;

public class LeaderboardInGame : MonoBehaviour
{
    [SerializeField] private string leadersText;
    //[SerializeField] private TextMeshProUGUI leadersText1Bottom;

    [SerializeField] private TextMeshProUGUI[] linesTexts;
    [SerializeField] private TextMeshProUGUI[] linesPointsTexts;
    [SerializeField] private LeaderboardCell[] leaderboardCells;
    [SerializeField] private TextMeshProUGUI LeaderBoardUpdateText;

    public string LeaderboardName;
    private float timeFlag = 0;

    void Start()
    {
        int time = Convert.ToInt32(Geekplay.Instance.remainingTimeUntilUpdateLeaderboard);
        SetNamesAndPointsFieldsFromCells();
        if (Geekplay.Instance.language == "en")
        {
            for (int i = 0; i < linesTexts.Length; i++)
            {
                linesTexts[i].text = "Empty Slot";
            }
        }
        else if (Geekplay.Instance.language == "ru")
        {
            for (int i = 0; i < linesTexts.Length; i++)
            {
                linesTexts[i].text = "Пустой слот";
            }
        }
        else if (Geekplay.Instance.language == "tr")
        {
            for (int i = 0; i < linesTexts.Length; i++)
            {
                linesTexts[i].text = "Boş yuva";
            }
        }

        //    LeaderBoardUpdateText.text = $"Table will be updated in: {time}";
        //}
        //else if (Geekplay.Instance.language == "ru")
        //{
        //    LeaderBoardUpdateText.text = $"Таблица обновится через: {time}";
        //}
        //else if (Geekplay.Instance.language == "tr")
        //{
        //    LeaderBoardUpdateText.text = $"Su yolla güncellendi: {time}";
        //}



        if (Geekplay.Instance.remainingTimeUntilUpdateLeaderboard <= 0)
            UpdateLeaderBoard();

        else if (Geekplay.Instance.lastLeaderText != string.Empty)
        {
            leadersText = Geekplay.Instance.lastLeaderText;
            ToLines();
        }
    }

    private void SetNamesAndPointsFieldsFromCells()
    {
        if (leaderboardCells.Length > linesTexts.Length)
        {
            Debug.Log("linesTexts more than cells");
            return;
        }
        for (int i = 0; i < leaderboardCells.Length; i++)
        {
            linesTexts[i] = leaderboardCells[i].NameText;
            linesPointsTexts[i] = leaderboardCells[i].PointText;
        }
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

    public void SetText()
    {
        leadersText = "";
        Geekplay.Instance.lastLeaderText = "";
        for (int i = 0; i < Geekplay.Instance.lS.Count; i++)
        {
            if (Geekplay.Instance.lS[i] != null && Geekplay.Instance.lN[i] != null)
            {
                string s = $"{i + 1}. {Geekplay.Instance.lN[i]} : {Geekplay.Instance.lS[i]}\n";
                if (s == $"{i + 1}.  : \n")
                {
                    s = $"{i + 1}.\n";
                }

                Geekplay.Instance.lastLeaderText += $"{i + 1}. {Geekplay.Instance.lN[i]} : {Geekplay.Instance.lS[i]}\n";
                leadersText = Geekplay.Instance.lastLeaderText;
                ToLines();
            }
        }
    }

    public void UpdateLeaderBoard()
    {
        Geekplay.Instance.remainingTimeUntilUpdateLeaderboard = Geekplay.Instance.timeToUpdateLeaderboard;

        Geekplay.Instance.leaderNumber = 0;
        Geekplay.Instance.leaderNumberN = 0;
        //Utils.GetLeaderboard("score", 0, LeaderboardName);
        //Utils.GetLeaderboard("name", 0, LeaderboardName);
    }

    public void SetLeadersView(string[] name, string[] value, int count)
    {

        for (int i = 0; i < count; i++)
        {
            Debug.Log("Name: " + name[i]);
            Debug.Log("Value: " + value[i]);
        }
    }
    void ToLines()
    {
        int index = 0;

        for (int i = 0; i < Geekplay.Instance.lN.Count; i++)
        {
            linesTexts[i].text = Geekplay.Instance.lN[i];
            linesPointsTexts[i].text = Geekplay.Instance.lS[i];
        }
    }
}