using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    ready, running, hit, finished
}

public class GameController : MonoBehaviour
{

#region variables

    static GameController _instance;
    float startTime = 0;

    [Header("Player Params")]
    public float speed;
    public GameState gameState = GameState.ready;

    [Header("Neural Params")]
    public GameObject birdPrefab;    

#endregion

#region Getters/Setters
    public float elapsedTime
    {
        get => ((gameState == GameState.running) ? (Time.time - startTime) : 0);
    }

    public static GameController instance {
        get => _instance;
    }

    float MAX_SPEED = 1.0f;

#endregion

    void Awake()
    {
        _instance = this;
        gameState = GameState.ready;
    }

    //public int birdCount = 1;

    /*void Start(){
        for(int i = 0; i < birdCount; i++)
            Instantiate(birdPrefab).transform.position = new Vector3(0,0,-2);
    }*/


    public void onStartGame()
    {
        gameState = GameState.running;
        startTime = Time.time;
    }
    
    public void onRestart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator delayedStart()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


    void Update()
    {
        if (gameState == GameState.finished)
        {
            onRestart();
        }
        
    }


    
}
