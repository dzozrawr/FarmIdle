using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using static ShopController;
using UnityEngine.Scripting;

[DataContract]
public class SaveData
{
    [DataMember]
    public int level;
    [DataMember]
    public int money;
    [DataMember]
    public KeyValuePair<PlantInfo.PlantType, ShopItemInfo> s;

    [DataMember]
    public List<ShopItemInfo> shopItemInfos;
    [DataMember]
    public int windmillLevel;
    [DataMember]
    public int marketLevel;


    public SaveData(int _level)
    {
        level = _level;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
        shopItemInfos = ShopController.shopItemInfos;
        windmillLevel = WindmillScript.Lvl;
        marketLevel = MarketScript.Lvl;
    }

    public SaveData()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
        shopItemInfos = ShopController.shopItemInfos;
        windmillLevel = WindmillScript.Lvl;
        marketLevel = MarketScript.Lvl;
    }





}
