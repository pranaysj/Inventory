using System.Diagnostics;

public class TransactionService 
{
    private TransactionController transactionController;

    public TransactionService(TransactionView transactionView, UIService uiService, PlayerService playerService)
    {
        transactionController = new TransactionController(transactionView, uiService, playerService);        
    }
    public void PlusButton()
    {
        transactionController.IncreaseQuantity();
    }

    public void MinusButton()
    {
        transactionController.DecreaseQuantity();
    }

    public void BuyAndSell()
    {
        transactionController.BuyAndSellButton();
    }
}
