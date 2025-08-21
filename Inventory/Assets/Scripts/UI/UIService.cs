using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [Header("HIERARCHY : Canvas/Menu/Inventory")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Tabs")]
    [SerializeField] private TextMeshProUGUI[] tabNames;
    [SerializeField] private GameObject[] tabItems;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Player/Inventory/ItemContainer/Viewport")]
    [SerializeField] private GameObject content;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Item Info")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI buyingPrice;

    public TextMeshProUGUI[] TanNames => tabNames;
    public GameObject[] TabItems => tabItems;
    public GameObject InventoryPanel => inventoryPanel;
    public GameObject Content => content;

    public Image Icon => icon;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI Description => description;
    public TextMeshProUGUI Weight => weight;
    public TextMeshProUGUI BuyingPrice => buyingPrice;
}
