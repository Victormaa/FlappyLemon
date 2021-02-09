using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Test_Temp")]
    public Text gameState_display;
    public GameObject Menu_display;

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
    #endregion

    public bool isPlaying = false;

    public bool isMenu = true;

    public bool isPause = false;

    public void InitialGame()
    {
        SwitchState("Menu");
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
        switch (GameState)
        {
            case "Menu":
                break;
            case "Playing":
                if (Input.GetKeyDown(KeyCode.P))
                {
                    SwitchState("Pause");
                }
                switch (InGameState)
                {
                    case "Normal":
                        break;
                    case "WindOn":
                        break;
                    default:
                        break;
                }
                break;
            case "Pause":
                if (Input.GetKeyDown(KeyCode.P))
                {
                    SwitchState("Playing");
                }
                break;
            case "End":
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
                isPlaying = false;
                break;
            case "Pause":
                isPause = false;
                break;
            case "End":
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
