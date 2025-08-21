using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    private GameObject contentHolder;
    private GameObject itemPrefab;
    private ShopView shopView;
    private ShopDatabaseSO shopDatabaseSO;

    private Dictionary<string, int> playerItemTabs = new Dictionary<string, int>();
    private Dictionary<string, GameObject> prefabItemList = new Dictionary<string, GameObject>();

    private Image icon;
    private TextMeshProUGUI material;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;

    private void Start()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem += SpawnItemInPlayerInventory;
    }

    public void Initialize(PlayerController playerController)
    {
        this.playerController = playerController;

        contentHolder = ServiceLocator.Get<UIService>().Content;
        itemPrefab = ServiceLocator.Get<GameService>().PlayerItemPrefab;
        shopView = ServiceLocator.Get<GameService>().ShopView;
        shopDatabaseSO = ServiceLocator.Get<GameService>().ShopDatabase;

        icon = ServiceLocator.Get<UIService>().IconR;
        material = ServiceLocator.Get<UIService>().MaterialR;
        itemName = ServiceLocator.Get<UIService>().ItemNameR;
        description = ServiceLocator.Get<UIService>().DescriptionR;
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem -= SpawnItemInPlayerInventory;
    }

    private ShopItemSO GetItemData()
    {
        int randomIndex = Random.Range(0, shopDatabaseSO.shopItems.Count);
        ShopItemSO itemData = shopDatabaseSO.shopItems[randomIndex];
        return itemData;
    }

    private bool IsItemAlreadyInInventory(string itemName)
    {
        return playerItemTabs.ContainsKey(itemName);
    }

    private void SpawnItemInPlayerInventory()
    {
        string itemName = GetItemData().itemName;
        int itemQuantity = playerItemTabs.ContainsKey(itemName) ? playerItemTabs[itemName] : 0;

        if (IsItemAlreadyInInventory(itemName))
        {
            playerItemTabs[itemName]++;

            TabView tabView = prefabItemList.TryGetValue(itemName, out GameObject itemInstance) 
                ? itemInstance.GetComponent<TabView>() 
                : null;

            tabView?.ItemsQuantity(playerItemTabs[itemName]);
        }
        else
        {
            playerItemTabs.Add(itemName, 1);

            // Instantiate the item prefab inside the temporary content holder
            // pushing the item to plater inventory

            GameObject itemInstance = Instantiate(itemPrefab, contentHolder.transform);
            TabView tabView = itemInstance.GetComponent<TabView>();

            prefabItemList.Add(itemName, itemInstance);

            tabView.Initialize(shopView, GetItemData(), GameService.TabType.Player);
            tabView.ItemsQuantity(playerItemTabs[itemName]);
        }
    }

    public void TemporaryItemPanel()
    {
        
    }
}
