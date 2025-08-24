using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionModel 
{
    private TransactionController transactionController;
    private PlayerService playerService;
    private ShopService shopService;

    private TextMeshProUGUI grossWeightText;
    private TextMeshProUGUI buyAndSellText;
    private TextMeshProUGUI totalPriceText;
    private TextMeshProUGUI quantityText;
    private TextMeshProUGUI buttonText;
    public TransactionController TransactionController => transactionController;
    public PlayerService PlayerService => playerService;
    public ShopService ShopService => shopService;

    public TextMeshProUGUI GrossWeightText => grossWeightText;
    public TextMeshProUGUI BuyAndSellText => buyAndSellText;
    public TextMeshProUGUI TotalPriceText => totalPriceText;
    public TextMeshProUGUI QuantityText => quantityText;
    public TextMeshProUGUI ButtonText => buttonText;

    private int grossWeight = 0;
    private int buyingPrice = 0;
    private int sellingPrice = 0;
    private int quantity = 0;

    public int GrossWeightValue
    {
        get => grossWeight;
        set
        {
            grossWeight = value;
        }
    }
    public int BuyingPrice
    {
        get => buyingPrice;
        set
        {
            buyingPrice = value;
        }
    }
    public int SellingPrice
    {
        get => sellingPrice;
        set
        {
            sellingPrice = value;
        }
    }
    public int QuantityValue
    {
        get => quantity;
        set
        {
            quantity = value;
        }
    }

    public TransactionModel(TransactionController transactionController)
    {
        this.transactionController = transactionController;
    }

    internal void Initialize(
        PlayerService playerService,
        ShopService shopService,
        TextMeshProUGUI grossWeight, 
        TextMeshProUGUI buyAndSellText, 
        TextMeshProUGUI totalPrice, 
        TextMeshProUGUI quantity, 
        TextMeshProUGUI buttonText)
    {
        this.playerService = playerService;
        this.shopService = shopService;
        this.grossWeightText = grossWeight;
        this.buyAndSellText = buyAndSellText;
        this.totalPriceText = totalPrice;
        this.quantityText = quantity;
        this.buttonText = buttonText;
    }
}
