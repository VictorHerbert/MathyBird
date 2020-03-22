using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Params")]
    public float tapForce;

    [Header("Score")]
    public float fitness = 0;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    public float angleScale;

    void Update(){
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0,0,rb.velocity.y*angleScale),
            1
        );
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if(col.name == "Pipe(Clone)")
            fitness ++;
        else
            onfinishBird();
    }

    public void onTap(){
        rb.velocity = new Vector2(0,tapForce);
    }

    public void onfinishBird(){
        GameController.isRunning = false;
    }
}
