using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public PlayerModel PlayerModel => playerModel;
    public PlayerView PlayerView => playerView;

    public Sprite tempIcon;

    private Dictionary<string, int> itemsCountByName = new Dictionary<string, int>();
    private Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();


    public PlayerController()
    {
        playerModel = new PlayerModel(this);

    }
    public void Initialize(
        PlayerView playerView, 
        GameObject contentHolder, 
        GameObject itemPrefab, 
        ShopView shopView, 
        ShopDatabaseSO shopDatabaseSO, 
        Image icon, 
        TextMeshProUGUI type, 
        TextMeshProUGUI rarity, 
        TextMeshProUGUI itemName, 
        TextMeshProUGUI description,
        TextMeshProUGUI bagWeight,
        Sprite selectedPlayerItemBGIcon,
        Sprite unselectedPlayerItemBGIcon)
    {
        this.playerView = playerView;
        PlayerView.Initialize(this);

        PlayerModel.Initialize(
            contentHolder,
            itemPrefab,
            shopView,
            shopDatabaseSO,
            icon,
            type,
            rarity,
            itemName,
            description,
            bagWeight,
            selectedPlayerItemBGIcon,
            unselectedPlayerItemBGIcon);

        tempIcon = icon.sprite;
        Reset();
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

            PlayerView.UpdateBagWeight(tempItem.weight);
        }
        else
        {
            itemsCountByName.Add(itemName, 1);

            GameObject itemInstance = PlayerView.InstantiateItem();
            TabView tabView = itemInstance.GetComponent<TabView>();

            itemInstanceByName.Add(itemName, itemInstance);

            PlayerView.UpdateBagWeight(tempItem.weight);

            tabView.Initialize(GetShopView(), tempItem, GameService.TabType.Player);
            tabView.ItemsQuantity(itemsCountByName[itemName]);
        }
    }
    private bool IsItemAlreadyInInventory(string itemName)
    {
        return itemsCountByName.ContainsKey(itemName);
    }

    public ShopItemSO GetItemData()
    {
        int randomIndex = UnityEngine.Random.Range(0, GetShopDatabaseSO().shopItems.Count);
        ShopItemSO itemData = GetShopDatabaseSO().shopItems[randomIndex];
        return itemData;
    }

    public void Reset()
    {
        GetIcon().sprite = tempIcon;
        GetItemType().text = "Item Type";
        GetRarity().text = "Rarity";
        GetItemName().text = "Name";
        GetDescription().text = "Description";
    }

    public Dictionary<string, GameObject> GetItemInstanceByName()
    {
        return itemInstanceByName;
    }
    public GameObject GetContentHolder()
    {
        return playerModel.ContentHolder;
    }
    public GameObject GetItemPrefab()
    {
        return playerModel.ItemPrefab;
    }
    public ShopView GetShopView()
    {
        return playerModel.ShopView;
    }
    public ShopDatabaseSO GetShopDatabaseSO()
    {
        return playerModel.ShopDatabaseSO;
    }
    public Image GetIcon()
    {
        return playerModel.Icon;
    }
    public TextMeshProUGUI GetItemType()
    {
        return playerModel.Type;
    }
    public TextMeshProUGUI GetRarity()
    {
        return playerModel.Rarity;
    }
    public TextMeshProUGUI GetItemName()
    {
        return playerModel.ItemName;
    }
    public TextMeshProUGUI GetDescription()
    {
        return playerModel.Description;
    }
    public int GetMoney()
    {
        return playerModel.Money;
    }
    public void SetMoney(int value)
    {
        playerModel.Money = value;
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
    public TextMeshProUGUI GetBagWight()
    {
        return playerModel.BagWeight;
    }
    public Sprite GetSelectedPlayerItemBGIcon => playerModel.SelectedPlayerItemBGIcon;
    public Sprite GetUnselectedPlayerItemBGIcon => playerModel.UnselectedPlayerItemBGIcon;
}
