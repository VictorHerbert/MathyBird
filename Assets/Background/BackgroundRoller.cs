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

    float offsetGround,offsetBackground;
    
    void Update()
    {
        offsetGround += groundSpeed*GameController.gameSpeed;
        offsetBackground += backgroundSpeed*GameController.gameSpeed;
        
        groundMat.SetTextureOffset("_MainTex",new Vector2(offsetGround,0));
        backgroundMat.SetTextureOffset("_MainTex",new Vector2(offsetBackground,0));
    }


}
