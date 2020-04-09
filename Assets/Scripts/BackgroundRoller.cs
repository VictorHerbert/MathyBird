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
        
        offsetGround += groundSpeed*GameController.instance.speed;
        offsetBackground += backgroundSpeed*GameController.instance.speed;

        if(GameController.instance.gameState == GameState.running){
            blend = blendCurve.Evaluate(GameController.instance.elapsedTime);
            backgroundMat.SetFloat("_blend", blend);
        }

        groundMat.SetTextureOffset("_BaseMap",new Vector2(offsetGround,0));
        backgroundMat.SetVector("_offset",new Vector2(offsetBackground,0));
        
        
    }


}

