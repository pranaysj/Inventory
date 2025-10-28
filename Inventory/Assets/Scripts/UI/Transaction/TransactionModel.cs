using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionModel 
{
    private TransactionController controller;
    
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
        this.controller = transactionController;
    }
}
