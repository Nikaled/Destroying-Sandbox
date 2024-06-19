using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public static WeaponSelector instance;
    [SerializeField] GameObject[] WeaponsInChild;
    private void Awake()
    {
        instance = this;
    }
    public void SelectWeapon(int WeaponsInChildIndex)
    {
        HideAllWeapons();
        WeaponsInChild[WeaponsInChildIndex].SetActive(true);
    }
    public void HideAllWeapons()
    {
        for (int i = 0; i < WeaponsInChild.Length; i++)
        {
            WeaponsInChild[i].SetActive(false);
        }
    }
    void Start()
    {
        CycleManager.instance.BuildingPhaseStarted += HideAllWeapons;
    }
    private void OnEnable()
    {
        if (CycleManager.instance !=null)
        {
            CycleManager.instance.BuildingPhaseStarted += HideAllWeapons;
        }
    }
    private void OnDisable()
    {
        if (CycleManager.instance != null)
        {
            CycleManager.instance.BuildingPhaseStarted -= HideAllWeapons;
        }
    }
}
