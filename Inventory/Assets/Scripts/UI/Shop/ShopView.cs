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
        ServiceLocator.Get<EventService>().OnClikedIShopItem += SelectShopItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem += UnSelectAnotherItem;
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnClikedIShopItem -= SelectShopItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem += UnSelectAnotherItem;
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
                    TabView tabView = Instantiate(ShopController.GetShopItemPrefab(), contentHolder.transform).GetComponent<TabView>();
                    itemInstanceByName[item.itemName] = tabView.gameObject;
                    tabView.Initialize(this, item, GameService.TabType.Shop);
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
        view.itemBGIconGameobject.sprite = ServiceLocator.Get<UIService>().SelectedShopItemBGIcon;
    }

    private void UnSelectAnotherItem(TabView view)
    {
        foreach (var item in itemInstanceByName.Values)
        {
            Sprite unselectedSprite = view.itemBGIconGameobject.sprite;

            TabView tabView = item.GetComponent<TabView>();
            if (tabView != null && tabView != view)
            {
                tabView.itemBGIconGameobject.sprite = unselectedSprite;
            }
        }
    }
}
