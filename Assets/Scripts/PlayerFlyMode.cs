using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyMode : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = Player.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.motor.MoveCharacter(Vector3.forward);
        }
    }
}
