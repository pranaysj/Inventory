using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameService;
using static UnityEditor.Progress;

public class TabView : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI nameGameobject;
    public Image iconGameobject;
    public TextMeshProUGUI rarityGameobject;
    public TextMeshProUGUI priceGameobject;
    public TextMeshProUGUI quantityGameobject;
    public Image itemBGIconGameobject;

    private TabType tabType;
    private ItemType itemType;
    private string itemName;
    private Sprite icon;
    private string description;
    private int buyingPrice;
    private int sellingPrice;
    private int weight;
    private Rarity rarity;
    private int quantity;

    private ShopView shopView;

    public TabType TabType => tabType;
    public string ItemName => itemName;
    public int Weight => weight;
    public int BuyingPrice => buyingPrice;
    public int SellingPrice => sellingPrice;
    public int Quantity => quantity;


    public void Initialize(ShopView shopView, ShopItemSO itemSO, TabType tabType)
    {
        this.shopView = shopView;
        AssignData(tabType, itemSO);
    }
    private void AssignData(TabType tabType, ShopItemSO itemSO)
    {
        if (nameGameobject) nameGameobject.text = itemSO.itemName;
        if (iconGameobject != null) iconGameobject.sprite = itemSO.icon;
        if (rarityGameobject) rarityGameobject.text = itemSO.rarity.ToString();
        if (priceGameobject) priceGameobject.text = itemSO.buyingPrice.ToString() + " G";

        this.tabType = tabType;
        itemType = itemSO.itemType;
        itemName = itemSO.itemName;
        icon = itemSO.icon;
        description = itemSO.description;
        buyingPrice = itemSO.buyingPrice;
        sellingPrice = itemSO.sellingPrice;
        weight = itemSO.weight;
        rarity = itemSO.rarity;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(itemName != null)
            shopView.FillItemInfo(tabType, itemName);

        ServiceLocator.Get<EventService>().ClickedItem(this);
    }

    public void ItemsQuantity(int quantity)
    {
        this.quantity = quantity;
        if (quantityGameobject) quantityGameobject.text = "x " + this.quantity.ToString();
    }

    internal ShopItemSO GetItemData()
    {
        ShopItemSO itemData = ScriptableObject.CreateInstance<ShopItemSO>();
        itemData.itemName = itemName;
        itemData.icon = icon;
        itemData.description = description;
        itemData.buyingPrice = buyingPrice;
        itemData.sellingPrice = sellingPrice;
        itemData.weight = weight;
        itemData.rarity = rarity;
        itemData.itemType = itemType;
        return itemData;
    }
}
