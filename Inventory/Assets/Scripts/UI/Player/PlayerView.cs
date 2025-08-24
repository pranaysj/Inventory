using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    private ShopItemSO tempItem;

    [SerializeField]private int bagWeight = 0;
    private TextMeshProUGUI bagWeightText;
    

    private void Start()
    {
        ServiceLocator.Get<EventService>().OnClikedIPlayerItem += SelectPlayerItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem += UnSelectAnotherItem;

        bagWeightText = PlayerController.GetBagWight();
        bagWeightText.text = "Weight: " + bagWeight.ToString() + " / " + PlayerController.GetMaxWeight() + " kg";
    }

    public void Initialize(PlayerController playerController)
    {
        this.playerController = playerController;

    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EventService>().OnClikedIPlayerItem -= SelectPlayerItem;
        ServiceLocator.Get<EventService>().OnClickedAnotehrItem -= UnSelectAnotherItem;
    }

    public GameObject InstantiateItem()
    {
        return Instantiate(PlayerController.GetItemPrefab(), PlayerController.GetContentHolder().transform);
    }
    public void UpdateBagWeight(int weightChange)
    {
        bagWeight += weightChange;
        PlayerController.SetWeight(bagWeight);
        bagWeightText.text = "Weight: " + bagWeight.ToString() + " / " + PlayerController.GetMaxWeight() + " kg";
    }
    public void UpdateMoney()
    {
        PlayerController.GetMonkeyText().text = PlayerController.GetMoney().ToString();
    }

    public void GetTemporaryItemInPanel()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        tempItem = PlayerController.GetItemData();

        PlayerController.GetIcon().sprite = tempItem.icon;
        PlayerController.GetItemType().text = tempItem.itemType.ToString();
        PlayerController.GetRarity().text = tempItem.rarity.ToString();
        PlayerController.GetItemName().text = tempItem.itemName;
        PlayerController.GetDescription().text = tempItem.description;

    }

    public void GetTempItemInPlayerInventory()
    {
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.ButtonClick);

        if (tempItem == null) return;

        int nextWeight = PlayerController.GetWeight() + tempItem.weight;

        if (tempItem != null && nextWeight < PlayerController.GetMaxWeight())
        {
            PlayerController.SpawnItemInPlayerInventory(tempItem);
            tempItem = null;
            PlayerController.Reset();
        }
    }

    public void SelectPlayerItem(TabView view)
    {
        view.itemBGIconGameobject.sprite = PlayerController.GetSelectedPlayerItemBGIcon;
    }

    public void UnSelectAnotherItem(TabView view)
    {
        foreach (var item in PlayerController.GetItemInstanceByName().Values)
        {
            Sprite unselectedSprite = PlayerController.GetUnselectedPlayerItemBGIcon;

            TabView tabView = item.GetComponent<TabView>();

            if (tabView != null && tabView != view)
            {
                tabView.itemBGIconGameobject.sprite = unselectedSprite;
            }
        }
    }
}
