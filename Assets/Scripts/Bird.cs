using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Params")]
    public float tapForce;

    [Header("Score")]
    public int fitness = 0;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public float angleScale;

    void Update(){
        if ( (GameController.instance.gameState == GameState.toStart) || (GameController.instance.gameState == GameState.ready) )
        {
            transform.position = new Vector3(transform.position.x,.5f*Mathf.Sin(Time.time),transform.position.z);

        }
        if(GameController.instance.gameState == GameState.running){
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0,0,rb.velocity.y*angleScale),
                .8f
            );
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Pipe(Clone)"){
            onPassPipe();
        }
        else
            onfinishBird();
    }

    public void onTap(){
        rb.gravityScale = 2;
        rb.velocity = new Vector2(0,tapForce);
    }

    public void onPassPipe(){
        fitness++;
        GameController.instance.score=fitness;
    }

    public void onfinishBird(){
        GameController.instance.gameState = GameState.finished;
    }
}