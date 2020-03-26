using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRoller : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material groundMat;
    [SerializeField] Material backgroundMat;

    
    
    [Header("Speed")]    
    public float groundSpeed;
    public float backgroundSpeed;

    [Header("Night shift")]
    [Range(0.0f,1.19f)]
    [SerializeField] float blend;
    public AnimationCurve startCurve;
    public AnimationCurve blendCurve;

    float offsetGround,offsetBackground;

    void Start()
    {
        blend = 0;
        backgroundMat.SetFloat("_blend", blend);
    }

    void Update()
    {
        if(GameController.instance.gameState == GameState.running){
            offsetGround += groundSpeed*GameController.instance.speed;
            offsetBackground += backgroundSpeed*GameController.instance.speed;

            blend = blendCurve.Evaluate(GameController.instance.elapsedTime);
            //Debug.Log(GameController.instance.elapsedTime);
                    
            backgroundMat.SetFloat("_blend", blend);
            groundMat.SetTextureOffset("_BaseMap",new Vector2(offsetGround,0));
            backgroundMat.SetVector("_offset",new Vector2(offsetBackground,0));
        }
        else if ((GameController.instance.gameState == GameState.toStart) || (GameController.instance.gameState == GameState.ready)){
            blend = startCurve.Evaluate(GameController.instance.elapsedMenuTime);

            offsetGround += groundSpeed*Mathf.Min(blend,GameController.instance.startSpeed);
            offsetBackground += backgroundSpeed*Mathf.Min(blend,GameController.instance.startSpeed);
            
            groundMat.SetTextureOffset("_BaseMap",new Vector2(offsetGround,0));
            backgroundMat.SetVector("_offset",new Vector2(offsetBackground,0));
        }
    }


}

