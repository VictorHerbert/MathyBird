using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

#region variables

    static GameController _instance;
    
    
    [Header("Params")]
    [SerializeField] float _startSpeed = .01f;
    [SerializeField] float _speed = .01f;
    [SerializeField] float _speedIncrease = .001f;

    const float MAX_SPEED = 0.14f;

    int _score;
    public bool isRunning = false;
    public bool isFinished = false;

    [Header("GameObjects")]
    public TextMeshProUGUI scoreText;
    public GameObject tapIcon;


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

    public void onStartGame(){
        isRunning = true;
        tapIcon.SetActive(false);
        speed = startSpeed;
    }
    

    void Update()
    {
        if(isRunning){
            speed += Time.deltaTime*speedIncrease;
        }
        else{
            speed = 0;


            if(isFinished){
                isRunning = true;
                isFinished = false;
                SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
            }
        }
    }
}
