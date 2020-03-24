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

          if(GameController.instance.gameState == GameState.running){
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0,0,rb.velocity.y*angleScale),
                .8f
            );
        }
        else if(GameController.instance.gameState == GameState.toStart){
            Vector3 v = transform.position;
            if(v.x > 0){
                v.x -= vel*Time.deltaTime;
                vel -= 0.001f;
            }
            else
                GameController.instance.gameState = GameState.ready;
            v.y = .5f*Mathf.Sin(Time.time);
            transform.position = v;
        }

        
    }

    public float vel;
    public float timecorr;

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