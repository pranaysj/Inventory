using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShopController
{
    private ShopService shopService;
    private ShopModel shopModel;
    private ShopView shopView;

    public ShopService ShopService => shopService;
    public ShopModel ShopModel => shopModel;
    public ShopView ShopView => shopView;

    public ShopController(ShopService service, ShopView view, ShopDatabaseSO database)
    {
        shopService = service;
        shopView = view;
        shopModel = new ShopModel(this, database);
    }

    public void Initialize(TextMeshProUGUI[] tabsButtons, GameObject[] tabsPanels, GameObject shopItemPrefab)
    {
        shopView.Initialize(this);
        shopModel.Initialize(tabsButtons, tabsPanels, shopItemPrefab);
    }

    public void Switch(int tabID)
    {
        foreach (var button in shopModel.GetTabButtonList())
        {
            if (button != null)
                button.color = shopModel.inactiveColor;
        }

        foreach (var panel in shopModel.GetTabPanelsList())
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (shopModel.GetTabButton(tabID) != null)
            shopModel.GetTabButton(tabID).color = shopModel.activeColor;

        if (shopModel.GetTabPanel(tabID) != null)
            shopModel.GetTabPanel(tabID).SetActive(true);
    }

    public GameObject[] GetTabsPanelList()
    {
        return shopModel.GetTabPanelsList();
    }
    public GameObject GetTabPanel(int tabID)
    {
        return shopModel.GetTabPanel(tabID);
    }
    public TextMeshProUGUI[] GetTabsButton()
    {
        return shopModel.GetTabButtonList();
    }
    public ShopDatabaseSO GetShopDatabase()
    {
        return shopModel.ShopDataBase;
    }
    public GameObject GetShopItemPrefab()
    {
        return shopModel.GetShopItemPrefab();
    }
}
