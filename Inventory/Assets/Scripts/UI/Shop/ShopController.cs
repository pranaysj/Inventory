using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static GameService;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;

    public ShopModel ShopModel => shopModel;
    public ShopView ShopView => shopView;

    public ShopController()
    {
        shopModel = new ShopModel(this);
    }

    public void Initialize(
        ShopView shopView,
        ShopDatabaseSO database,
        TextMeshProUGUI[] tabsButtons,
        GameObject[] tabsPanels,
        GameObject shopItemPrefab,
        Image icon,
        TextMeshProUGUI itemName,
        TextMeshProUGUI description,
        TextMeshProUGUI weight,
        TextMeshProUGUI transaction,
        TextMeshProUGUI price,
        Sprite selectedShopItemBGIcon,
        Sprite unselectedShopItemBGIcon)
    {
        this.shopView = shopView;
        ShopView.Initialize(this);

        ShopModel.Initialize(
            database, 
            tabsButtons, 
            tabsPanels, 
            shopItemPrefab, 
            icon, 
            itemName, 
            description, 
            weight, 
            transaction, 
            price,
            selectedShopItemBGIcon,
            unselectedShopItemBGIcon);

        Reset();
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

        if (ShopModel.GetTabPanelbyID(tabID) != null)
            ShopModel.GetTabPanelbyID(tabID).SetActive(true);
    }

    public GameObject[] GetTabsPanelList()
    {
        return ShopModel.GetTabPanelsList();
    }
    public GameObject GetTabPanelbyID(int tabID)
    {
        return ShopModel.GetTabPanelbyID(tabID);
    }
    public TextMeshProUGUI[] GetTabsButton()
    {
        return ShopModel.GetTabButtonList();
    }
    public ShopDatabaseSO GetShopDatabase()   //List of ScriptableObjects
    {
        return ShopModel.ShopDataBase;
    }
    public GameObject GetShopItemPrefab()
    {
        return ShopModel.GetShopItemPrefab();
    }
    public void SetItemInfo(TabType tabType, string name)
    {
        foreach (var item in GetShopDatabase().shopItems)
        {
            if (item.itemName == name)
            {
                ShopModel.Icon.sprite = item.icon;
                ShopModel.ItemName.text = item.itemName;
                ShopModel.Description.text = item.description;
                ShopModel.Weight.text = item.weight.ToString() + " kg";

                switch (tabType)
                {
                    case TabType.Shop:
                        ShopModel.Transaction.text = "Buying Price";
                        ShopModel.Price.text = item.buyingPrice.ToString() + " G";
                        break;

                    case TabType.Player:
                        ShopModel.Transaction.text = "Selling Price";
                        ShopModel.Price.text = item.sellingPrice.ToString() + " G";
                        break;
                }

                return;
            }
        }
        UnityEngine.Debug.LogWarning("Item not found: " + name);
    }
    private void Reset()
    {
        ShopModel.Icon.sprite = ShopModel.TempIcon.sprite;
        ShopModel.ItemName.text = "Name";
        ShopModel.Description.text = "Description";
    }

    public Sprite GetSelectedShopItemBGIcon => ShopModel.SelectedShopItemBGIcon;
    public Sprite GetUnselectedShopItemBGIcon => ShopModel.UnselectedShopItemBGIcon;

}
