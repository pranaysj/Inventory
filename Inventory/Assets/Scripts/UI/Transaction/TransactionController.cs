using System.Diagnostics;

public class TransactionController 
{
    private TransactionModel models;
    public TransactionModel TransactionModel => models;
    
    private TransactionView view;
    public TransactionView TransactionView => view;

    private PlayerService playerService;
    private PlayerController playerController;

    public TransactionController(TransactionView transactionView, UIService uiService, PlayerService playerService)
    {
        view = transactionView;
        this.playerService = playerService;

        models = new TransactionModel(this);

        view.Initialize(this, uiService);
        playerController = playerService.PlayerController;
    }

    public bool IsBuyingLimitExceed()
    {
        int tempBuyingLimit = TransactionModel.BuyingPrice;
        tempBuyingLimit = tempBuyingLimit + TransactionView.SelectedItemView.BuyingPrice;

        int tempWeightLimit = TransactionModel.GrossWeightValue;
        tempWeightLimit = tempWeightLimit + TransactionView.SelectedItemView.Weight;

        int monkey = playerController.GetMoney();
        int maxWeight = playerController.GetMaxWeight();

        if (tempBuyingLimit < monkey && tempWeightLimit < maxWeight)
        {
            return true;
        }
        return false;
    }

    public bool IsSellingLimitExceed()
    {
        int itemQuantity = TransactionView.SelectedItemView.Quantity;

        if (TransactionModel.QuantityValue <= itemQuantity)
        {
            return true;
        }
        return false;
    }

    public void CheckItemTypeForBuyAndSellButton(TransactionType type)
    {
        if (!TransactionView.isItemSelected) return;

        switch (TransactionView.SelectedItemView.TabType)
        {
            case TabType.Player:
                if (type == TransactionType.Increment)
                {
                    TransactionModel.SellingPrice = TransactionModel.SellingPrice + TransactionView.SelectedItemView.SellingPrice;
                }
                if (type == TransactionType.Decrement)
                {
                    TransactionModel.SellingPrice = TransactionModel.SellingPrice - TransactionView.SelectedItemView.SellingPrice;

                    if (TransactionModel.SellingPrice < 0)
                        TransactionModel.SellingPrice = 0;
                }
                view.SetTotalPrice(TabType.Player, TransactionModel.SellingPrice);
                
                break;

            case TabType.Shop:
                if (type == TransactionType.Increment)
                {
                    TransactionModel.BuyingPrice = TransactionModel.BuyingPrice + TransactionView.SelectedItemView.BuyingPrice;
                }
                if (type == TransactionType.Decrement)
                {
                    TransactionModel.BuyingPrice = TransactionModel.BuyingPrice - TransactionView.SelectedItemView.BuyingPrice;

                    if (TransactionModel.BuyingPrice < 0)
                        TransactionModel.BuyingPrice = 0;
                }
                view.SetTotalPrice(TabType.Shop, TransactionModel.BuyingPrice);
                break;
        }
    }

    //Assign on button click events
    public void IncreaseQuantity()
    {
        if (!view.isItemSelected) return;

        var quantity = TransactionModel.QuantityValue;

        quantity++;

        if (view.SelectedTabTypeInShop())
        {
            if (!IsBuyingLimitExceed())
            {   
                quantity--;
                return;
            }
        }

        if (view.SelectedTabTypeInPlayer())
        {
            if (!IsSellingLimitExceed())
            {
                quantity--;
                return;
            }
        }

        TransactionModel.QuantityValue = quantity;

        TransactionModel.GrossWeightValue = TransactionModel.GrossWeightValue + view.TabView().Weight;
        CheckItemTypeForBuyAndSellButton(TransactionType.Increment);
        view.UpdateText();
    }

    public void DecreaseQuantity()
    {
        if (!view.isItemSelected) return;

        var quantity = TransactionModel.QuantityValue;

        quantity--;

        if (quantity < 0) 
            quantity = 0;
        
        TransactionModel.QuantityValue = quantity;

        var weight = TransactionModel.GrossWeightValue;

        weight = weight - view.TabView().Weight;
        
        if (weight < 0)
            weight = 0;

        TransactionModel.GrossWeightValue = weight;

        CheckItemTypeForBuyAndSellButton(TransactionType.Decrement);
        view.UpdateText();
    }

    public void BuyAndSellButton()
    {
        var quantity = TransactionModel.QuantityValue;

        if (!view.isItemSelected || quantity == 0) 
            return;

        var type = view.TabView();
        
        var playerModel = playerController.PlayerModel;
        int totalValue = playerController.GetTotalValue();
        int defaultValue = playerModel.BagDefaultValue;

        switch (type.TabType)
        {
            case TabType.Player:
                playerService.SellItem(type, quantity, TransactionModel.SellingPrice, TransactionModel.GrossWeightValue);
                totalValue -= defaultValue;
                playerModel.TotalValue = totalValue;
                view.SetBackgroundIcon(TabType.Player);
                break;

            case TabType.Shop:
                playerService.BuyItem(type, quantity, type.BuyingPrice, TransactionModel.GrossWeightValue);
                view.SetBackgroundIcon(TabType.Shop);
                break;
        }
        view.ResetText();
    }


    public void ResetData()
    {
        TransactionModel.GrossWeightValue = 0;
        TransactionModel.BuyingPrice = 0;
        TransactionModel.SellingPrice = 0;
        TransactionModel.QuantityValue = 0;
    }
}
