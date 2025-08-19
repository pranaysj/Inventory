using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    public ShopController ShopController => shopController;

    public void Initialize(ShopController controller)
    {
        this.shopController = controller;
    }
    private void Start()
    {
        if (shopController != null)
        {
            shopController.Switch(0); // Switch to the first tab by default
            FillTabs();
        }
    }

    public void FillTabs()
    {
        for (int i = 0; i < ShopController.GetTabsPanelList().Length; i++)
        {
            GameObject contentHolder = GameObjectExtensions.FindChildOfChildByName(ShopController.GetTabPanel(i), "Content");
            
            foreach (var item in ShopController.GetShopDatabase().shopItems)
            {
                if (item.itemType == (ItemType)i)
                {
                    GameObject shopItem = Instantiate(ShopController.GetShopItemPrefab(), contentHolder.transform);
                    TabView tabView = shopItem.GetComponent<TabView>();

                    var name = tabView._name;
                    var icon = tabView.icon;
                    var rarity = tabView.rarity;
                    var buyingPrice = tabView.buyingPrice;

                    if (name) name.text = item.itemName;
                    if (icon != null) icon.sprite = item.icon;
                    if (rarity) rarity.text = item.rarity.ToString();
                    if (buyingPrice) buyingPrice.text = item.buyingPrice.ToString() + " G";
                }
            }
        }
    }
}
