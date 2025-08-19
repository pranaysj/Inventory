using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopService
{
    private ShopController shopController;

    private TextMeshProUGUI[] tabsButton;
    private GameObject[] tabPanels;
    private GameObject shopItemPrefab;

    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI weight;
    private TextMeshProUGUI buyingPrice;

    public ShopService(ShopView shopView, ShopDatabaseSO shopDatabase)
    {
        shopController = new ShopController(this, shopView, shopDatabase);
        Initialize();
    }

    public void Initialize()
    {
        tabsButton = ServiceLocator.Get<GameService>().TabButton;
        tabPanels = ServiceLocator.Get<GameService>().TabPanel;
        shopItemPrefab = ServiceLocator.Get<GameService>().ShopItemPrefab;

        icon = ServiceLocator.Get<GameService>().Icon;
        itemName = ServiceLocator.Get<GameService>().ItemName;
        description = ServiceLocator.Get<GameService>().Description;
        weight = ServiceLocator.Get<GameService>().Weight;
        buyingPrice = ServiceLocator.Get<GameService>().BuyingPrice;

        shopController.Initialize(tabsButton, tabPanels, shopItemPrefab, icon, itemName, description, weight, buyingPrice);
    }

    public void Switch(int tabID)
    {
        shopController.Switch(tabID);
    }
}
