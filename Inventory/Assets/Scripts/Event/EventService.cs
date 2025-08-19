using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public event Action OnInventoryKeyPressed;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryKeyPressed?.Invoke();
        }
    }
}
