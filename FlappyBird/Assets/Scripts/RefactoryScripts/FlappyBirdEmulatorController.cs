using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum EmulatorState
{
    None,
    Created,
    Awaked,
    Started,
    MatchCreated,
}
public class FlappyBirdEmulatorController
{
    #region Singleton
    private static FlappyBirdEmulatorController _instance;
    public static FlappyBirdEmulatorController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FlappyBirdEmulatorController();
            }
            return _instance;
        }
    }
    private FlappyBirdEmulatorController()
    {
    }
    #endregion

    public static int GAME_LOGIC_FRAME_RATE = 30;
    public static float PVE_GAME_LOGIC_FRAME_DURATION = 1.0f / GAME_LOGIC_FRAME_RATE;
    public static float PVP_GAME_LOGIC_FRAME_DURATION = 0.03333f;
    public float frameDelta = 0;

    private EventWaitHandle _mainThreadGo = new EventWaitHandle(false, EventResetMode.AutoReset);
    private EventWaitHandle _logicThreadGo = new EventWaitHandle(false, EventResetMode.AutoReset);

    public EmulatorFacade emulator;
    public EmulatorState emulatorState;
    public TimelineController timelineController;
    int updateTimes = 0;
    bool _isUseLogicThread { get;}
    public void StartEmulator()
    {
        if (emulator == null)
        {
            emulator = new EmulatorFacade();
            emulatorState = EmulatorState.Created;
            emulator.OnAwake();
            emulatorState = EmulatorState.Awaked;
            emulator.OnStart();
            emulatorState = EmulatorState.Started;
            timelineController = new TimelineController();
        }
    }
    public void StartGame()
    {
        emulator.CreateMatch();
        emulatorState = EmulatorState.MatchCreated;
        timelineController.OnStart(GameplayRoleMode.PvE);
        updateTimes = 1;
        emulator.UnpauseMatch();
    }
    public void OnUpdate()
    {
        if (emulatorState == EmulatorState.MatchCreated)
        {
            timelineController.OnUpdate();
            if (timelineController.isUpdateNextFrame)
            {
                //MatchConfigData matchConfigData = emulator.GetMatchConfigData();

                // input update need to be in main thread
                //if (matchConfigData.inputCofigData.localDeviceInputs != null)
                //{
                //    foreach (var localDeviceInput in matchConfigData.inputCofigData.localDeviceInputs)
                //    {
                //        localDeviceInput.device.OnUpdate();
                //    }
                //}

                
                //networkDevice.OnUpdate(); // local input -> network device -> input
                //if (updateTimes == 0)
                //    timelineController.interpolation = 1;

                //foreach (var deviceInput in matchConfigData.inputCofigData.deviceInputs) // match input
                //{
                //    deviceInput.device.OnUpdate();
                //}

                if (_isUseLogicThread)
                {
                    WaitHandle.SignalAndWait(_logicThreadGo, _mainThreadGo);
                }
                else
                {
                    UpdateEmulator();
                }
            }
            frameDelta = emulator.GetGameState() == GameState.InGamePlaying ? timelineController.interpolation : 1;
        }
#if (DEBUG && !PERFORMANCE) && !UI_DEVELOP_MODE
        //BasketballDebugManager.Instance.frameDelta = frameDelta;
        //BasketballDebugManager.Instance.Update();
#endif
    }
    public void UpdateEmulator()
    {
        for (int i = 0; i < updateTimes; i++)
        {
            // logic frame update
            //networkDevice.OnLogicUpdate();
            emulator.OnEarlyUpdate();
            emulator.OnUpdate();
            emulator.OnLateUpdate();
            //UpdateUtils.Instance.OnUpdate();
            //matchSceneManager.OnLogicUpdate();
#if (DEBUG && !PERFORMANCE) && !UI_DEVELOP_MODE
            //BasketballDebugManager.Instance.LogicUpdate();
#endif
        }
    }
    internal void OnLateUpdate()
    {
        if (emulatorState == EmulatorState.MatchCreated)
        {
            //matchSceneManager.OnLateUpdate();
        }
    }
    public void DisposeMatch()
    {
        emulator.DisposeMatch();
        emulatorState = EmulatorState.Started;
    }
    internal void EndGame()
    {
        //if (_isUseLogicThread)
        //{
        //    _logicThread.Join();
        //}
        emulator.PauseMatch();
        //Physics.autoSimulation = _autoSimulationBeforeMatch;
    }
}
