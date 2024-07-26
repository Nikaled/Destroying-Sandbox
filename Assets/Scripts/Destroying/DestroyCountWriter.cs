using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCountWriter : MonoBehaviour
{
    private int CashedCount;
    private int CurrentCount;
    void Start()
    {
        StartCoroutine(WriteToLeaderboard());
    }
    private IEnumerator WriteToLeaderboard()
    {
        CashedCount = Geekplay.Instance.PlayerData.DestroyCount;
        yield return new WaitForSeconds(5);
        if(CashedCount < Geekplay.Instance.PlayerData.DestroyCount)
        {
            Geekplay.Instance.Save();
            Geekplay.Instance.Leaderboard("Destroy", Geekplay.Instance.PlayerData.DestroyCount);
        }
    }
}
