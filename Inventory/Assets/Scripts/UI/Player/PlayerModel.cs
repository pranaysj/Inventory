using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    private GameObject contentHolder;
    private GameObject itemPrefab;
    private ShopView shopView;
    private ShopDatabaseSO shopDatabaseSO;
    private Image icon;
    private TextMeshProUGUI type;
    private TextMeshProUGUI rarity;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI description;

    private TextMeshProUGUI bagWeight;

    private Sprite selectedPlayerItemBGIcon;
    private Sprite unselectedPlayerItemBGIcon;

    private int money;
    private int weight;
    private int maxWeight = 30;

    public GameObject ContentHolder => contentHolder;
    public GameObject ItemPrefab => itemPrefab;
    public ShopView ShopView => shopView;
    public ShopDatabaseSO ShopDatabaseSO => shopDatabaseSO;
    public Image Icon => icon;
    public TextMeshProUGUI Type => type;
    public TextMeshProUGUI Rarity => rarity;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI Description => description;
    public TextMeshProUGUI BagWeight => bagWeight;

    public Sprite SelectedPlayerItemBGIcon => selectedPlayerItemBGIcon;
    public Sprite UnselectedPlayerItemBGIcon => unselectedPlayerItemBGIcon;

    public int Money { get => money; set => money = value; }
    public int Weight { get => weight; set => weight = value; }
    public int MaxWeight => maxWeight;

    public PlayerModel(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Initialize(
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
        Sprite selectedPlayerItemBGIcon,
        Sprite unselectedPlayerItemBGIcon)
    {
        this.contentHolder = contentHolder;
        this.itemPrefab = itemPrefab;
        this.shopView = shopView;
        this.shopDatabaseSO = shopDatabaseSO;
        this.icon = icon;
        this.type = type;
        this.rarity = rarity;
        this.itemName = itemName;
        this.description = description;
        this.bagWeight = bagWeight;
        this.selectedPlayerItemBGIcon = selectedPlayerItemBGIcon;
        this.unselectedPlayerItemBGIcon = unselectedPlayerItemBGIcon;
    }
}
