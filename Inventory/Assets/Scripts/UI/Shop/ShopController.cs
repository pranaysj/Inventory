using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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

    public void Initialize(
        TextMeshProUGUI[] tabsButtons,
        GameObject[] tabsPanels,
        GameObject shopItemPrefab,
        Image icon,
        TextMeshProUGUI itemName,
        TextMeshProUGUI description,
        TextMeshProUGUI weight,
        TextMeshProUGUI buyingPrice)
    {
        ShopView.Initialize(this);
        ShopModel.Initialize(tabsButtons, tabsPanels, shopItemPrefab, icon, itemName, description, weight, buyingPrice);
    }

    public void Switch(int tabID)
    {
        foreach (var button in ShopModel.GetTabButtonList())
        {
            if (button != null)
                button.color = ShopModel.inactiveColor;
        }

        foreach (var panel in ShopModel.GetTabPanelsList())
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (ShopModel.GetTabButton(tabID) != null)
            ShopModel.GetTabButton(tabID).color = ShopModel.activeColor;

        if (ShopModel.GetTabPanel(tabID) != null)
            ShopModel.GetTabPanel(tabID).SetActive(true);
    }

    public GameObject[] GetTabsPanelList()
    {
        return ShopModel.GetTabPanelsList();
    }
    public GameObject GetTabPanel(int tabID)
    {
        return ShopModel.GetTabPanel(tabID);
    }
    public TextMeshProUGUI[] GetTabsButton()
    {
        return ShopModel.GetTabButtonList();
    }
    public ShopDatabaseSO GetShopDatabase()
    {
        return ShopModel.ShopDataBase;
    }
    public GameObject GetShopItemPrefab()
    {
        return ShopModel.GetShopItemPrefab();
    }
    public void SetItemInfo(string name)
    {
        ShopModel.SetItemInfo(name);
    }
}
