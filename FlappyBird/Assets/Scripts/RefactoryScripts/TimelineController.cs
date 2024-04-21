using UnityEngine;

/// <summary>
/// Not allow chase frame
/// 30 fps stable
/// </summary>

public enum GameplayRoleMode
{
    EvE, // AI vs AI
    PvE, // human vs AI
    PvP_Offline, // human vs human, offline mode, no network
    PvP_Online, // human vs human, online mode, via network
    PvP_AI, // human as AI, pvp mode, no network
}
public class TimelineController
{
    float _deltaTime;
    public float deltaTime => _deltaTime;
    float[] _prevUpdateTime;
    public float interpolation;
    bool _isUpdateNextFrame;
    public bool isUpdateNextFrame => _isUpdateNextFrame;
    float _frameDuration;

    public void OnStart(GameplayRoleMode roleMode)
    {
        if (roleMode == GameplayRoleMode.PvP_Online || roleMode == GameplayRoleMode.PvP_AI)
        {
            _frameDuration = FlappyBirdEmulatorController.PVP_GAME_LOGIC_FRAME_DURATION;
        }
        else
        {
            _frameDuration = FlappyBirdEmulatorController.PVE_GAME_LOGIC_FRAME_DURATION;
        }
        interpolation = 1f;
        _prevUpdateTime = new float[10];
    }

    public bool Is30FPS()
    {
        return Application.targetFrameRate == 30 && FlappyBirdEmulatorController.GAME_LOGIC_FRAME_RATE == 30 && QualitySettings.vSyncCount == 0;
    }

    public void OnUpdate()
    {
        // avg update time
        for (int i = _prevUpdateTime.Length - 1; i > 0; i--)
        {
            _prevUpdateTime[i] = _prevUpdateTime[i - 1];
        }
        _prevUpdateTime[0] = Time.deltaTime;

        // 30fps
        if (Is30FPS())
        {
            _isUpdateNextFrame = true;
            interpolation = 1.0f;
            _deltaTime = 1.0f / 30;
            return;
        }

        int avgCount = Mathf.CeilToInt((float)Application.targetFrameRate / FlappyBirdEmulatorController.GAME_LOGIC_FRAME_RATE);
        if (avgCount < 1) avgCount = 1;
        float sumTime = 0;
        for (int i = 0; i < avgCount; i++)
        {
            sumTime += _prevUpdateTime[i];
        }
        float avgUpdateTime = sumTime / avgCount;

        _deltaTime = avgUpdateTime;
        float remainLogicFrameTime = (1 - interpolation) * _frameDuration;
        if (remainLogicFrameTime <= _deltaTime)
        {
            // update game logic once afterwards
            _isUpdateNextFrame = true;
            interpolation = Mathf.Clamp01((_deltaTime - remainLogicFrameTime) / _frameDuration);
        }
        else
        {
            _isUpdateNextFrame = false;
            interpolation += _deltaTime / _frameDuration;
        }
    }

}