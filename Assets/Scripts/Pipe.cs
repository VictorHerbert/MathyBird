using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    Vector3 pos;

    const float MAX_SCREEN_TIME = 14;

    
    void Start()
    {
        pos = transform.position;
        pos.y -= GameController.instance.speed;
        transform.position = pos;
        Destroy(gameObject, MAX_SCREEN_TIME);
    }

    void Update()
    {   
        pos.x -= GameController.instance.speed;
        transform.position = pos;
    }
}
