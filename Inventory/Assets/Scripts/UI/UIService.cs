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
    [SerializeField] private TextMeshProUGUI transaction;
    [SerializeField] private TextMeshProUGUI price;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Player/Random Resources/Item")]
    [SerializeField] private Image r_icon;
    [SerializeField] private TextMeshProUGUI r_type;
    [SerializeField] private TextMeshProUGUI r_rarity;
    [SerializeField] private TextMeshProUGUI r_itemName;
    [SerializeField] private TextMeshProUGUI r_description;

    [Header("PROJECT")]
    [SerializeField] private Sprite selectedShopItemBGIcon;
    [SerializeField] private Sprite unselectedShopItemBGIcon;
    [SerializeField] private Sprite selectedPlayerItemBGIcon;
    [SerializeField] private Sprite unselectedPlayerItemBGIcon;

    [Header("HIERARCHY : UIService")]
    [SerializeField] private TextMeshProUGUI money;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Player/Inventory/Weight Text")]
    [SerializeField] private TextMeshProUGUI bagWeight;

    [Header("HIERARCHY : Canvas/Menu/Inventory/Item Panel/Shop/Item Info")]
    [SerializeField] private TextMeshProUGUI grossWeight;
    [SerializeField] private TextMeshProUGUI buyAndSellText;
    [SerializeField] private TextMeshProUGUI totalPrice;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private TextMeshProUGUI buttonText;

    public TextMeshProUGUI[] TabNames => tabNames;
    public GameObject[] TabItems => tabItems;
    public GameObject InventoryPanel => inventoryPanel;
    public GameObject Content => content;

    public Image Icon => icon;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI Description => description;
    public TextMeshProUGUI Weight => weight;
    public TextMeshProUGUI Transaction => transaction;
    public TextMeshProUGUI Price => price;

    public Image IconR => r_icon;
    public TextMeshProUGUI TypeR => r_type;
    public TextMeshProUGUI RarityR => r_rarity;
    public TextMeshProUGUI ItemNameR => r_itemName;
    public TextMeshProUGUI DescriptionR => r_description;
    public TextMeshProUGUI Money => money;
    public TextMeshProUGUI BagWeight => bagWeight;

    public TextMeshProUGUI GrossWeight => grossWeight;
    public TextMeshProUGUI BuyAndSellText => buyAndSellText;
    public TextMeshProUGUI TotalPrice => totalPrice;
    public TextMeshProUGUI Quantity => quantity;
    public TextMeshProUGUI ButtonText => buttonText;

    public Sprite SelectedShopItemBGIcon => selectedShopItemBGIcon;
    public Sprite UnselectedShopItemBGIcon => unselectedShopItemBGIcon;
    public Sprite SelectedPlayerItemBGIcon => selectedPlayerItemBGIcon;
    public Sprite UnselectedPlayerItemBGIcon => unselectedPlayerItemBGIcon;

    public GameObject PlayerItemPrefab { get; internal set; }
}
