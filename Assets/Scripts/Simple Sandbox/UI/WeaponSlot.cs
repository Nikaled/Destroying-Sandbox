using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public int WeaponNumber;
    [SerializeField] Image ActiveWeaponImage;
    [SerializeField] TextMeshProUGUI NumberOfSlot;
    [SerializeField] Image BlockImage;
    // Start is called before the first frame update
    void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            NumberOfSlot.gameObject.SetActive(false);
        }
    }
    public void TakeWeapon()
    {
        Player.instance.SwitchWeapon(WeaponNumber);
    }
    public void ChangeSpriteInSlot(Sprite blockSprite)
    {
        BlockImage.sprite = blockSprite;
    }
    public void BlockIsActive()
    {
        ActiveWeaponImage.enabled = true;
    }
    public void BlockIsInactive()
    {
        ActiveWeaponImage.enabled = false;
    }
}
