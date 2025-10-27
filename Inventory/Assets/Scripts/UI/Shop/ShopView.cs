using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameService;

public class ShopView : MonoBehaviour
{
    private UIService uiService;
    private EventService eventService;

    private ShopController shopController;
    private GameService gameService;

    public ShopController ShopController => shopController;

    private GameObject shopItemPrefab;
    private ShopDatabaseSO shopDatabase;

    public Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();

    private void Start()
    {
        eventService = ServiceLocator.Get<EventService>();

        if (ShopController != null)
        {
            ShopController.Switch(0); // Switch to the first tab by default
            //FillTabs();
        }

        eventService.OnClickedIShopItem += SelectShopItem;
        eventService.OnClickedAnotherItem += UnSelectAnotherItem;
    }

    public void Initialize(ShopController shopController, GameService gameService, UIService uiService)
    {
        this.shopController = shopController;
        this.gameService = gameService;
        this.uiService = uiService;

        shopDatabase = gameService.ShopDatabase;
        shopItemPrefab = gameService.ShopItemPrefab;
    }

    private void OnDestroy()
    {
        eventService.OnClickedIShopItem -= SelectShopItem;
        eventService.OnClickedAnotherItem -= UnSelectAnotherItem;
    }

    public void FillTabs()
    {
        for (int i = 0; i < uiService.TabItems.Length; i++)
        {
            GameObject contentHolder = GameObjectExtensions.FindChildOfChildByName(ShopController.GetTabPanelByID(i), "Content");
            
            foreach (var item in shopDatabase.shopItems)
            {
                if (item.itemType == (ItemType)i)
                {
                    if(shopItemPrefab == null)
                    {
                        Debug.LogError("Shop Item Prefab is not assigned in GameService.");
                    }
                    var gameobject = Instantiate(shopItemPrefab, contentHolder.transform);
                    TabView tabView = gameobject.GetComponent<TabView>();
                   
                    itemInstanceByName[item.itemName] = tabView.gameObject;
                    tabView.Initialize(this, item, TabType.Shop);
                }
            }
        }
    }

    public void FillItemInfo(TabType tabType, string itemName)
    {
        SetItemInfo(tabType, itemName);
        foreach (var item in shopDatabase.shopItems)
        {
            if (item.itemName == itemName)
            {
                uiService.Icon.sprite = item.icon;
                uiService.ItemName.text = item.itemName;
                uiService.Description.text = item.description;
                uiService.Weight.text = item.weight.ToString() + " kg";

                switch (tabType)
                {
                    case TabType.Shop:
                        uiService.Transaction.text = "Buying Price";
                        uiService.Price.text = item.buyingPrice.ToString() + " G";
                        break;

                    case TabType.Player:
                        uiService.Transaction.text = "Selling Price";
                        uiService.Price.text = item.sellingPrice.ToString() + " G";
                        break;
                }
                return;
            }
        }
        Debug.LogWarning("Item not found: " + name);
    }

    public void SelectShopItem(TabView view)
    {
        view.itemBGIconGameobject.sprite = uiService.SelectedShopItemBGIcon;
    }

    public void UnSelectAnotherItem(TabView view)
    {
        foreach (var item in itemInstanceByName.Values)
        {
            Sprite unselectedSprite = uiService.UnselectedShopItemBGIcon;

            TabView tabView = item.GetComponent<TabView>();
            if (tabView != null && tabView != view)
            {
                tabView.itemBGIconGameobject.sprite = unselectedSprite;
            }
        }
    }

    public void SetItemInfo(TabType tabType, string name)
    {
        foreach (var item in shopDatabase.shopItems)
        {
            if (item.itemName == name)
            {
                uiService.Icon.sprite = item.icon;
                uiService.ItemName.text = item.itemName;
                uiService.Description.text = item.description;
                uiService.Weight.text = item.weight.ToString() + " kg";

                switch (tabType)
                {
                    case TabType.Shop:
                        uiService.Transaction.text = "Buying Price";
                        uiService.Price.text = item.buyingPrice.ToString() + " G";
                        break;

                    case TabType.Player:
                        uiService.Transaction.text = "Selling Price";
                        uiService.Price.text = item.sellingPrice.ToString() + " G";
                        break;
                }

                return;
            }
        }
        UnityEngine.Debug.LogWarning("Item not found: " + name);
    }

    public void Reset()
    {
        uiService.Icon.sprite = uiService.Icon.sprite;
        uiService.ItemName.text = "Name";
        uiService.Description.text = "Description";
    }
}
