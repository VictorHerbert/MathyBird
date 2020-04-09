using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D rb;
    NeuralNetwork network;

    [Header("Params")]
    public float tapForce;
    public float cooldown;
    float prevtime = 0;
    public float angleScale;

    [Header("Score")]
    public int score = 0;
    public float fitness = 0;



    void Start(){
        rb = GetComponent<Rigidbody2D>();
        network = GetComponent<NeuralNetwork>();
        rb.gravityScale = 0;
    }

    

    void Update(){
        if (GameController.instance.gameState == GameState.ready)
            transform.position = new Vector3(transform.position.x,.5f*Mathf.Sin(Time.time),transform.position.z);
        if(GameController.instance.gameState == GameState.running){
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0,0,rb.velocity.y*angleScale),
                .8f
            );
        }
        fitness = Time.timeSinceLevelLoad;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Pipe(Clone)")
            onPassPipe();        
        else
            onfinishBird();
    }

    public void onTap(){
        rb.gravityScale = 2;
        if(Time.time - prevtime >= cooldown){
            prevtime = Time.time;
            rb.velocity = new Vector2(0,tapForce);
        }
    }

    public void onPassPipe(){
        score++;
    }

    public void onfinishBird(){
        if(network.weightData.fitness < fitness)
            network.SaveFile(fitness);
        GameController.instance.gameState = GameState.finished;
    }
}