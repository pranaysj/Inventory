using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopService
{
    private ShopController shopController;
    private ShopView shopView;
    private GameService gameService;
    private UIService uiService;

    private ShopDatabaseSO shopDatabaseSO;

    private TextMeshProUGUI[] tabsButton;
    private GameObject[] tabPanels;
    private GameObject shopItemPrefab;

    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI weight;
    private TextMeshProUGUI transaction;
    private TextMeshProUGUI price;

    private Sprite selectedShopItemBGIcon;
    private Sprite unselectedShopItemBGIcon;

    public ShopService(ShopDatabaseSO shopDatabase, GameService gameService, UIService uIService, ShopView shopView)
    {
        //this.shopDatabaseSO = shopDatabase;
        this.uiService = uIService;
        this.gameService = gameService;
        this.shopView = shopView;

        shopController = new ShopController(shopDatabase, gameService, uIService);
        Initialize();
    }

    public void Initialize()
    {

        tabsButton = ServiceLocator.Get<UIService>().TabNames;
        tabPanels = ServiceLocator.Get<UIService>().TabItems;
        shopItemPrefab = ServiceLocator.Get<GameService>().ShopItemPrefab;

        icon = ServiceLocator.Get<UIService>().Icon;
        itemName = ServiceLocator.Get<UIService>().ItemName;
        description = ServiceLocator.Get<UIService>().Description;
        weight = ServiceLocator.Get<UIService>().Weight;
        transaction = ServiceLocator.Get<UIService>().Transaction;
        price = ServiceLocator.Get<UIService>().Price;

        selectedShopItemBGIcon = ServiceLocator.Get<UIService>().SelectedShopItemBGIcon;
        unselectedShopItemBGIcon = ServiceLocator.Get<UIService>().UnselectedShopItemBGIcon;

        shopController.Initialize(
            shopView,
            shopDatabaseSO,
            tabsButton,
            tabPanels,
            shopItemPrefab,
            icon,
            itemName,
            description,
            weight,
            transaction,
            price,
            selectedShopItemBGIcon,
            unselectedShopItemBGIcon);
    }

    public void Switch(int tabID)
    {
        shopController.Switch(tabID);
    }

    public ShopController ShopController => shopController;
}
