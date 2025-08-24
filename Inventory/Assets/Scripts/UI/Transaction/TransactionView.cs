using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TransactionType
{
    Increment,
    Decrement
}       

public class TransactionView : MonoBehaviour
{
    private TransactionController transactionController;
    private int grossWeight = 0;
    private int buyingPrice = 0;
    private int sellingPrice = 0;  
    private int quantity = 0;

    private bool isItemIsSelected = false;

    private TabView selectedItemView;


    public TransactionController TransactionController => transactionController;

    void Start()
    {
        ServiceLocator.Get<EventService>().OnClikedIShopItem += BuyInfo;
        ServiceLocator.Get<EventService>().OnClikedIPlayerItem += SellInfo;
    }
    void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnClikedIShopItem -= BuyInfo;
        ServiceLocator.Get<EventService>().OnClikedIShopItem -= SellInfo;
    }

    public void Initialize(TransactionController controller)
    {
        this.transactionController = controller;
    }

    private void BuyInfo(TabView view)
    {
        ResetTransactionInfo();

        selectedItemView = view;
        isItemIsSelected = true;

        TransactionController.GetGrossWeight.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Buying Price : ";
        TransactionController.GetTotalPrice.text = "0 G";
        TransactionController.GetButtonText.text = "BUY";
    }

    private void SellInfo(TabView view)
    {
        ResetTransactionInfo();

        selectedItemView = view;
        isItemIsSelected = true;

        TransactionController.GetGrossWeight.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Selling Price : ";
        TransactionController.GetTotalPrice.text = "0 G";
        TransactionController.GetButtonText.text = "SELL";
    }

    public void IncreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        quantity++;

        if(selectedItemView.TabType == TabType.Shop)
        {
            if (!IsBuyingLimitExceed())
            {
                quantity--;
                return;
            }
        }

        if(selectedItemView.TabType == TabType.Player)
        {
            if (!IsSellingLimitExceed())
            {
                quantity--;
                return;
            }
        }

        grossWeight = grossWeight + selectedItemView.Weight;

        CheckItemTypeForBuyAndSellButton(TransactionType.Increment);

        TransactionController.GetQuantity.text = quantity.ToString();
        TransactionController.GetGrossWeight.text = grossWeight.ToString() + " kg";
    }

    public void DecreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        quantity--;
        if (quantity < 0) quantity = 0;

        grossWeight = grossWeight - selectedItemView.Weight;
        if (grossWeight < 0) grossWeight = 0;

        CheckItemTypeForBuyAndSellButton(TransactionType.Decrement);

        TransactionController.GetQuantity.text = quantity.ToString();
        TransactionController.GetGrossWeight.text = grossWeight.ToString() + " kg";
    }

    void CheckItemTypeForBuyAndSellButton(TransactionType type)
    {
        if (!isItemIsSelected) return;

        switch (selectedItemView.TabType)
        {
            case TabType.Player:
                if(type == TransactionType.Increment)
                {
                    sellingPrice = sellingPrice + selectedItemView.SellingPrice;
                }
                if(type == TransactionType.Decrement)
                {
                    sellingPrice = sellingPrice - selectedItemView.SellingPrice;

                    if (sellingPrice < 0)
                        sellingPrice = 0;
                }
                TransactionController.GetTotalPrice.text = sellingPrice.ToString() + " G";
                break;

            case TabType.Shop:
                if(type == TransactionType.Increment)
                {
                    buyingPrice = buyingPrice + selectedItemView.BuyingPrice;
                }
                if(type == TransactionType.Decrement)
                {
                    buyingPrice = buyingPrice - selectedItemView.BuyingPrice;

                    if (buyingPrice < 0)
                        buyingPrice = 0;
                }
                TransactionController.GetTotalPrice.text = buyingPrice.ToString() + " G";
                break;
        }
    }

    private void ResetTransactionInfo()
    {
        grossWeight = 0;
        buyingPrice = 0;
        sellingPrice = 0;
        quantity = 0;

        isItemIsSelected = false;
        selectedItemView = null;

        TransactionController.GetGrossWeight.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Buying Price : ";
        TransactionController.GetTotalPrice.text = "0 G";
        TransactionController.GetQuantity.text = "0";
        TransactionController.GetButtonText.text = "BUY/SELL";
    }

    private bool IsBuyingLimitExceed()
    {
        int tempBuyingLimit = buyingPrice;
        tempBuyingLimit = tempBuyingLimit + selectedItemView.BuyingPrice;

        int tempWeightLimit = grossWeight;
        tempWeightLimit = tempWeightLimit + selectedItemView.Weight;

        int monkey = transactionController.GetPlayerService.PlayerController.GetMoney();
        int maxWeight = transactionController.GetPlayerService.PlayerController.GetMaxWeight();

        if (tempBuyingLimit < monkey && tempWeightLimit < maxWeight)
        {
            return true;
        }
        return false;
    }

    private bool IsSellingLimitExceed()
    {
        int itemQuantity = selectedItemView.Quantity;

        if (quantity <= itemQuantity)
        {
            return true;
        }
        return false;
    }

    public void BuyAndSellButton()
    {
        if (!isItemIsSelected) return;

        switch (selectedItemView.TabType)
        {
            case TabType.Player:
                transactionController.GetPlayerService.SellItem(selectedItemView, quantity, sellingPrice, grossWeight);
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemSold);
                break;
            case TabType.Shop:
                transactionController.GetPlayerService.BuyItem(selectedItemView, quantity, buyingPrice, grossWeight);
                selectedItemView.itemBGIconGameobject.sprite = transactionController.GetShopService.ShopController.GetUnselectedShopItemBGIcon;
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemPurchased);
                break;
        }
        ResetTransactionInfo();
    }
}

