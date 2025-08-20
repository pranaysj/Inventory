using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    [Header("HIERARCHY : Canvas/Menu/Inventory")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Tabs")]
    [SerializeField] private GameObject[] tabPanel;
    [SerializeField] private TextMeshProUGUI[] tabButton;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Player/Inventory")]
    [SerializeField] private GameObject itemContainer;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI buyingPrice;

    [Header("HIERARCHY : UIManager")]
    [SerializeField] private ShopView shopView;
    [SerializeField] private PlayerView playerView;

    [Header("PROJECT")]
    [Header("ScriptableObject")]
    [SerializeField] private ShopDatabaseSO shopDatabase;
    [Header("Prefab")]
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject playerItemPrefab;

    public GameObject[] TabPanel => tabPanel;
    public TextMeshProUGUI[] TabButton => tabButton;
    public ShopDatabaseSO ShopDatabase => shopDatabase;
    public GameObject ShopItemPrefab => shopItemPrefab;
    public GameObject PlayerItemPrefab => playerItemPrefab;
    public GameObject InventoryPanel => inventoryPanel;
    public GameObject ItemContainer => itemContainer;

    public Image Icon => icon;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI Description => description;
    public TextMeshProUGUI Weight => weight;
    public TextMeshProUGUI BuyingPrice => buyingPrice;

    public PlayerView PlayerView => playerView;

    private EventService eventService;
    private ShopService shopService;
    public PlayerService playerService;

    private void Awake()
    {

        eventService = new EventService();
        ServiceLocator.Register(this);
        ServiceLocator.Register(eventService);

        shopService = new ShopService(shopView, shopDatabase);
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
