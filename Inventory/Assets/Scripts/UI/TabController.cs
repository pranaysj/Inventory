using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TabController : MonoBehaviour
{
    [SerializeField] private ShopDatabaseSO shopDatabase;
    [SerializeField] private GameObject[] tabPanels;
    [SerializeField] private TextMeshProUGUI[] tabsButton;
    [SerializeField] private GameObject shopItemPrefab;

    private Color activeColor = new Color32(220,219,218,225);
    private Color inactiveColor = new Color32(115, 115, 115,225);


    private void Start()
    {
        tabPanels = ServiceLocator.Get<GameService>().TabPanel;
        tabsButton = ServiceLocator.Get<GameService>().TabButton;
        shopDatabase = ServiceLocator.Get<GameService>().ShopDatabase;
        shopItemPrefab = ServiceLocator.Get<GameService>().ShopItemPrefab;
        FillTabs();
        Switch(0);
    }

    private void FillTabs()
    {
        for(int i = 0; i < tabPanels.Length; i++)
        {
            GameObject contentHolder = GameObjectExtensions.FindChildOfChildByName(tabPanels[i], "Content");
            foreach (var item in shopDatabase.shopItems)
            {
                if (item.itemType == (ItemType)i)
                {
                    GameObject shopItem = Instantiate(shopItemPrefab, contentHolder.transform);

                    var name = GameObjectExtensions.FindChildOfChildByName(shopItem, "Name").GetComponent<TextMeshProUGUI>();
                    var icon = GameObjectExtensions.FindChildOfChildByName(shopItem, "Icon").GetComponent<UnityEngine.UI.Image>();
                    var rarity = GameObjectExtensions.FindChildOfChildByName(shopItem, "Rarity").GetComponent<TextMeshProUGUI>();
                    var buyingPrice = GameObjectExtensions.FindChildOfChildByName(shopItem, "Price").GetComponent<TextMeshProUGUI>();

                    if (name) name.text = item.itemName;
                    if (icon != null) icon.sprite = item.icon;
                    if (rarity) rarity.text = item.rarity.ToString();
                    if (buyingPrice) buyingPrice.text = item.buyingPrice.ToString() + " G";
                }
            }
        }
    }

    public void Switch(int tabID)
    {
        if(tabPanels == null || tabsButton == null) return;
        if (tabID < 0 || tabID >= tabPanels.Length || tabID >= tabsButton.Length) return;

        foreach (var panel in tabPanels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        foreach (var button in tabsButton)
        {
            if (button != null)
                button.color = inactiveColor;
        }

        if (tabPanels[tabID] != null)
            tabPanels[tabID].SetActive(true);
        if (tabsButton[tabID] != null) 
            tabsButton[tabID].color = activeColor;
    }
}
