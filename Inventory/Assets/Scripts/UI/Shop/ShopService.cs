
public class ShopService
{
    private ShopController shopController;
    public ShopController ShopController => shopController;

    public ShopService(GameService gameService, UIService uIService)
    {
        shopController = new ShopController(gameService, uIService);
    }

    public void Switch(int tabID)
    {
        shopController.Switch(tabID);
    }
}
