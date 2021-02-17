using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Test_Temp")]
    public Text gameState_display;
    public GameObject Menu_display;
    public GameObject ThanksForTest;

    #region Singleton for GameManager
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            GameManager[] gameManagers = FindObjectsOfType(typeof(GameManager)) as GameManager[];
            if (gameManagers.Length == 0)
            {
                Log.Warning("GameManager not present on the scene. Creating a new one.");
                GameManager manager = new GameObject("GameManager").AddComponent<GameManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return gameManagers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Log.Error("You can only use one UduinoManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    #endregion

    [Header("Genera")]
    //"Menu", "Playing", "Pause", "End"
    public string GameState = "Menu";

    //"Normal", "WindOn"
    public string InGameState = "Normal";

    public GameObject WarningSign;
    public Text game_Score_text;
    public int game_Score = 0;

    #region Event on Different State
    [HideInInspector]
    public UnityEvent onMenuEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent onPlayingEvent = new UnityEvent();
        [HideInInspector]
        public UnityEvent onPlayingNormalEvent = new UnityEvent();
        [HideInInspector]
        public UnityEvent onPlayingWindOnEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent onPauseEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent onEndEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent offMenuEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent offPlayingEvent = new UnityEvent();
         [HideInInspector]
         public UnityEvent offPlayingNormalEvent = new UnityEvent();
         [HideInInspector]
         public UnityEvent offPlayingWindOnEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent offPauseEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent offEndEvent = new UnityEvent();

    public bool isPlaying = false;

    public bool isMenu = true;

    public bool isPause = false;
    #endregion

    [Header("Game Design")]
    public int[] _windOnPoint;
    public int[] _windOffPoint;

    private Queue<int> WindOnPoint = new Queue<int>();
    private Queue<int> WindOffPoint = new Queue<int>();


    public void InitialGame()
    {
        game_Score = 0;
        game_Score_text.text = 0.ToString();

        //set up Wind on/off point
        foreach (int a in _windOnPoint)
        {
            WindOnPoint.Enqueue(a);
        }

        foreach (int a in _windOffPoint)
        {
            WindOffPoint.Enqueue(a);
        }
    }

    private void Awake()
    {
        InitialGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        onMenuEvent.AddListener(onMenu);
        offMenuEvent.AddListener(offMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        switch (GameState)
        {
            case "Menu":
                break;
            case "Playing":
                if(game_Score == 10)
                {
                    SwitchState("End");
                    //Thanks for test
                    ThanksForTest.SetActive(true);
                }
                    
                if (Input.GetKeyDown(KeyCode.P))
                {
                    SwitchState("Pause");
                }
                switch (InGameState)
                {
                    case "Normal":
                        if (game_Score == WindOnPoint.Peek() - 1)
                        {
                            WarningSign.SetActive(true);
                        }
                            
                        if (game_Score == WindOnPoint.Peek())
                        {
                            WindOnPoint.Dequeue();
                            SwitchInGameState("WindOn");
                        }
                        break;
                    case "WindOn":
                        if (game_Score == WindOffPoint.Peek())
                        {
                            WindOffPoint.Dequeue();
                            SwitchInGameState("Normal");
                        }
                        break;
                    default:
                        break;
                }
                game_Score_text.text = game_Score.ToString();
                break;
            case "Pause":
                if (Input.GetKeyDown(KeyCode.P))
                {
                    SwitchState("Playing");
                }
                break;
            case "End":
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SwitchState("ForEndoff");
                    SceneManager.LoadScene(0);
                }
                break;
            default:
                break;
        }
    }

    public void SwitchState(string State)
    {
        if (GameState == State)
            return;
        //onExit
        switch (GameState)
        {
            case "Menu":
                offMenuEvent.Invoke();
                isMenu = false;
                break;
            case "Playing":
                offPlayingEvent.Invoke();
                isPlaying = false;
                break;
            case "Pause":
                offPauseEvent.Invoke();
                isPause = false;
                break;
            case "End":
                offEndEvent.Invoke();
                break;
            default:
                break;
        }

        GameState = State;

        // onEnter
        switch (GameState)
        {
            case "Menu":
                onMenuEvent.Invoke();
                StateCheck("Menu");
                isMenu = true;
                break;
            case "Playing":
                StateCheck("Playing");
                onPlayingEvent.Invoke();
                isPlaying = true;
                break;
            case "Pause":
                StateCheck("Pause");
                onPauseEvent.Invoke();
                isPause = true;
                break;
            case "End":
                onEndEvent.Invoke();
                StateCheck("End");
                break;
            default:
                break;
        }
    }

    public void SwitchInGameState(string State)
    {
        if (InGameState == State)
            return;
        //onExit
        switch (InGameState)
        {
            case "Normal":
                offPlayingNormalEvent.Invoke();
                break;
            case "WindOn":
                offPlayingWindOnEvent.Invoke();
                break;
            default:
                break;
        }
        InGameState = State;
        //onEnter
        switch (InGameState)
        {
            case "Normal":
                onPlayingNormalEvent.Invoke();
                break;
            case "WindOn":
                onPlayingWindOnEvent.Invoke();
                break;
            default:
                break;
        }
    }

    private void onMenu()
    {
        Menu_display.SetActive(true);
        InitialGame();
    }

    private void offMenu()
    {
        Menu_display.SetActive(false);
    }

    private void StateCheck(string State)
    {
        gameState_display.text = "Game State: " + State;
    }

    public void ArduinoSetOnNoDevice()
    {
        UduinoManager.Instance.autoReconnect = false;
    }
}
