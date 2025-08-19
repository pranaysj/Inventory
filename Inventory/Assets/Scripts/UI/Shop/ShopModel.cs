using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopModel
{
    private ShopDatabaseSO shopDataBase;
    private ShopController shopController;

    private GameObject[] tabPanels;
    private TextMeshProUGUI[] tabsButton;
    private GameObject shopItemPrefab;

    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI weight;
    private TextMeshProUGUI buyingPrice;

    public ShopDatabaseSO ShopDataBase => shopDataBase;
    public ShopController ShopController => shopController;

    public Color activeColor = new Color32(220, 219, 218, 225);
    public Color inactiveColor = new Color32(115, 115, 115, 225);


    public ShopModel(ShopController shopController, ShopDatabaseSO shopDataBase)
    {
        this.shopController = shopController;
        this.shopDataBase = shopDataBase;
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
        this.tabsButton = tabsButtons;
        this.tabPanels = tabsPanels;
        this.shopItemPrefab = shopItemPrefab;
        this.icon = icon;
        this.itemName = itemName;
        this.description = description;
        this.weight = weight;
        this.buyingPrice = buyingPrice;
    }

    public TextMeshProUGUI[] GetTabButtonList()
    {
        if (tabsButton == null)
            return null;
        return tabsButton;
    }
    public TextMeshProUGUI GetTabButton(int tabID)
    {
        if (tabID < 0 || tabID >= tabsButton.Length)
            return null;
        return tabsButton[tabID];
    }

    public GameObject[] GetTabPanelsList()
    {
        if (tabPanels == null)
            return null;
        return tabPanels;
    }
    public GameObject GetTabPanel(int tabID)
    {
        if (tabID < 0 || tabID >= tabPanels.Length)
            return null;
        return tabPanels[tabID];
    }
    public GameObject GetShopItemPrefab()
    {
        return shopItemPrefab;
    }
    public void SetItemInfo(string name)
    {
        foreach (var item in shopDataBase.shopItems)
        {
            if (item.itemName == name)
            {
                icon.sprite = item.icon;
                itemName.text = item.itemName;
                description.text = item.description;
                weight.text = item.weight.ToString() + " kg";
                buyingPrice.text = item.buyingPrice.ToString() + " G";
                return;
            }
        }
        Debug.LogWarning("Item not found: " + name);
    }
}