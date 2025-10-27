using TMPro;
using UnityEngine;

public class ShopController
{
    private GameService gameService;
    private UIService uiService;

    private ShopView shopView;

    private ShopDatabaseSO shopDatabase;
    private GameObject[] tabItems;
    private TextMeshProUGUI[] tabName;

    public Color activeColor = new Color32(220, 219, 218, 225);
    public Color inactiveColor = new Color32(115, 115, 115, 225);

    public ShopController(GameService gameService, UIService uiService)
    {
        this.uiService = uiService;
        this.gameService = gameService;


        Initialize();
    }

    public void Initialize()
    {
        shopDatabase = gameService.ShopDatabase;
        shopView = gameService.ShopView;
        tabItems = uiService.TabItems;
        tabName = uiService.TabNames;

        shopView.Initialize(this, uiService, shopDatabase);

        FillTabs();
        shopView.Reset();
    }

    public void FillTabs()
    {
        for (int i = 0; i < tabItems.Length; i++)
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
        foreach (var button in tabName)
        {
            if (button != null)
                button.color = inactiveColor;
        }

        foreach (var panel in tabItems)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (GetTabsButtonByID(tabID) != null) GetTabsButtonByID(tabID).color = activeColor;

        if (GetTabPanelByID(tabID) != null) GetTabPanelByID(tabID).SetActive(true);
    }

    public GameObject GetTabPanelByID(int tabID)
    {
        if (tabID < 0 || tabID >= tabItems.Length)
            return null;
        return tabItems[tabID];
    }

    public TextMeshProUGUI GetTabsButtonByID(int tabID)
    {
        if (tabID < 0 || tabID >= tabName.Length)
            return null;
        return tabName[tabID];
    }
}
