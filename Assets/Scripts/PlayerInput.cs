using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    Bird bird;

    void Start()
    {
        bird = gameObject.GetComponent<Bird>();
    }
    
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)){
            if(GameController.instance.gameState == GameState.ready){
                GameController.instance.onStartGame();
                bird.onTap();
            }
            if(GameController.instance.gameState == GameState.running){
                GameController.instance.onStartGame();
                bird.onTap();
            }
            
        }
        
    }
}
