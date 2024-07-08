using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public static WeaponSelector instance;
    [SerializeField] GameObject[] WeaponsInChild;
    [SerializeField] WeaponSlotManager WeaponManager;
    public int CurrentIndexToOpen;
    private bool[] UnlockOneTime;
    private void Awake()
    {
        instance = this;
    }
    public void UnlockAllWeaponForTutorial()
    {
        if (UnlockOneTime == null)
        {
            UnlockOneTime = new bool[10];
        }
        for (int i = 0; i < UnlockOneTime.Length; i++)
        {
        UnlockOneTime[i] = true;
        }
        SetUnlockImages(Geekplay.Instance.PlayerData.WeaponOpenedArray);
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
        //Geekplay.Instance.PlayerData.WeaponOpenedArray = null;
        //Geekplay.Instance.Save();
    }
    private void OnEnable()
    {
        if (CycleManager.instance != null)
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
    private void SetUnlockImages(bool[] OpenedWeapons)
    {
        bool[] AllWaysUnlockedWeapons = new bool[OpenedWeapons.Length];

        for (int i = 0; i < AllWaysUnlockedWeapons.Length; i++)
        {
            AllWaysUnlockedWeapons[i] = OpenedWeapons[i];
        }
        for (int i = 0; i < AllWaysUnlockedWeapons.Length; i++)
        {
            if (UnlockOneTime == null)
            {
                break;
            }
            if (i < UnlockOneTime.Length)
            {
                if (UnlockOneTime[i] == true)
                {
                    AllWaysUnlockedWeapons[i] = true;
                }
            }
        }
        WeaponManager.ChangeLockedWeaponImages(AllWaysUnlockedWeapons);
    }
    public bool IsWeaponAvailable(int WeaponPressedNumber)
    {
        int WeaponIndex = WeaponPressedNumber - 1;
        if (Geekplay.Instance.PlayerData.WeaponOpenedArray == null)
        {
            Geekplay.Instance.PlayerData.WeaponOpenedArray = new bool[10];
            for (int i = 0; i < 5; i++)
            {
                Geekplay.Instance.PlayerData.WeaponOpenedArray[i] = true;
            }
            Geekplay.Instance.Save();
        }
        else if (Geekplay.Instance.PlayerData.WeaponOpenedArray.Length < 9)
        {
            Geekplay.Instance.PlayerData.WeaponOpenedArray = new bool[10];
            for (int i = 0; i < 5; i++)
            {
                Geekplay.Instance.PlayerData.WeaponOpenedArray[i] = true;
            }
            Geekplay.Instance.Save();
        }
        bool[] OpenedWeapon = Geekplay.Instance.PlayerData.WeaponOpenedArray;
        SetUnlockImages(OpenedWeapon);

        if (UnlockOneTime != null)
        {
            if (UnlockOneTime[WeaponIndex])
            {
                return true;
            }
        }
        if (OpenedWeapon[WeaponIndex])
        {
            return true;
        }
        else
        {
            CanvasManager.instance.ShowUnlockWeaponSlotUI(true);
            CurrentIndexToOpen = WeaponIndex;
            return false;
        }
    }
    public void UnlockWeapon()
    {
        Geekplay.Instance.PlayerData.WeaponOpenedArray[CurrentIndexToOpen] = true;
        Player.instance.SwitchWeapon(CurrentIndexToOpen + 1);
        Geekplay.Instance.Save();
    }
    public void UnlockWeaponOneTime()
    {
        if (UnlockOneTime == null)
        {
            UnlockOneTime = new bool[10];
        }
        UnlockOneTime[CurrentIndexToOpen] = true;
        SetUnlockImages(Geekplay.Instance.PlayerData.WeaponOpenedArray);
        Player.instance.SwitchWeapon(CurrentIndexToOpen + 1);

    }
}
