using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameService;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    public ShopController ShopController => shopController;

    private Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();

    private GameObject shopItemPrefab;

    public void Initialize(ShopController controller)
    {
        this.shopController = controller;
    }
    private void Start()
    {
        shopItemPrefab = ServiceLocator.Get<GameService>().ShopItemPrefab;
        if (ShopController != null)
        {
            ShopController.Switch(0); // Switch to the first tab by default
            FillTabs();
        }


        ServiceLocator.Get<EventService>().OnClickedIShopItem += SelectShopItem;
        ServiceLocator.Get<EventService>().OnClickedAnotherItem += UnSelectAnotherItem;
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnClickedIShopItem -= SelectShopItem;
        ServiceLocator.Get<EventService>().OnClickedAnotherItem -= UnSelectAnotherItem;
    }
    public void FillTabs()
    {
        for (int i = 0; i < ShopController.GetTabsPanelList().Length; i++)
        {
            GameObject contentHolder = GameObjectExtensions.FindChildOfChildByName(ShopController.GetTabPanelbyID(i), "Content");
            
            foreach (var item in ShopController.GetShopDatabase().shopItems)
            {
                if (item.itemType == (ItemType)i)
                {
                    TabView tabView = Instantiate(shopItemPrefab, contentHolder.transform).GetComponent<TabView>();
                    itemInstanceByName[item.itemName] = tabView.gameObject;
                    tabView.Initialize(this, item, TabType.Shop);
                }
            }
        }
    }

    public void FillItemInfo(TabType tabType, string itemName)
    {
        ShopController.SetItemInfo(tabType, itemName);
    }

    public void SelectShopItem(TabView view)
    {
        view.itemBGIconGameobject.sprite = ShopController.GetSelectedShopItemBGIcon;
    }

    public void UnSelectAnotherItem(TabView view)
    {
        foreach (var item in itemInstanceByName.Values)
        {
            Sprite unselectedSprite = ShopController.GetUnselectedShopItemBGIcon;

            TabView tabView = item.GetComponent<TabView>();
            if (tabView != null && tabView != view)
            {
                tabView.itemBGIconGameobject.sprite = unselectedSprite;
            }
        }
    }
}
