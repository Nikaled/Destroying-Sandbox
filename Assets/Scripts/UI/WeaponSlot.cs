using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        Player.instance.SwitchActiveWeaponSlot(WeaponNumber);
    }
    public void ChangeSpriteInSlot(Sprite blockSprite)
    {
        BlockImage.sprite = blockSprite;
    }
    public void WeaponIsActive()
    {
        ActiveWeaponImage.enabled = true;
    }
    public void WeaponIsInactive()
    {
        ActiveWeaponImage.enabled = false;
    }
}
