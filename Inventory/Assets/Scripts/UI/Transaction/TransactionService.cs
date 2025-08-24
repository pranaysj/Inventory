using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionService 
{
    private TransactionController transactionController;
    private TransactionView transactionView;

    private TextMeshProUGUI grossWeight;
    private TextMeshProUGUI buyAndSellText;
    private TextMeshProUGUI totalPrice;
    private TextMeshProUGUI quantity;
    private TextMeshProUGUI buttonText;
    public TransactionController TransactionController => transactionController;
    public TransactionService()
    {
        transactionController = new TransactionController();
        Initialize();
    }
    public void Initialize()
    {
        grossWeight = ServiceLocator.Get<UIService>().GrossWeight;
        buyAndSellText = ServiceLocator.Get<UIService>().BuyAndSellText;
        totalPrice = ServiceLocator.Get<UIService>().TotalPrice;
        quantity = ServiceLocator.Get<UIService>().Quantity;
        buttonText = ServiceLocator.Get<UIService>().ButtonText;

        transactionView = ServiceLocator.Get<GameService>().TransactionView;

        transactionController.Initialize(
            transactionView,
            grossWeight,
            buyAndSellText,
            totalPrice,
            quantity,
            buttonText);
    }
}
