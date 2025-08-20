using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    public PlayerService()
    {
        playerController = new PlayerController();
    }
}
