using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    const float MAX_SCREEN_TIME = 14;

    Vector3 pos;
    
    void Start()
    {
        pos = transform.position;

        Destroy(gameObject, MAX_SCREEN_TIME);
    }

    void Update()
    {
        if (GameController.instance.gameState == GameState.running){
            pos.x -= GameController.instance.speed;
            transform.position = pos;
        }
    }
}
