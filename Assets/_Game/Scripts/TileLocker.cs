using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileLocker : MonoBehaviour
{
    public bool isLocked = false;   //all the tiles are unlocked by default

    public Canvas lockedUI = null;

    public int price = 0;

    public TMP_Text priceText = null;

    private List<TileController> tileControllers = null;

    private GameController gameController = null;

    private void Awake()
    {
        priceText.text = price + "";
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isLocked)
        {
            lockedUI.gameObject.SetActive(true);
            if (tileControllers == null) tileControllers = new List<TileController>();
            foreach (TileController tc in GetComponentsInChildren<TileController>())
            {
                tileControllers.Add(tc);
                tc.Lock();
            }
        }
    }

    public void Unlock()
    {
        if (GameController.CoinAmount >= price)
        {
            if (gameController == null) gameController = GameController.Instance;
            gameController.AddCoins(-price);
        }else{
            return;
        }

        lockedUI.gameObject.SetActive(false);

        foreach (TileController tc in tileControllers)
        {
            tc.Unlock();
        }
    }
}
