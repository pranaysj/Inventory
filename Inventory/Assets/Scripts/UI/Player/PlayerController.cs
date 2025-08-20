using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public PlayerModel PlayerModel => playerModel;
    public PlayerView PlayerView => playerView;

    public PlayerController()
    {
        playerModel = new PlayerModel(this);
        playerView = ServiceLocator.Get<GameService>().PlayerView;
        PlayerView.Initialize(this);
    }
}
