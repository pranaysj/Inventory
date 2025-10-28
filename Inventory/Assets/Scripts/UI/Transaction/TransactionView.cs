using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum TransactionType
{
    Increment,
    Decrement
}       

public class TransactionView : MonoBehaviour
{
    private UIService uiService;
    private EventService eventService;

    private TransactionController controller;
    public TransactionController Controller => controller;

    private TabView selectedItemView;
    public TabView SelectedItemView => selectedItemView;

    private bool isItemIsSelected = false;
    public bool isItemSelected => isItemIsSelected;

    private TextMeshProUGUI grossWeightText;
    private TextMeshProUGUI buyAndSellText;
    private TextMeshProUGUI totalPriceText;
    private TextMeshProUGUI quantityText;
    private TextMeshProUGUI buttonText;

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
        this.controller = controller;
        this.uiService = uiService;

        InitializeText();
    }

    private void InitializeText()
    {
        grossWeightText = uiService.GrossWeight;
        buyAndSellText = uiService.BuyAndSellText;
        totalPriceText = uiService.TotalPrice;
        quantityText = uiService.Quantity;
        buttonText = uiService.ButtonText;
    }

    private void BuyInfo(TabView view)
    {
        ResetText();

        selectedItemView = view;
        isItemIsSelected = true;

        grossWeightText.text = "0 kg";
        buyAndSellText.text = "Buying Price : ";
        totalPriceText.text = "0 G";
        buttonText.text = "BUY";
    }

    private void SellInfo(TabView view)
    {
        ResetText();

        selectedItemView = view;
        isItemIsSelected = true;

        grossWeightText.text = "0 kg";
        buyAndSellText.text = "Selling Price : ";
        totalPriceText.text = "0 G";
        buttonText.text = "SELL";
    }

    public void IncreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        Controller.QuantityValue++;
        Debug.Log("Quantity Value: " + Controller.QuantityValue);
        if (selectedItemView.TabType == TabType.Shop)
        {
            if (!Controller.IsBuyingLimitExceed())
            {
                Controller.QuantityValue--;
                return;
            }
        }

        if (selectedItemView.TabType == TabType.Player)
        {
            if (!Controller.IsSellingLimitExceed())
            {
                Controller.QuantityValue--;
                return;
            }
        }

        Controller.GrossWeightValue = Controller.GrossWeightValue + selectedItemView.Weight;

        Controller.CheckItemTypeForBuyAndSellButton(TransactionType.Increment);

    }

    public bool SelectedTabTypeInShop()
    {
        if (selectedItemView.TabType == TabType.Shop)
            return true;
        return false;
    }

    public bool SelectedTabTypeInPlayer()
    {
        if (selectedItemView.TabType == TabType.Player)
            return true;
        return false;
    }

    public TabView TabView()
    {
        return selectedItemView;
    }

    internal void UpdateText()
    {
        quantityText.text = Controller.QuantityValue.ToString();
        grossWeightText.text = Controller.GrossWeightValue.ToString() + " kg";
    }

    public void DecreaseQuantity()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (!isItemIsSelected) return;

        Controller.QuantityValue--;
        if (Controller.QuantityValue < 0) Controller.QuantityValue = 0;

        Controller.GrossWeightValue = Controller.GrossWeightValue - selectedItemView.Weight;
        if (Controller.GrossWeightValue < 0) Controller.GrossWeightValue = 0;

        Controller.CheckItemTypeForBuyAndSellButton(TransactionType.Decrement);

        Controller.GetQuantityText.text = Controller.QuantityValue.ToString();
        Controller.GetGrossWeighText.text = Controller.GrossWeightValue.ToString() + " kg";
    }

    public void BuyAndSellButton()
    {
        if (!isItemIsSelected || Controller.QuantityValue == 0) return;

        switch (selectedItemView.TabType)
        {
            case TabType.Player:
                controller.GetPlayerService.SellItem(selectedItemView, Controller.QuantityValue, Controller.SellingPrice, Controller.GrossWeightValue);
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemSold);
                break;
            case TabType.Shop:
                controller.GetPlayerService.BuyItem(selectedItemView, Controller.QuantityValue, selectedItemView.BuyingPrice, Controller.GrossWeightValue);
                selectedItemView.itemBGIconGameobject.sprite = uiService.UnselectedShopItemBGIcon;
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemPurchased);
                break;
        }
        ResetText();
    }

    public void ResetText()
    {
        Controller.ResetData();

        isItemIsSelected = false;
        selectedItemView = null;

        grossWeightText.text = "0 kg";
        buyAndSellText.text = "Buying Price : ";
        totalPriceText.text = "0 G";
        quantityText.text = "0";
        buttonText.text = "BUY/SELL";

    }

}

