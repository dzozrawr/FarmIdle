using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Collider hitBoxCollider=null;
    public PlantController plant=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnHit(){
        plant.Grow();
        plant.PlantGrown+=OnPlantGrown;
    }

    public void OnPlantGrown(){
        Debug.Log("Plant finished growing.");

        plant.PlantGrown-=OnPlantGrown;
    }

}
