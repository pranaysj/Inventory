using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private SoundService soundService;

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

    public ShopDatabaseSO ShopDatabase => shopDatabase;
    public GameObject ShopItemPrefab => shopItemPrefab;
    public GameObject PlayerItemPrefab => playerItemPrefab;
    public ShopView ShopView => shopView;
    public PlayerView PlayerView => playerView;
    public TransactionView TransactionView => transactionView;
    public SoundService SoundService => soundService;

    private EventService eventService;
    private ShopService shopService;
    private PlayerService playerService;
    private TransactionService transactionService;

    private void Awake()
    {

        eventService = new EventService();
        ServiceLocator.Register(this);
        ServiceLocator.Register(eventService);
        ServiceLocator.Register(uiService);

        shopService = new ShopService();
        playerService = new PlayerService();
        ServiceLocator.Register(shopService);
        ServiceLocator.Register(playerService);

        transactionService = new TransactionService();
        ServiceLocator.Register(transactionService);

        soundService = new SoundService(soundSO, sfxSource, bgMusicSource);
        ServiceLocator.Register(soundService);
    }

    private void Update()
    {
        eventService.Update();
    }

    public void SwitchTab(int tanID)
    {
        //get the switch function from the controller
        if (shopService != null)
        {
            shopService.Switch(tanID);
        }
        else
        {
            Debug.LogError("ShopService is not initialized in GameService.");
        }
        ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.TabChanged);
    }
}
