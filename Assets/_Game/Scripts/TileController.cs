using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Collider hitBoxCollider=null;
    public Transform placeForPlant=null;
    private PlantController plant=null;

    private TileState state;

    public PlantController Plant { get => plant; set => plant = value; }

    private void Awake() {
        state=new TileStartingState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnHit(){
        state.OnHit();
/*         if(plant==null){
            plant= Instantiate(GameController.Instance.curSelectedPlant,placeForPlant.position, Quaternion.identity,transform);            
        }

        plant.Plant();
        plant.PlantGrown+=OnPlantGrown; */
    }

    public void OnPlantGrown(){
     //   Debug.Log("Plant finished growing.");

        state=state.NextState();

        plant.PlantGrown-=OnPlantGrown;
    }

    public void GoToNextState(){
        state=state.NextState();
    }

    public void EnableHitboxAfterDelay(float delay){
        Invoke(nameof(EnableHitbox),delay);
    }

    public void EnableHitbox(){
        hitBoxCollider.enabled=true;
    }

}
