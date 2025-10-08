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
    private UIService uiService;
    private ShopDatabaseSO shopDatabase;
    public ShopController ShopController => shopController;

    public Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();

    private GameObject shopItemPrefab;

    public void Initialize(ShopController controller, UIService uiService, ShopDatabaseSO shopDatabase)
    {
        this.shopController = controller;
        this.uiService = uiService;
        this.shopDatabase = shopDatabase;
    }
    private void Start()
    {
        shopItemPrefab = ServiceLocator.Get<GameService>().ShopItemPrefab;

        if (ShopController != null)
        {
            ShopController.Switch(0); // Switch to the first tab by default
            //FillTabs();
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
