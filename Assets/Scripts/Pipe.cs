using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float minY,maxY;

    Vector3 pos;

    const float MAX_SCREEN_TIME = 5;


    
    void Start()
    {
        pos = transform.position;
        pos.y -= GameController.gameSpeed;
        transform.position = pos;
        Destroy(gameObject, MAX_SCREEN_TIME);
    }

    void Update()
    {   
        pos.x -= GameController.gameSpeed;
        transform.position = pos;
    }
}
