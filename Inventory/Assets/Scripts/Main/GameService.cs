using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private EventService eventService;

    [SerializeField] private GameObject inventoryPanel;
    public GameObject InventoryPanel => inventoryPanel;

    private void Awake()
    {
        eventService = new EventService();
        ServiceLocator.Register(eventService);
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        eventService.Update();
    }
}
