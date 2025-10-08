using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private GameService gameService;
    private UIService uiService;
    private ShopDatabaseSO shopDatabase;

    public ShopView ShopView => shopView;

    public Color activeColor = new Color32(220, 219, 218, 225);
    public Color inactiveColor = new Color32(115, 115, 115, 225);

    public ShopController(ShopDatabaseSO shopDatabase, GameService gameService, UIService uiService)
    {
        this.shopDatabase = shopDatabase;
        this.uiService = uiService;
        this.gameService = gameService;

    }

    public void Initialize(
        ShopView shopView,
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
        this.shopView = shopView;
        ShopView.Initialize(this, uiService, shopDatabase);

        FillTabs();
        Reset();
    }

    public void FillTabs()
    {
        for (int i = 0; i < GetTabsPanelList().Length; i++)
        {
            GameObject contentHolder = GameObjectExtensions.FindChildOfChildByName(GetTabPanelByID(i), "Content");

            foreach (var item in shopDatabase.shopItems)
            {
                if (item.itemType == (ItemType)i)
                {
                    var prefab = gameService.ShopItemPrefab;
                    if (prefab == null)
                    {
                        Debug.LogError("Shop Item Prefab is not assigned in GameService.");
                    }
                    var gameobject = GameObject.Instantiate(prefab, contentHolder.transform);
                    TabView tabView = gameobject.GetComponent<TabView>();

                    shopView.itemInstanceByName[item.itemName] = tabView.gameObject;
                    tabView.Initialize(shopView, item, TabType.Shop);
                }
            }
        }
    }

    public void Switch(int tabID)
    {
        foreach (var button in uiService.TabNames)
        {
            if (button != null)
                button.color = inactiveColor;
        }

        foreach (var panel in uiService.TabItems)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (GetTabsButtonByID(tabID) != null)
            GetTabsButtonByID(tabID).color = activeColor;

        if (GetTabPanelByID(tabID) != null)
            GetTabPanelByID(tabID).SetActive(true);
    }

    public GameObject[] GetTabsPanelList()
    {
        return uiService.TabItems;
    }

    public GameObject GetTabPanelByID(int tabID)
    {
        GameObject[] tabPanels = uiService.TabItems;

        if (tabID < 0 || tabID >= tabPanels.Length)
            return null;
        return tabPanels[tabID];
    }
    public TextMeshProUGUI GetTabsButtonByID(int tabID)
    {
        TextMeshProUGUI[] tabsButton = uiService.TabNames;

        if (tabID < 0 || tabID >= tabsButton.Length)
            return null;
        return tabsButton[tabID];
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

    public void UnSelectAnotherItem(TabView view)
    {
        ShopView.UnSelectAnotherItem(view);
    }

    private void Reset()
    {
        uiService.Icon.sprite = uiService.Icon.sprite;
        uiService.ItemName.text = "Name";
        uiService.Description.text = "Description";
    }

    public Sprite GetSelectedShopItemBGIcon => uiService.SelectedShopItemBGIcon;
    public Sprite GetUnselectedShopItemBGIcon => uiService.UnselectedShopItemBGIcon;

}
