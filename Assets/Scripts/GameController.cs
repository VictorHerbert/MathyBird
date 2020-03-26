using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public enum GameState
{
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
    int _highScore = 0;

    float startTime = -100;
    public float elapsedTime
    {
        get => ((gameState == GameState.running) ? (Time.time - startTime) : 0);
    }
    public float elapsedMenuTime
    {
        get => (((gameState == GameState.toStart) || (gameState == GameState.ready)) ? (Time.time - startTime) : 0);
    }


    [Header("GameObjects")]
    public TextMeshProUGUI scoreText;
    public GameObject tapIcon;
    public GameObject menu;
    public GameObject bird;

    [Header("EndMenu")]
    public GameObject endMenu;
    public TextMeshProUGUI endMenuScore;
    public TextMeshProUGUI endMenuHighScore;


    #endregion

    #region Getters/Setters

    public static GameController instance
    {
        get => _instance;
    }

    public float speed
    {
        get => _speed;
        set
        {
            if ((value >= 0) && (value < MAX_SPEED))
                _speed = value;
        }

    }

    public float startSpeed
    {
        get => _startSpeed;
    }

    public float speedIncrease
    {
        get => _speedIncrease;
        set
        {
            if (value >= 0)
                _speedIncrease = value;
        }

    }

    public int score
    {
        get => _score;
        set
        {
            if (value >= 0)
            {
                _score = value;
                scoreText.text = score.ToString();
            }
        }
    }

    #endregion

    void Start()
    {
        if (_instance == null)
            Debug.Log("awakou");
        _instance = this;
    }

    public void onMenuEnter()
    {
        menu.GetComponent<Animator>().SetBool("GameStarted", true);

        gameState = GameState.toStart;
        startTime = Time.time;

        bird.transform.DOMoveX(0, 12);
        scoreText.rectTransform.DOAnchorPosY(-84.4f, 6, false);
        tapIcon.transform.DOMoveX(-1f, 10, false)
            .OnComplete(
                () => gameState = GameState.ready
            );
    }

    public void onStartGame()
    {
        gameState = GameState.running;
        startTime = Time.time;
        Debug.Log("??");

        tapIcon.SetActive(false);
        speed = startSpeed;
    }


    void Update()
    {
        if (gameState == GameState.ready)
        {

        }
        if (gameState == GameState.running)
        {
            speed += Time.deltaTime * speedIncrease;
        }
        else
        {
            speed = 0;


            if (gameState == GameState.finished)
            {
                showMenu();
            }
        }
    }

    void showMenu()
    {
        _highScore = Mathf.Max(PlayerPrefs.GetInt("Score", 0), score);
        PlayerPrefs.SetInt("Score", _highScore);

        scoreText.gameObject.SetActive(false);
        endMenuScore.text = _score.ToString();
        endMenuHighScore.text = _highScore.ToString();


        endMenu.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
