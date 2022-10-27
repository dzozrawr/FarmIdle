public class PlantInfo 
{
    public enum PlantType{
        Capsule, MultiPlant
    }

    private PlantType type;
    private int price;

    public PlantType Type { get => type;  }
    public int Price { get => price; }
    

    public PlantInfo(PlantType type, int price)
    {
        this.type = type;
        this.price = price;
    }
    
}
