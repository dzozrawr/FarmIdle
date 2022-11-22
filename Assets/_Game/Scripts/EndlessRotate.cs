using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRotate : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;

    public float rotSpeed=10f;

    public Space space = Space.World;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotSpeed * Time.deltaTime, space);
    }
}
