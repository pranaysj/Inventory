
public class PlayerModel
{
    private int money = 0;
    private int weight = 0;
    private int maxWeight = 30;
    private int totalValue = 0;
    private int bagDefaultValue = 25;

    public int Money { get => money; set => money = value; }
    public int Weight { get => weight; set => weight = value; }
    public int MaxWeight => maxWeight;
    public int TotalValue { get => totalValue; set => totalValue = value; }
    public int BagDefaultValue => bagDefaultValue;


    internal int UpdateBagWeight(int weight)
    {
        this.weight += weight;
        return this.weight;
    }
}
