using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopService
{
    private ShopController shopController;

    private TextMeshProUGUI[] tabsButton;
    private GameObject[] tabPanels;
    private GameObject shopItemPrefab;

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
        
        shopController.Initialize(tabsButton, tabPanels, shopItemPrefab);
    }

    public void Switch(int tabID)
    {
        shopController.Switch(tabID);
    }
}
