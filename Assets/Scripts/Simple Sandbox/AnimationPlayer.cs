using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Player player;
    public bool IsMoving;
    public bool RidingAnimal;

    private readonly string FirstMovement = "FirstMovement";
    private void OnEnable()
    {
        player.PistolFire += DoPistolAnimation;
        player.characterController.myJumpAction += JumpAnimation;

    }
    private void OnDisable()
    {
        player.PistolFire -= DoPistolAnimation;
        player.characterController.myJumpAction -= JumpAnimation;

    }
    void Update()
    {
        if (player.InterfaceActive)
        {
            anim.SetBool("isRun", false);
            return;
        }
        if (player.examplePlayer.MyLockOnShoot == false)
        {
            if(Geekplay.Instance.mobile == false)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                IsMoving = h != 0 || v != 0;
                anim.SetBool("isRun", h != 0 || v != 0);
            }
            else
            {
                float h = player.examplePlayer.FixedJoystick.Horizontal;
                float v = player.examplePlayer.FixedJoystick.Vertical;
                IsMoving = h != 0 || v != 0;
                anim.SetBool("isRun", h != 0 || v != 0);
            }
            if (IsMoving)
            {
                if (Geekplay.Instance.PlayerData.IsFirstMovement == false)
                {
                    Geekplay.Instance.PlayerData.IsFirstMovement = true;
                    Geekplay.Instance.Save();
                    Analytics.instance.SendEvent(FirstMovement);
                }
            }

        }
        else
        {
            anim.SetBool("isRun", false);

        }
    }
    public void JumpAnimation()
    {
        if(Player.instance.examplePlayer.MyLockOnShoot == false)
        {
        anim.SetTrigger("jump");
        }

    }
    private void DoPistolAnimation()
    {
        anim.SetTrigger("PistolFire");
    }

}
