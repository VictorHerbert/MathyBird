using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType{
    Keyboard, NeuralNetwork
}

public class PlayerInput : MonoBehaviour
{

#region "Components"   
    Bird bird;
    NeuralNetwork network;
#endregion

    [Header("Input")]
    public InputType inputType;
    
    [Header("Network")]
    public float[] networkInput = new float[3];

    void Start()
    {
        bird = gameObject.GetComponent<Bird>();
        network = gameObject.GetComponent<NeuralNetwork>();
        if(inputType == InputType.NeuralNetwork){
            GameController.instance.onStartGame();
            bird.onTap();
        }

    }

    IEnumerator delayedStart()
    {
        Debug.Log("INput ok");
        yield return new WaitForSeconds(2.0f);
        bird.onTap();        
    }

    bool neuralTap(){
        return network.getOuput()[0] > 0;
    }
    
    
    
    void Update()
    {
        networkInput[0] = transform.position.y;
        networkInput[1] = Spawner.closestPipePos.x;
        networkInput[2] = Spawner.closestPipePos.y;

        network.setInput(networkInput);        
        
        if(
            ((inputType == InputType.Keyboard)      && Input.GetKeyDown(KeyCode.Space)) ||
            ((inputType == InputType.NeuralNetwork) && neuralTap())
        ){
            if(GameController.instance.gameState == GameState.ready){
                GameController.instance.onStartGame();
                bird.onTap();
            }
            if(GameController.instance.gameState == GameState.running){
                bird.onTap();
            }
            
        }
        
    }
}
