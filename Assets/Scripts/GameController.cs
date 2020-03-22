using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float _gameSpeed = .01f;

    public static float gameSpeed {
        get => _gameSpeed;
        set {
            if(value > 0)
                _gameSpeed = value;
        }
        
    }
    
    public float speed;
    public float speedIncreaseRate = 0.01f;

    const float MAX_SPEED = 0.14f;


    void Start()
    {
        
    }

    void Update()
    {   
        if(speed < MAX_SPEED)
            speed += Time.deltaTime*speedIncreaseRate;
        gameSpeed = speed;
    }
}
