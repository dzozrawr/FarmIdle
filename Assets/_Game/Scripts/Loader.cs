using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private int levelToLoad=-1;
    private void Awake()
    {
        //TTPCore.Setup();

        SaveData saveData = SaveSystem.LoadGameXML();
        if (saveData != null)
        {
            levelToLoad = saveData.level;
            ShopController.shopItemInfos=saveData.shopItemInfos;
            GameController.CoinAmount=saveData.money;
            WindmillScript.Lvl=saveData.windmillLevel;
            MarketScript.Lvl=saveData.marketLevel;
        }
        else
        {
            levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
           // GameController.missionID = 1;
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
