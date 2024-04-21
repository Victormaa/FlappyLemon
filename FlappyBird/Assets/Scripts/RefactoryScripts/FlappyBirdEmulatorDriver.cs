using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlappyBirdEmulatorDriver : MonoBehaviour
{
    public FlappyBirdEmulatorController flappyBirdEmulatorController;
    // Start is called before the first frame update
    private void Awake()
    {
        flappyBirdEmulatorController = FlappyBirdEmulatorController.Instance;
        flappyBirdEmulatorController.StartEmulator();
        flappyBirdEmulatorController.StartGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        flappyBirdEmulatorController.OnUpdate();
    }
    private void LateUpdate()
    {
        flappyBirdEmulatorController.OnLateUpdate();
    }

#if (DEBUG && !PERFORMANCE) && !UI_DEVELOP_MODE
    private void OnApplicationQuit()
    {
        flappyBirdEmulatorController.EndGame();
        flappyBirdEmulatorController.DisposeMatch();
    }
#endif
}
