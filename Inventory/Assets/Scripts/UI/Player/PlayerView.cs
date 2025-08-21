using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    private void Start()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem += GetItemData;
    }

    public void Initialize(PlayerController playerController)
    {
        this.playerController = playerController;   
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnPlayerGetItem -= GetItemData;
    }

    private void GetItemData()
    {
        GameObject contentHolder = ServiceLocator.Get<UIService>().Content;

        GameObject itemPrefab = ServiceLocator.Get<GameService>().PlayerItemPrefab;

        ShopView shopView = ServiceLocator.Get<GameService>().ShopView;

        ShopDatabaseSO shopDatabaseSO = ServiceLocator.Get<GameService>().ShopDatabase;
        int randomIndex = Random.Range(0, shopDatabaseSO.shopItems.Count);
        ShopItemSO itemData = shopDatabaseSO.shopItems[randomIndex];

        GameObject itemInstance = Instantiate(itemPrefab, contentHolder.transform);
        TabView tabView = itemInstance.GetComponent<TabView>();

        tabView.Initialize(shopView, itemData);
    }

    private void SpawnItemInPlayerInventory()
    {
        // spawn Item in player inventory
    }
}
