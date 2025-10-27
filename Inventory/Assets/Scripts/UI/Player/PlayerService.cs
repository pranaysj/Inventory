using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerService
{
    private PlayerController playerController;
    private PlayerView playerView;

    public PlayerController PlayerController => playerController;


    public PlayerService(PlayerView playerView, UIService uiService, GameService gameService)
    {
        playerController = new PlayerController(playerView, uiService, gameService);

    }

    public int GetMoney()
    {
        return PlayerController.GetMoney();
    }
    public void SetMoney(int value)
    {
        PlayerController.SetMoney(value);
    }
    internal void SellItem(TabView selectedItemView, int quantity, int sellingPrice, int grossWeight)
    {
        PlayerController.SellItem(selectedItemView, quantity, sellingPrice, grossWeight);
    }
    internal void BuyItem(TabView selectedItemView, int quantity, int buyingPrice, int grossWeight)
    {
        PlayerController.BuyItem(selectedItemView, quantity, buyingPrice, grossWeight);
    }
}
