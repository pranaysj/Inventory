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
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        ShopController.SetItemInfo(tabType, itemName);
=======
        foreach (var item in gameService.ShopDatabase.shopItems)
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
>>>>>>> Stashed changes
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
