using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionController 
{
    private TransactionModel transactionModel;
    private TransactionView transactionView;
    public TransactionModel TransactionModel => transactionModel;
    public TransactionView TransactionView => transactionView;

    public TransactionController()
    {
        transactionModel = new TransactionModel(this);
    }

    public void Initialize(TransactionView transactionView, TextMeshProUGUI grossWeight, TextMeshProUGUI buyAndSellText, TextMeshProUGUI totalPrice, TextMeshProUGUI quantity, TextMeshProUGUI buttonText)
    {
        this.transactionView = transactionView;
        TransactionView.Initialize(this);

        TransactionModel.Initialize(
            grossWeight,
            buyAndSellText,
            totalPrice,
            quantity,
            buttonText);
    }

    public TextMeshProUGUI GetGrossWeight => TransactionModel.GrossWeight;
    public TextMeshProUGUI GetBuyAndSellText => TransactionModel.BuyAndSellText;
    public TextMeshProUGUI GetTotalPrice => TransactionModel.TotalPrice;
    public TextMeshProUGUI GetQuantity => TransactionModel.Quantity;
    public TextMeshProUGUI GetButtonText => TransactionModel.ButtonText;
}
