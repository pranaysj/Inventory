
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private EventService eventService;

    private PlayerController playerController;
    private UIService uiService;

    public PlayerController PlayerController => playerController;
    
    private Image itemIcon;
    private TextMeshProUGUI typeTxt;
    private TextMeshProUGUI rarityTxt;
    private TextMeshProUGUI itemNameTxt;
    private TextMeshProUGUI descriptionTxt;

    private TextMeshProUGUI bagWeightTxt;
    private TextMeshProUGUI moneyTextTxt;

    private Sprite selectedPlayerItemBGSprite;
    private Sprite unselectedPlayerItemBGSprite;

    private ShopItemSO tempItem;


    private void Awake()
    {
        eventService = ServiceLocator.Get<EventService>();
    }
    private void Start()
    {

        eventService.OnClickedPlayerItem += SelectPlayerItem;
        eventService.OnClickedAnotherItem += UnSelectAnotherItem;

        UpdateBagWeight(0);
    }

    public void Initialize(PlayerController playerController, UIService uiService)
    {
        this.playerController = playerController;
        this.uiService = uiService;

        InitializeUIElement();
    }

    private void InitializeUIElement()
    {
        itemIcon = uiService.Icon_R;
        typeTxt = uiService.Type_R;
        rarityTxt = uiService.Rarity_R;
        itemNameTxt = uiService.ItemName_R;
        descriptionTxt = uiService.Description_R;
        bagWeightTxt = uiService.BagWeight;
        moneyTextTxt = uiService.Money;
        selectedPlayerItemBGSprite = uiService.SelectedPlayerItemBGIcon;
        unselectedPlayerItemBGSprite = uiService.UnselectedPlayerItemBGIcon;

    }

    private void OnDestroy()
    {
        eventService.OnClickedPlayerItem -= SelectPlayerItem;
        eventService.OnClickedAnotherItem -= UnSelectAnotherItem;
    }
    public GameObject InstantiateItem()
    {
        return Instantiate(PlayerController.GetItemPrefab(), PlayerController.GetContentHolder().transform);
    }
    public void UpdateBagWeight(int bagWeight)
    {
        bagWeightTxt.text = "Weight: " + bagWeight.ToString() + " / " + PlayerController.GetMaxWeight() + " kg";
    }
    public void UpdateMoney()
    {
        moneyTextTxt.text = PlayerController.GetMoney().ToString();
    }
   
   
    public void SelectPlayerItem(TabView view)
    {
        view.itemBGIconGameobject.sprite = selectedPlayerItemBGSprite;
    }
    public void UnSelectAnotherItem(TabView view)
    {
        foreach (var item in PlayerController.GetItemInstanceByName().Values)
        {

            TabView tabView = item.GetComponent<TabView>();

            if (tabView != null && tabView != view)
            {
                tabView.itemBGIconGameobject.sprite = unselectedPlayerItemBGSprite;
            }
        }
    }

    public ShopItemSO GetTemporaryItemInPanel()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        tempItem = PlayerController.GetItemData();

        itemIcon.sprite = tempItem.icon;
        typeTxt.text = tempItem.itemType.ToString();
        rarityTxt.text = tempItem.rarity.ToString();
        itemNameTxt.text = tempItem.itemName;
        descriptionTxt.text = tempItem.description;

        return tempItem;
    }

    public void Reset()
    {
        itemIcon.sprite = unselectedPlayerItemBGSprite;
        typeTxt.text = "Item Type";
        rarityTxt.text = "Rarity";
        itemNameTxt.text = "Name";
        descriptionTxt.text = "Description";
    }
}
