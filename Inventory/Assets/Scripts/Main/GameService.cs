using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Tabs")]
    [SerializeField] private GameObject[] tabPanel;
    [SerializeField] private TextMeshProUGUI[] tabButton;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;

    [Header("HIERARCHY : UIManager")]
    [SerializeField] private ShopView shopView;

    [Header("PROJECT")]
    [Header("ScriptableObject")]
    [SerializeField] private ShopDatabaseSO shopDatabase;
    [Header("Prefab")]
    [SerializeField] private GameObject shopItemPrefab;

    public GameObject[] TabPanel => tabPanel;
    public TextMeshProUGUI[] TabButton => tabButton;
    public ShopDatabaseSO ShopDatabase => shopDatabase;
    public GameObject ShopItemPrefab => shopItemPrefab;

    private EventService eventService;
    private ShopService shopService;

    private void Awake()
    {
        ServiceLocator.Register(this);
        ServiceLocator.Register(eventService);
        ServiceLocator.Register(shopService);
        eventService = new EventService();
        shopService = new ShopService(shopView, shopDatabase);
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
