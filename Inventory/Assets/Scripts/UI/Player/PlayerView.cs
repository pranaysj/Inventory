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

    private Dictionary<string, int> itemsCountByName = new Dictionary<string, int>();
    private Dictionary<string, GameObject> itemInstanceByName = new Dictionary<string, GameObject>();

    private Image icon;
    private TextMeshProUGUI type;
    private TextMeshProUGUI rarity;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;
    
    public Sprite tempIcon;
    private ShopItemSO tempItem;

    private void Start()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem += GetTemporaryItemInPanel;
        ServiceLocator.Get<EventService>().OnClikedIPlayerItem += SelectPlayerItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem += UnSelectAnotherItem;
    }

    public void Initialize(PlayerController playerController)
    {
        this.playerController = playerController;

        contentHolder = ServiceLocator.Get<UIService>().Content;
        itemPrefab = ServiceLocator.Get<GameService>().PlayerItemPrefab;
        shopView = ServiceLocator.Get<GameService>().ShopView;
        shopDatabaseSO = ServiceLocator.Get<GameService>().ShopDatabase;

        icon = ServiceLocator.Get<UIService>().IconR;
        type = ServiceLocator.Get<UIService>().TypeR;
        rarity = ServiceLocator.Get<UIService>().RarityR;
        itemName = ServiceLocator.Get<UIService>().ItemNameR;
        description = ServiceLocator.Get<UIService>().DescriptionR;

        tempIcon = icon.sprite;
        Reset();
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem -= GetTemporaryItemInPanel;
        ServiceLocator.Get<EventService>().OnClikedIPlayerItem -= SelectPlayerItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem -= UnSelectAnotherItem;
    }

    private ShopItemSO GetItemData()
    {
        int randomIndex = Random.Range(0, shopDatabaseSO.shopItems.Count);
        ShopItemSO itemData = shopDatabaseSO.shopItems[randomIndex];
        return itemData;
    }

    private bool IsItemAlreadyInInventory(string itemName)
    {
        return itemsCountByName.ContainsKey(itemName);
    }

    private void SpawnItemInPlayerInventory(ShopItemSO tempItem)
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
        }
        else
        {
            itemsCountByName.Add(itemName, 1);

            GameObject itemInstance = Instantiate(itemPrefab, contentHolder.transform);
            TabView tabView = itemInstance.GetComponent<TabView>();

            itemInstanceByName.Add(itemName, itemInstance);

            tabView.Initialize(shopView, tempItem, GameService.TabType.Player);
            tabView.ItemsQuantity(itemsCountByName[itemName]);
        }
    }

    public void GetTemporaryItemInPanel()
    {
        tempItem = GetItemData();

        icon.sprite = tempItem.icon;
        type.text = tempItem.itemType.ToString();
        rarity.text = tempItem.rarity.ToString();
        itemName.text = tempItem.itemName;
        description.text = tempItem.description;

    }

    public void GetTempItemInPlayerInventory()
    {
        if (tempItem != null)
        {
            SpawnItemInPlayerInventory(tempItem);
            tempItem = null;
            Reset();
        }
    }

    private void Reset()
    {
        icon.sprite = tempIcon;
        type.text = "Item Type";
        rarity.text = "Rarity";
        itemName.text = "Name";
        description.text = "Description";
    }

    //make unselected from itmeInstance by name when click outside
    public void SelectPlayerItem(TabView view)
    {
        view.itemBGIconGameobject.sprite = ServiceLocator.Get<UIService>().SelectedPlayerItemBGIcon;
    }

    public void UnSelectAnotherItem(TabView view)
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
