using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    None = 0,
    OutOfGame,
    InGamePlaying,
    InGamePausing,
}
public enum GameStage
{
    NotStart,
    GameEnd,
}
public class FlappyBirdEmulator 
{
    #region Singleton
    // singleton
    static private FlappyBirdEmulator _instance;
    static public FlappyBirdEmulator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FlappyBirdEmulator();
            }
            return _instance;
        }
    }
    private FlappyBirdEmulator()
    {
        //resourceLoader = new BasketballResourceLoader();
        //motionLoader = new BasketballMotionLoader();
        //eventEncoder = new GameEngineEventEncoder();
    }
    static public void Dispose()
    {
        _instance = null;
    }
    #endregion
    GameState _state = GameState.None;
    public GameState state => _state;

    internal FlappyBirdMatch match;

    public void OnAwake()
    {
        _state = GameState.OutOfGame;
        //motionLoader.OnAwake();
        //bindManager = BindManager.Instance;
        //bindManager.OnAwake();
    }

    public void OnStart()
    {
    }

    public void OnEarlyUpdate()
    {
        if (_state == GameState.InGamePlaying)
        {
            match.OnEarlyUpdate();
        }
    }

    public void OnUpdate()
    {
        if (_state == GameState.InGamePlaying)
        {
            match.OnUpdate();
        }
    }

    public void OnLateUpdate()
    {
        if (_state == GameState.InGamePlaying)
        {
            //_gameScenario.OnUpdate();
            match.OnLateUpdate();
        }
    }

    public void CreateMatch()
    {
        _state = GameState.InGamePausing;

        match = new FlappyBirdMatch();

        match.OnAwake();

        //_gameScenario.InitMatch();
        match.OnStart();
        //_gameScenario.OnInit();
    }

    public void DisposeMatch()
    {
        _state = GameState.OutOfGame;
        match.OnDestroy();
        match = null;
    }

    public void PauseMatch()
    {
        if (_state == GameState.InGamePlaying)
            _state = GameState.InGamePausing;
    }

    public void UnpauseMatch()
    {
        if (_state == GameState.InGamePausing)
            _state = GameState.InGamePlaying;
    }
}
