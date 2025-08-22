using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameService;

public class EventService
{
    public event Action OnInventoryKeyPressed, OnPlayerGetItem;
    public event Action<TabView> OnClikedIPlayerItem, OnClikedIShopItem, OnClickedAnotehrItem;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryKeyPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            OnPlayerGetItem?.Invoke();
        }
    }

    public void ClickedItem(TabView view)
    {
        OnClickedAnotehrItem?.Invoke(view);

        TabType type = view.tabType;

        if (type == TabType.Player)
            OnClikedIPlayerItem?.Invoke(view);
        if (type == TabType.Shop)
            OnClikedIShopItem?.Invoke(view);
    }
}
