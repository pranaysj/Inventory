using TMPro;
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

        eventService.OnClickedShopItem += BuyInfo;
        eventService.OnClickedPlayerItem += SellInfo;
    }
    void OnDestroy()
    {
        eventService.OnClickedShopItem -= BuyInfo;
        eventService.OnClickedShopItem -= SellInfo;
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
        var model = Controller.TransactionModel;
        quantityText.text = model.QuantityValue.ToString();
        grossWeightText.text = model.GrossWeightValue.ToString() + " kg";
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);
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

    internal void SetBackgroundIcon(TabType type)
    {
        switch (type)
        {
            case TabType.Player:
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemSold);
                break;

            case TabType.Shop:
                selectedItemView.itemBGIconGameobject.sprite = uiService.UnselectedShopItemBGIcon;
                ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ItemPurchased);
                break;
        }
    }

    internal void SetTotalPrice(TabType type, int price)
    {
        switch (type)
        {
            case TabType.Player:
                totalPriceText.text = price.ToString() + " G";
                break;
            case TabType.Shop:
                totalPriceText.text = price.ToString() + " G";
                break;
        }
    }
}

