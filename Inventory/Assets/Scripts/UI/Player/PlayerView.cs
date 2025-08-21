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
        GameObject itemInstance = Instantiate(itemPrefab, contentHolder.transform);
        SpawnItemInPlayerInventory();
    }

    private void SpawnItemInPlayerInventory()
    {
        // spawn Item in player inventory
    }
}
