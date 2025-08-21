using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameService;

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
        if (ShopController != null)
        {
            ShopController.Switch(0); // Switch to the first tab by default
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
                    TabView tabView = Instantiate(ShopController.GetShopItemPrefab(), contentHolder.transform).GetComponent<TabView>();
                    tabView.Initialize(this, item, GameService.TabType.Shop);
                }
            }
        }
    }

    public void FillItemInfo(TabType tabType, string itemName)
    {
        ShopController.SetItemInfo(tabType, itemName);
    }
}
