using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TabView : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI nameGameobject;
    public Image iconGameobject;
    public TextMeshProUGUI rarityGameobject;
    public TextMeshProUGUI buyingPriceGameobject;

    private ItemType itemType;
    private string itemName;
    private Sprite icon;
    private string description;
    private int buyingPrice;
    private int sellingPrice;
    private int weight;
    private Rarity rarity;

    private ShopView shopView;

    public void Initialize(ShopView shopView, ShopItemSO itemSO)
    {
        this.shopView = shopView;
        AssignData(itemSO);
    }
    private void AssignData(ShopItemSO itemSO)
    {
        if(nameGameobject) nameGameobject.text = itemSO.itemName;
        if (iconGameobject != null) iconGameobject.sprite = itemSO.icon;
        if (rarityGameobject) rarityGameobject.text = itemSO.rarity.ToString();
        if (buyingPriceGameobject) buyingPriceGameobject.text = itemSO.buyingPrice.ToString() + " G";

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
            shopView.FillItemInfo(itemName);
    }
}
