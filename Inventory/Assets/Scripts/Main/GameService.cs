using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    [Header("HIERARCHY : UIService")]
    [SerializeField] private UIService uiService;
    [SerializeField] private ShopView shopView;
    [SerializeField] private PlayerView playerView;

    [Header("PROJECT")]
    [Header("ScriptableObject")]
    [SerializeField] private ShopDatabaseSO shopDatabase;
    [Header("Prefab")]
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject playerItemPrefab;

    public ShopDatabaseSO ShopDatabase => shopDatabase;
    public GameObject ShopItemPrefab => shopItemPrefab;
    public GameObject PlayerItemPrefab => playerItemPrefab;
    public PlayerView PlayerView => playerView;

    private EventService eventService;
    private ShopService shopService;
    public PlayerService playerService;

    private void Awake()
    {

        eventService = new EventService();
        ServiceLocator.Register(this);
        ServiceLocator.Register(eventService);

        ServiceLocator.Register(uiService);
        shopService = new ShopService(shopView, ShopDatabase);
        playerService = new PlayerService();

        ServiceLocator.Register(shopService);
        ServiceLocator.Register(playerService);
    }

    private void Update()
    {
        eventService.Update();
    }

    public void SwitchTab(int tanID)
    {
        //get the switch function from the controller
        if (shopService != null)
        {
            shopService.Switch(tanID);
        }
        else
        {
            Debug.LogError("ShopService is not initialized in GameService.");
        }

    }
}
