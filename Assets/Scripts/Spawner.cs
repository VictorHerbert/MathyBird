using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

#region "Inspector"

    [Header("GameObjects")]
    public GameObject pipe;

    [Header("Params")]
    public float distanceBetween = 0.06f;

    [Header("Ranges")]
    public float minY = -0.7f;
    public float maxY = 2.6f;
    public float maxRange = .12f;    

    [Header("Status")]
    public static Vector2 closestPipePos;

#endregion

#region "Variables"

    float instTime = 0;
    Vector3 instPos;
    float prevY;

    Queue<GameObject> pipes = new Queue<GameObject>();

#endregion

    void Start()
    {
        instPos = transform.position;
        prevY = 0;

    }

    void Update()
    {
        if(GameController.instance.gameState == GameState.running){
            if(Time.time > instTime){
                
                instPos.y = Random.Range(Mathf.Max(minY,prevY-maxRange), Mathf.Min(maxY,prevY+maxRange));
                prevY = instPos.y;

                pipes.Enqueue(
                    Instantiate(
                        pipe,
                        instPos,
                        Quaternion.identity,
                        transform
                    )
                );

                instTime = distanceBetween/GameController.instance.speed+Time.time;
            }
            if(pipes.Count != 0){
                if(pipes.Peek().transform.position.x >= 0)
                    closestPipePos = pipes.Peek().transform.position;
                else
                    pipes.Dequeue(); 
            }          
        }
    }

}
