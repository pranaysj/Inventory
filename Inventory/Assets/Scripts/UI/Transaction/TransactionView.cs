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
    private UIService uiService;
    private EventService eventService;

    public TransactionController TransactionController => transactionController;

    private bool isItemIsSelected = false;

    private TabView selectedItemView;

    public bool IsItemIsSelected => isItemIsSelected;
    public TabView SelectedItemView => selectedItemView;

    

    void Start()
    {
        eventService = ServiceLocator.Get<EventService>();

        eventService.OnClickedIShopItem += BuyInfo;
        eventService.OnClickedIPlayerItem += SellInfo;
    }
    void OnDestroy()
    {
        eventService.OnClickedIShopItem -= BuyInfo;
        eventService.OnClickedIShopItem -= SellInfo;
    }

    public void Initialize(TransactionController controller, UIService uiService)
    {
        this.transactionController = controller;
        this.uiService = uiService;
    }

    private void BuyInfo(TabView view)
    {
        ResetTransactionInfo();

        selectedItemView = view;
        isItemIsSelected = true;

        TransactionController.GetGrossWeighText.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Buying Price : ";
        TransactionController.GetTotalPriceText.text = "0 G";
        TransactionController.GetButtonText.text = "BUY";
    }

    private void SellInfo(TabView view)
    {
        ResetTransactionInfo();

        selectedItemView = view;
        isItemIsSelected = true;

        TransactionController.GetGrossWeighText.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Selling Price : ";
        TransactionController.GetTotalPriceText.text = "0 G";
        TransactionController.GetButtonText.text = "SELL";
    }

    public void IncreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        TransactionController.QuantityValue++;
        Debug.Log("Quantity Value: " + TransactionController.QuantityValue);
        if (selectedItemView.TabType == TabType.Shop)
        {
            if (!TransactionController.IsBuyingLimitExceed())
            {
                TransactionController.QuantityValue--;
                return;
            }
        }

        if (selectedItemView.TabType == TabType.Player)
        {
            if (!TransactionController.IsSellingLimitExceed())
            {
                TransactionController.QuantityValue--;
                return;
            }
        }

        TransactionController.GrossWeightValue = TransactionController.GrossWeightValue + selectedItemView.Weight;

        TransactionController.CheckItemTypeForBuyAndSellButton(TransactionType.Increment);

        TransactionController.GetQuantityText.text = TransactionController.QuantityValue.ToString();
        TransactionController.GetGrossWeighText.text = TransactionController.GrossWeightValue.ToString() + " kg";
    }

    public void DecreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        TransactionController.QuantityValue--;
        if (TransactionController.QuantityValue < 0) TransactionController.QuantityValue = 0;

        TransactionController.GrossWeightValue = TransactionController.GrossWeightValue - selectedItemView.Weight;
        if (TransactionController.GrossWeightValue < 0) TransactionController.GrossWeightValue = 0;

        TransactionController.CheckItemTypeForBuyAndSellButton(TransactionType.Decrement);

        TransactionController.GetQuantityText.text = TransactionController.QuantityValue.ToString();
        TransactionController.GetGrossWeighText.text = TransactionController.GrossWeightValue.ToString() + " kg";
    }

    public void BuyAndSellButton()
    {
        if (!isItemIsSelected || TransactionController.QuantityValue == 0) return;

        switch (selectedItemView.TabType)
        {
            case TabType.Player:
                transactionController.GetPlayerService.SellItem(selectedItemView, TransactionController.QuantityValue, TransactionController.SellingPrice, TransactionController.GrossWeightValue);
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemSold);
                break;
            case TabType.Shop:
                transactionController.GetPlayerService.BuyItem(selectedItemView, TransactionController.QuantityValue, selectedItemView.BuyingPrice, TransactionController.GrossWeightValue);
                selectedItemView.itemBGIconGameobject.sprite = uiService.UnselectedShopItemBGIcon;
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemPurchased);
                break;
        }
        ResetTransactionInfo();
    }

    public void ResetTransactionInfo()
    {
        TransactionController.ResetTransactionInfo();

        isItemIsSelected = false;
        selectedItemView = null;

        TransactionController.GetGrossWeighText.text = "0 kg";
        TransactionController.GetBuyAndSellText.text = "Buying Price : ";
        TransactionController.GetTotalPriceText.text = "0 G";
        TransactionController.GetQuantityText.text = "0";
        TransactionController.GetButtonText.text = "BUY/SELL";

    }
}

