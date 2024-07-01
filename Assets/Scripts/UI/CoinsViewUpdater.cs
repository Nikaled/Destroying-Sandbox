using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsViewUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinsCount;
    private void OnEnable()
    {
        SetCoinsText(Geekplay.Instance.PlayerData.Coins);
        Geekplay.Instance.PlayerData.CoinsChanged += SetCoinsText;
    }
    private void OnDisable()
    {
        Geekplay.Instance.PlayerData.CoinsChanged -= SetCoinsText;
    }
    private void SetCoinsText(int currentCoins)
    {
        CoinsCount.text = currentCoins.ToString();
    }
}
