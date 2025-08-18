using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private EventService eventService;

    [SerializeField] private GameObject inventoryPanel;
    public GameObject InventoryPanel => inventoryPanel;

    [SerializeField] private GameObject[] tabPanel;
    public GameObject[] TabPanel => tabPanel;

    [SerializeField] private TextMeshProUGUI[] tabButton;
    public TextMeshProUGUI[] TabButton => tabButton;

    [SerializeField] private ShopDatabaseSO shopDatabase;
    public ShopDatabaseSO ShopDatabase => shopDatabase;

    [SerializeField] private GameObject shopItemPrefab;
    public GameObject ShopItemPrefab => shopItemPrefab;

    private void Awake()
    {
        eventService = new EventService();
        ServiceLocator.Register(eventService);
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        eventService.Update();
    }
}
