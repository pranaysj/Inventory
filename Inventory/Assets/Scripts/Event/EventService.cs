using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public event Action OnInventoryKeyPressed, OnPlayerGetItem;

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
}
