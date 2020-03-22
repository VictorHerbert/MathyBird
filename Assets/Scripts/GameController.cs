using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static float _gameSpeed = .01f;

    public static float gameSpeed {
        get => _gameSpeed;
        set {
            if(value >= 0)
                _gameSpeed = value;
        }
        
    }
    
    public float speed;
    public float speedIncreaseRate = 0.01f;

    const float MAX_SPEED = 0.14f;

    public static bool _isRunning = true;
    
    public static bool isRunning{
        get => _isRunning;
        set {_isRunning = value;}
    }


    void Start()
    {
        
    }

    void Update()
    {
        if(isRunning){
            if(speed < MAX_SPEED)
                speed += Time.deltaTime*speedIncreaseRate;
            gameSpeed = speed;
        }
        else{
            gameSpeed = 0;


            isRunning = true;
            SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
        }
    }
}
