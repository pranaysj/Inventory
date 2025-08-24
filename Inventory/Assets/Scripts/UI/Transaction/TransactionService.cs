using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionService 
{
    private TransactionController transactionController;
    private TransactionView transactionView;
    private PlayerService playerService;
    private ShopService shopService;

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

        playerService = ServiceLocator.Get<PlayerService>();
        shopService = ServiceLocator.Get<ShopService>();
        transactionView = ServiceLocator.Get<GameService>().TransactionView;

        transactionController.Initialize(
            playerService,
            shopService,
            transactionView,
            grossWeight,
            buyAndSellText,
            totalPrice,
            quantity,
            buttonText);

    }
}
