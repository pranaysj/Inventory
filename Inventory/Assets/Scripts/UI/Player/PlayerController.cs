using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController
{
    private UIService uiService;    
    private GameService gameService;

    private PlayerModel playerModel;
    public PlayerModel PlayerModel => playerModel;

    private PlayerView playerView;
    public PlayerView PlayerView => playerView;
    private ShopView shopView;

    private GameObject itemPrefab;
    private GameObject contentHolder;

    private ShopItemSO tempItem;
    private ShopDatabaseSO shopDatabaseSO;

    private Dictionary<string, int> itemsCountByName = new Dictionary<string, int>();
    private Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();

    public PlayerController(PlayerView playerView, UIService uiService, GameService gameService)
    {
        playerModel = new PlayerModel();
        this.playerView = playerView;
        this.uiService = uiService;
        this.gameService = gameService;

        Initialize();
        PlayerView.Initialize(this, uiService);
    }
    public void Initialize()
    {
        playerView.Reset();

        //NEW approach to get prefab from GameService
        itemPrefab = gameService.PlayerItemPrefab;
        shopDatabaseSO = gameService.ShopDatabase;
        shopView = gameService.ShopView;

        contentHolder = uiService.Content;
    }
    public void SpawnItemInPlayerInventory(ShopItemSO tempItem)
    {
        string itemName = tempItem.itemName;
        int itemQuantity = itemsCountByName.ContainsKey(itemName) ? itemsCountByName[itemName] : 0;

        if (IsItemAlreadyInInventory(itemName))
        {
            itemsCountByName[itemName]++;

            TabView tabView = itemInstanceByName.TryGetValue(itemName, out GameObject itemInstance)
                ? itemInstance.GetComponent<TabView>()
                : null;

            tabView?.ItemsQuantity(itemsCountByName[itemName]);

            var bagWeight = PlayerModel.UpdateBagWeight(tempItem.weight);
            PlayerView.UpdateBagWeight(bagWeight);
        }
        else
        {
            itemsCountByName.Add(itemName, 1);

            GameObject itemInstance = PlayerView.InstantiateItem();
            TabView tabView = itemInstance.GetComponent<TabView>();

            itemInstanceByName.Add(itemName, itemInstance);

            PlayerView.UpdateBagWeight(tempItem.weight);
            PlayerView.UpdateMoney();

            tabView.Initialize(shopView, tempItem, TabType.Player);
            tabView.ItemsQuantity(itemsCountByName[itemName]);
        }
    }

    private bool IsItemAlreadyInInventory(string itemName)
    {
        return itemsCountByName.ContainsKey(itemName);
    }
    public ShopItemSO GetItemData()
    {
        int randomIndex = UnityEngine.Random.Range(0, shopDatabaseSO.shopItems.Count);
        ShopItemSO itemData = shopDatabaseSO.shopItems[randomIndex];
        return itemData;
    }
    public void SellItem(TabView selectedItemView, int quantity, int sellingPrice, int grossWeight)
    {
        if (selectedItemView == null)
        {
            Debug.LogWarning("No item selected to sell.");
            return;
        }
        string itemName = selectedItemView.ItemName;
        if (itemsCountByName.ContainsKey(itemName))
        {
            itemsCountByName[itemName] -= quantity;
            if (itemsCountByName[itemName] <= 0)
            {
                itemsCountByName.Remove(itemName);
                if (itemInstanceByName.TryGetValue(itemName, out GameObject itemInstance))
                {
                    GameObject.Destroy(itemInstance);
                    itemInstanceByName.Remove(itemName);
                }
            }
            else
            {
                TabView tabView = itemInstanceByName.TryGetValue(itemName, out GameObject itemInstance)
                    ? itemInstance.GetComponent<TabView>()
                    : null;
                tabView?.ItemsQuantity(itemsCountByName[itemName]);
            }
            SetMoney(GetMoney() + sellingPrice);
            PlayerView.UpdateBagWeight(-grossWeight);
            PlayerView.UpdateMoney();
        }
    }
    internal void BuyItem(TabView selectedItemView, int quantity, int buyingPrice, int grossWeight)
    {
        string itemName = selectedItemView.nameGameobject.text;
        ShopItemSO itemData = GetItemScriptableObject(itemName);
        if (GetMoney() >= buyingPrice && (GetWeight() + grossWeight) <= GetMaxWeight())
        {
            for (int i = 0; i < quantity; i++)
            {
                SetMoney(GetMoney() - buyingPrice);
                SpawnItemInPlayerInventory(itemData);
            }
        }
        else
        {
            if (GetMoney() < buyingPrice)
            {
                Debug.LogWarning("Not enough money to buy the item.");
            }
            if ((GetWeight() + grossWeight) > GetMaxWeight())
            {
                Debug.LogWarning("Not enough bag weight capacity to carry the item.");
            }
        }
    }
    private ShopItemSO GetItemScriptableObject(string itemName)
    {
        foreach (var item in shopDatabaseSO.shopItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        Debug.LogWarning("Item not found: " + itemName);
        return null;
    }
    public Dictionary<string, GameObject> GetItemInstanceByName()
    {
        return itemInstanceByName;
    }
    public GameObject GetContentHolder()
    {
        return contentHolder;
    }
    public GameObject GetItemPrefab()
    {
        return itemPrefab;
    }
   
    public int GetMoney()
    {
        return playerModel.Money;
    }
    public void SetMoney(int value)
    {
        var moneyTxtGO = uiService.Money.gameObject;
        NumberCounter numberCounter = moneyTxtGO.GetComponent<NumberCounter>();
        if (numberCounter != null)
        {
            numberCounter.Value = value;
            playerModel.Money = value;
        }
        else
        {
            playerModel.Money = value;
            moneyTxtGO.GetComponent<TextMeshProUGUI>().text = value.ToString();
        }
    }
    public int GetWeight()
    {
        return playerModel.Weight;
    }
    public void SetWeight(int value)
    {
        playerModel.Weight = value;
    }
    public int GetMaxWeight()
    {
        return playerModel.MaxWeight;
    }

    public void GetTemporaryItemInPanel()
    {
        tempItem = playerView.GetTemporaryItemInPanel();
    }
    public void GetTempItemInPlayerInventory()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (tempItem == null) return;

        int nextWeight = GetWeight() + tempItem.weight;

        if (tempItem != null && nextWeight < GetMaxWeight())
        {
            SpawnItemInPlayerInventory(tempItem);
            tempItem = null;
            playerView.Reset();
        }
    }
}
