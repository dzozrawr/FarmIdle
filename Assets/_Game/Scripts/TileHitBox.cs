using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHitBox : MonoBehaviour
{
    public Collider _collider=null;
    public TileController tileController=null;


    public void OnHit(){
        tileController.OnHit();        
    }
    
}
