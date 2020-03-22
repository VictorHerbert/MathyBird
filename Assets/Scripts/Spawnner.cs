using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject pipe;

    [Header("Params")]
    public float distanceBetween = 0;

    [Header("Ranges")]
    public float minY;
    public float maxY;
    public float maxRange;
    

    float instTime;
    Vector3 instPos;
    float prevY;

    void Start()
    {
        instPos = transform.position;
        prevY = 0;
    }

    void Update()
    {
        if(Time.time > instTime){
            instPos.y = Random.Range(Mathf.Max(minY,prevY-maxRange), Mathf.Min(maxY,prevY+maxRange));
            prevY = instPos.y;

            Instantiate(
                pipe,
                instPos,
                Quaternion.identity,
                transform
            );


            instTime = distanceBetween/Mathf.Max(GameController.gameSpeed,.001f)+Time.time;
        }
    }

}
