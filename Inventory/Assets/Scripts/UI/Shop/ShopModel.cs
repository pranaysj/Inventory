using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameService;

public class ShopModel
{
    private ShopDatabaseSO shopDataBase;
    private ShopController shopController;

    private GameObject[] tabPanels;
    private TextMeshProUGUI[] tabsButton;
    private GameObject shopItemPrefab;

    private Image tempIcon;
    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI weight;
    private TextMeshProUGUI transaction;
    private TextMeshProUGUI price;

    private Sprite selectedShopItemBGIcon;
    private Sprite unselectedShopItemBGIcon;

    public Image TempIcon => tempIcon;
    public Image Icon => icon;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI Description => description;
    public TextMeshProUGUI Weight => weight;
    public TextMeshProUGUI Transaction => transaction;
    public TextMeshProUGUI Price => price;


    public ShopDatabaseSO ShopDataBase => shopDataBase;
    public ShopController ShopController => shopController;

    public Sprite SelectedShopItemBGIcon => selectedShopItemBGIcon;
    public Sprite UnselectedShopItemBGIcon => unselectedShopItemBGIcon;

    public Color activeColor = new Color32(220, 219, 218, 225);
    public Color inactiveColor = new Color32(115, 115, 115, 225);

    public ShopModel(ShopController shopController)
    {
        this.shopController = shopController;
    }

    public void Initialize(
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
        this.shopDataBase = database;
        this.tabsButton = tabsButtons;
        this.tabPanels = tabsPanels;
        this.shopItemPrefab = shopItemPrefab;
        this.icon = icon;
        this.itemName = itemName;
        this.description = description;
        this.weight = weight;
        this.transaction = transaction;
        this.price = price;

        this.tempIcon = icon;

        this.selectedShopItemBGIcon = selectedShopItemBGIcon;
        this.unselectedShopItemBGIcon = unselectedShopItemBGIcon;
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
    public GameObject GetTabPanelbyID(int tabID)
    {
        if (tabID < 0 || tabID >= tabPanels.Length)
            return null;
        return tabPanels[tabID];
    }
    public GameObject GetShopItemPrefab()
    {
        return shopItemPrefab;
    }
}