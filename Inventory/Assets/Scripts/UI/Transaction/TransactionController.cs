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

    public void Initialize(
        PlayerService playerService, 
        ShopService shopService,
        TransactionView transactionView, 
        TextMeshProUGUI grossWeight, 
        TextMeshProUGUI buyAndSellText, 
        TextMeshProUGUI totalPrice, 
        TextMeshProUGUI quantity, 
        TextMeshProUGUI buttonText)
    {
        this.transactionView = transactionView;
        TransactionView.Initialize(this);

        TransactionModel.Initialize(
            playerService,
            shopService,
            grossWeight,
            buyAndSellText,
            totalPrice,
            quantity,
            buttonText);
    }

    public TextMeshProUGUI GetGrossWeighText => TransactionModel.GrossWeightText;
    public TextMeshProUGUI GetBuyAndSellText => TransactionModel.BuyAndSellText;
    public TextMeshProUGUI GetTotalPriceText => TransactionModel.TotalPriceText;
    public TextMeshProUGUI GetQuantityText => TransactionModel.QuantityText;
    public TextMeshProUGUI GetButtonText => TransactionModel.ButtonText;

    public PlayerService GetPlayerService => TransactionModel.PlayerService;
    public ShopService GetShopService => TransactionModel.ShopService;

    public int GrossWeightValue
    {
        get => TransactionModel.GrossWeightValue;
        set => TransactionModel.GrossWeightValue = value;
    }
    public int BuyingPrice
    {
        get => TransactionModel.BuyingPrice;
        set => TransactionModel.BuyingPrice = value;
    }
    public int SellingPrice
    {
        get => TransactionModel.SellingPrice;
        set => TransactionModel.SellingPrice = value;
    }
    public int QuantityValue
    {
        get => TransactionModel.QuantityValue;
        set => TransactionModel.QuantityValue = value;
    }

    //Call from button
    //public void IncreaseQuantity()
    //{
    //    ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

    //    if (!TransactionView.IsItemIsSelected) return;

    //    TransactionModel.QuantityValue++;

    //    if (TransactionView.SelectedItemView.TabType == TabType.Shop)
    //    {
    //        if (!IsBuyingLimitExceed())
    //        {
    //            TransactionModel.QuantityValue--;
    //            return;
    //        }
    //    }

    //    if (TransactionView.SelectedItemView.TabType == TabType.Player)
    //    {
    //        if (!IsSellingLimitExceed())
    //        {
    //            TransactionModel.QuantityValue--;
    //            return;
    //        }
    //    }

    //    TransactionModel.GrossWeightValue = TransactionModel.GrossWeightValue + TransactionView.SelectedItemView.Weight;

    //    CheckItemTypeForBuyAndSellButton(TransactionType.Increment);

    //    GetQuantity.text = TransactionModel.QuantityValue.ToString();
    //    GetGrossWeight.text = TransactionModel.GrossWeightValue.ToString() + " kg";
    //}

    //public void DecreaseQuantity()
    //{
    //    ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

    //    if (!TransactionView.IsItemIsSelected) return;

    //    TransactionModel.QuantityValue--;
    //    if (TransactionModel.QuantityValue < 0) TransactionModel.QuantityValue = 0;

    //    TransactionModel.GrossWeightValue = TransactionModel.GrossWeightValue - TransactionView.SelectedItemView.Weight;
    //    if (TransactionModel.GrossWeightValue < 0) TransactionModel.GrossWeightValue = 0;

    //    CheckItemTypeForBuyAndSellButton(TransactionType.Decrement);

    //    GetQuantity.text = TransactionModel.QuantityValue.ToString();
    //    GetGrossWeight.text = TransactionModel.GrossWeightValue.ToString() + " kg";
    //}

    public bool IsBuyingLimitExceed()
    {
        int tempBuyingLimit = BuyingPrice;
        tempBuyingLimit = tempBuyingLimit + TransactionView.SelectedItemView.BuyingPrice;

        int tempWeightLimit = TransactionModel.GrossWeightValue;
        tempWeightLimit = tempWeightLimit + TransactionView.SelectedItemView.Weight;

        int monkey = GetPlayerService.PlayerController.GetMoney();
        int maxWeight = GetPlayerService.PlayerController.GetMaxWeight();

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
        if (!TransactionView.IsItemIsSelected) return;

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
                GetTotalPriceText.text = TransactionModel.SellingPrice.ToString() + " G";
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
                GetTotalPriceText.text = TransactionModel.BuyingPrice.ToString() + " G";
                break;
        }
    }

    //public void BuyAndSellButton()
    //{
    //    if (!TransactionView.IsItemIsSelected) return;

    //    switch (TransactionView.SelectedItemView.TabType)
    //    {
    //        case TabType.Player:
    //            GetPlayerService.SellItem(TransactionView.SelectedItemView, TransactionModel.QuantityValue, TransactionModel.SellingPrice, TransactionModel.GrossWeightValue);
    //            ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemSold);
    //            break;
    //        case TabType.Shop:
    //            GetPlayerService.BuyItem(TransactionView.SelectedItemView, TransactionModel.QuantityValue, TransactionModel.BuyingPrice, TransactionModel.GrossWeightValue);
    //            TransactionView.SelectedItemView.itemBGIconGameobject.sprite = GetShopService.ShopController.GetUnselectedShopItemBGIcon;
    //            ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemPurchased);
    //            break;
    //    }
    //    ResetTransactionInfo();
    //}

    public void ResetTransactionInfo()
    {
        TransactionModel.GrossWeightValue = 0;
        TransactionModel.BuyingPrice = 0;
        TransactionModel.SellingPrice = 0;
        TransactionModel.QuantityValue = 0;
        //TransactionView.ResetTransactionInfo();
    }
}
