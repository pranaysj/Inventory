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
    private UIService uIService;
    private GameService gameService;
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

    public PlayerService(UIService uiService, GameService gameService, PlayerView playerView)
    {
        //this.uIService = uiService;
        //this.gameService = gameService;

        //this.shopDatabaseSO = shopDatabase;
        //this.shopView = shopView;
        
        this.playerView = playerView;

        playerController = new PlayerController(uiService, gameService);

        Initialize();
    }
    public void Initialize()
    {
        UIService uIService = ServiceLocator.Get<UIService>();
        itemPrefab = ServiceLocator.Get<GameService>().PlayerItemPrefab;

        contentHolder = uIService.Content;

        icon = uIService.IconR;
        type = uIService.TypeR;
        rarity = uIService.RarityR;
        itemName = uIService.ItemNameR;
        description = uIService.DescriptionR;

        bagWight = uIService.BagWeight;
        money = uIService.Money;

        selectedPlayerItemBGIcon = uIService.SelectedPlayerItemBGIcon;
        unselectedPlayerItemBGIcon = uIService.UnselectedPlayerItemBGIcon;

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
