using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject pipe;

    [Header("Params")]
    public float distanceBetween = 0.06f;

    [Header("Ranges")]
    public float minY = -0.7f;
    public float maxY = 2.6f;
    public float maxRange = .12f;
    

    float instTime = 0;
    Vector3 instPos;
    float prevY;

    void Start()
    {
        instPos = transform.position;
        prevY = 0;

    }

    void Update()
    {
        if(GameController.instance.gameState == GameState.running){
            /*if(instTime == 0){
                instTime = distanceBetween/Mathf.Max(GameController.instance.speed,GameController.instance.startSpeed)+Time.time;
                Debug.Log(distanceBetween/Mathf.Max(GameController.instance.speed,GameController.instance.startSpeed));
            }*/
            if(Time.time > instTime){
                

                instPos.y = Random.Range(Mathf.Max(minY,prevY-maxRange), Mathf.Min(maxY,prevY+maxRange));
                prevY = instPos.y;

                Instantiate(
                    pipe,
                    instPos,
                    Quaternion.identity,
                    transform
                );


                instTime = distanceBetween/Mathf.Max(GameController.instance.speed,GameController.instance.startSpeed)+Time.time;
                //Debug.Log(distanceBetween/Mathf.Max(GameController.instance.speed,GameController.instance.startSpeed));
            }
        }
    }

}
