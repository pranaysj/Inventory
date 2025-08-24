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

    private GameObject contentHolder;
    private GameObject itemPrefab;
    private ShopView shopView;
    private ShopDatabaseSO shopDatabaseSO;

    private Image icon;
    private TextMeshProUGUI type;
    private TextMeshProUGUI rarity;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;

    private TextMeshProUGUI bagWight;
    private TextMeshProUGUI money;

    private Sprite selectedPlayerItemBGIcon;
    private Sprite unselectedPlayerItemBGIcon;

    public PlayerService()
    {
        playerController = new PlayerController();
        Initialize();
    }
    public void Initialize()
    {
        playerView = ServiceLocator.Get<GameService>().PlayerView;

        contentHolder = ServiceLocator.Get<UIService>().Content;
        itemPrefab = ServiceLocator.Get<GameService>().PlayerItemPrefab;
        shopView = ServiceLocator.Get<GameService>().ShopView;
        shopDatabaseSO = ServiceLocator.Get<GameService>().ShopDatabase;

        icon = ServiceLocator.Get<UIService>().IconR;
        type = ServiceLocator.Get<UIService>().TypeR;
        rarity = ServiceLocator.Get<UIService>().RarityR;
        itemName = ServiceLocator.Get<UIService>().ItemNameR;
        description = ServiceLocator.Get<UIService>().DescriptionR;

        bagWight = ServiceLocator.Get<UIService>().BagWeight;
        money = ServiceLocator.Get<UIService>().Money;

        selectedPlayerItemBGIcon = ServiceLocator.Get<UIService>().SelectedPlayerItemBGIcon;
        unselectedPlayerItemBGIcon = ServiceLocator.Get<UIService>().UnselectedPlayerItemBGIcon;

        PlayerController.Initialize(
            playerView, 
            contentHolder, 
            itemPrefab, 
            shopView, 
            shopDatabaseSO,
            icon,
            type,
            rarity,
            itemName,
            description,
            bagWight,
            money,
            selectedPlayerItemBGIcon,
            unselectedPlayerItemBGIcon);
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
