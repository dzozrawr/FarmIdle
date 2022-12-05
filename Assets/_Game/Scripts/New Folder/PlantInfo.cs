
using System.Runtime.Serialization;
[DataContract]
public class PlantInfo
{
    public enum PlantType
    {
        Sunflower, Tulip, Daffodil, Pepper, Tomato, Pumpkin, Strawberry, Watermelon
    }

    [DataMember]
    private PlantType type;
    private int price;

    public PlantType Type { get => type; }
    public int Price { get => price; }


    public PlantInfo(PlantType type, int price)
    {
        this.type = type;
        this.price = price;
    }

}
