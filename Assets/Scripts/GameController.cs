using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState{
    menu, toStart, ready, running, hit, finished
}

public class GameController : MonoBehaviour
{

#region variables

    static GameController _instance;
    
    
    [Header("Params")]
    [SerializeField] float _startSpeed = .01f;
    [SerializeField] float _speed = .01f;
    [SerializeField] float _speedIncrease = .001f;

    const float MAX_SPEED = 0.14f;

    public GameState gameState = GameState.menu;

    int _score;

    float startTime;
    public float elapsedTime{
        get => ((gameState == GameState.running) ? (Time.time - startTime) : 0); 
    }
    public float elapsedMenuTime{
        get => ( ((gameState == GameState.toStart)||(gameState == GameState.ready)) ? (Time.time - startTime) : 0); 
    }


    [Header("GameObjects")]
    public TextMeshProUGUI scoreText;
    public GameObject tapIcon;
    public Animator menu;


#endregion

#region Getters/Setters

    public static GameController instance{
        get => _instance;
    }

    public float speed {
        get => _speed;
        set {
            if((value >= 0) && (value < MAX_SPEED))
                _speed = value;
        }
        
    }

     public float startSpeed {
        get => _startSpeed;
    }

    public float speedIncrease {
        get => _speedIncrease;
        set {
            if(value >= 0)
                _speedIncrease = value;
        }
        
    }

    public int score{
        get => _score;
        set {
            if(value >= 0){
                _score = value;
                scoreText.text = score.ToString();
            }
        }
    }

#endregion

    void Start()
    {
        _instance = this;
    }

    public void onMenuEnter(){
        menu.SetBool("GameStarted",true);
        gameState = GameState.toStart;
        startTime = Time.time;
    }

    public void onStartGame(){
        gameState = GameState.running;
        startTime = Time.time;

        tapIcon.SetActive(false);
        speed = startSpeed;
    }
    

    void Update()
    {
        if(gameState == GameState.running){
            speed += Time.deltaTime*speedIncrease;
        }
        else{
            speed = 0;


            if(gameState == GameState.finished){
                SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
            }
        }
    }
}