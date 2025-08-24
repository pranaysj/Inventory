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

    private TextMeshProUGUI grossWeight;
    private TextMeshProUGUI buyAndSellText;
    private TextMeshProUGUI totalPrice;
    private TextMeshProUGUI quantity;
    private TextMeshProUGUI buttonText;
    public TransactionController TransactionController => transactionController;
    public PlayerService PlayerService => playerService;
    public ShopService ShopService => shopService;

    public TextMeshProUGUI GrossWeight => grossWeight;
    public TextMeshProUGUI BuyAndSellText => buyAndSellText;
    public TextMeshProUGUI TotalPrice => totalPrice;
    public TextMeshProUGUI Quantity => quantity;
    public TextMeshProUGUI ButtonText => buttonText;

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
        this.grossWeight = grossWeight;
        this.buyAndSellText = buyAndSellText;
        this.totalPrice = totalPrice;
        this.quantity = quantity;
        this.buttonText = buttonText;
    }
}
