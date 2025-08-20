using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    public PlayerModel(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
