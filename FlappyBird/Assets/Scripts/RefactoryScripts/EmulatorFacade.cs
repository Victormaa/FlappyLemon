using System.Threading;
using System.Collections.Generic;
using System;

public class EmulatorFacade
{
    #region Execution
    public void OnAwake()
    {
        FlappyBirdEmulator.Instance.OnAwake();
    }

    public void OnStart()
    {
        FlappyBirdEmulator.Instance.OnStart();
    }

    public void OnEarlyUpdate()
    {
        FlappyBirdEmulator.Instance.OnEarlyUpdate();
    }

    public void OnUpdate()
    {
        FlappyBirdEmulator.Instance.OnUpdate();
    }

    public void OnLateUpdate()
    {
        FlappyBirdEmulator.Instance.OnLateUpdate();
    }

    public void DisposeEmulator()
    {
        FlappyBirdEmulator.Dispose();
    }
    #endregion
    public void CreateMatch()
    {
        FlappyBirdEmulator.Instance.CreateMatch();
    }

    internal void PauseMatch()
    {
        FlappyBirdEmulator.Instance.PauseMatch();
    }
    public void UnpauseMatch()
    {
        FlappyBirdEmulator.Instance.UnpauseMatch();
    }
    internal void DisposeMatch()
    {
        FlappyBirdEmulator.Instance.UnpauseMatch();
    }

    public GameStage GetGameStage()
    {
        return FlappyBirdEmulator.Instance.match.gameStage;
    }
    public GameState GetGameState()
    {
        return FlappyBirdEmulator.Instance.state;
    }
}
