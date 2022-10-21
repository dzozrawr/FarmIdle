using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvasController : MonoBehaviour
{
    public TMP_Text coinAmountTxt=null;

    private GameController gameController=null;
    // Start is called before the first frame update
    void Start()
    {
        gameController=GameController.Instance;

        gameController.CoinAmountChanged+=OnCoinAmountChanged;
    }


    public void OnCoinAmountChanged(){
        coinAmountTxt.text= GameController.CoinAmount+"";            
    }
}
