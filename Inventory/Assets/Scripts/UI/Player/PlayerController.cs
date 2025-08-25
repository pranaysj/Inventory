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

    private GameObject moneyGameObject;

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
        TextMeshProUGUI money,
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
            money,
            selectedPlayerItemBGIcon,
            unselectedPlayerItemBGIcon);

        tempIcon = icon.sprite;
        Reset();

        this.moneyGameObject = money.gameObject;
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
            PlayerView.UpdateMoney();

            tabView.Initialize(GetShopView(), tempItem, TabType.Player);
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
            SetMoney(GetMoney() - buyingPrice);
            for (int i = 0; i < quantity; i++)
            {
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
        foreach (var item in GetShopDatabaseSO().shopItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        Debug.LogWarning("Item not found: " + itemName);
        return null;
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
        NumberCounter numberCounter = moneyGameObject.GetComponent<NumberCounter>();
        if (numberCounter != null)
        {
            numberCounter.Value = value;
            playerModel.Money = value;
        }
        else
        {
            playerModel.Money = value;
            moneyGameObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
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
    public TextMeshProUGUI GetBagWight()
    {
        return playerModel.BagWeight;
    }
    public TextMeshProUGUI GetMonkeyText()
    {
        return playerModel.MoneyText;
    }
    public Sprite GetSelectedPlayerItemBGIcon => playerModel.SelectedPlayerItemBGIcon;
    public Sprite GetUnselectedPlayerItemBGIcon => playerModel.UnselectedPlayerItemBGIcon;
}
