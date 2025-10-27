using System;
using UnityEngine;
public enum TabType
{
    Shop,
    Player
}

public class GameService : MonoBehaviour
{
    [Header("HIERARCHY : UIService")]
    [SerializeField] private UIService uiService;

    [SerializeField] private ShopView shopView;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private TransactionView transactionView;

    [Header("PROJECT")]
    [Header("ScriptableObject")]
    [SerializeField] private ShopDatabaseSO shopDatabase;
    [SerializeField] private SoundSO soundSO;

    [Header("Prefab")]
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject playerItemPrefab;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgMusicSource;

    public ShopView ShopView => shopView;
    public GameObject ShopItemPrefab => shopItemPrefab;
    public GameObject PlayerItemPrefab => playerItemPrefab;
    public ShopDatabaseSO ShopDatabase => shopDatabase;

    private ShopService shopService;
    private EventService eventService;
    private SoundService soundService;

    private InventoryController inventoryController;

    private void Awake()
    {
        Debug.Log("Hello");
        ServiceLocator.Register(this);
        ServiceLocator.Register(uiService);

        ServiceLocator.Register(new SoundService(soundSO, sfxSource, bgMusicSource));
        soundService = ServiceLocator.Get<SoundService>();

        ServiceLocator.Register(new EventService(soundService));
        eventService = ServiceLocator.Get<EventService>();

        ServiceLocator.Register(new ShopService(shopDatabase, this, uiService, shopView));
        shopService = ServiceLocator.Get<ShopService>();

        ServiceLocator.Register(new PlayerService(playerView, uiService, this));
        ServiceLocator.Register(new TransactionService(transactionView));

    }

    private void Start()
    {
        inventoryController = new InventoryController(uiService.InventoryPanel, eventService);
    }

    private void Update()
    {
        eventService.Update();
    }

    public void SwitchTab(int tabID)
    {
        //get the switch function from the controller
        if (shopService != null)
        {
            shopService.Switch(tabID);
        }
        else
        {
            Debug.LogError("ShopService is not initialized in GameService.");
        }
        soundService.PlaySoundEffects(SoundType.TabChanged);
    }

    private void OnDestroy()
    {
        ServiceLocator.Clean();
    }
}
