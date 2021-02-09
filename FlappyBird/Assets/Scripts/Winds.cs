using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.Events;

public class Winds : MonoBehaviour
{
    public bool toggle = false;

    public RoleControl RoleControl;

    private bool HardwareisConnected = false;

    UnityAction WindOnAction;
    UnityAction WindOffAction;
    // Start is called before the first frame update
    void Start()
    {
        if (UduinoManager.Instance.isConnected())
        {
            HardwareisConnected = true;
            Debug.Log("Something Connected");
        }
        else
        {
            Debug.Log("NoBoard Connected");
        }

        if (HardwareisConnected)
        {
            UduinoManager.Instance.pinMode(9, PinMode.Output);
            Debug.Log("OutPut Set Up");
        }
        else
        {
            GameManager.Instance.ArduinoSetOnNoDevice();
            Debug.Log("No PinMode Setup");
        }

        WindOnAction += delegate { ArduinoFanControl(true); };
        WindOffAction += delegate { ArduinoFanControl(false); };
        GameManager.Instance.onPlayingNormalEvent.AddListener(WindOffAction);
        GameManager.Instance.onPlayingWindOnEvent.AddListener(WindOnAction);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (UduinoManager.Instance.isConnected())
        {
            HardwareisConnected = true;
        }
        else
        {
            HardwareisConnected = false;
        }

        if (GameManager.Instance.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (toggle)
                {
                    GameManager.Instance.SwitchInGameState("Normal");
                }
                else
                {
                    GameManager.Instance.SwitchInGameState("WindOn");
                }
                toggle = !toggle;
            }
        }
    }

    private void ArduinoFanControl(bool windOn)
    {
        if (HardwareisConnected)
        {
            if (windOn)
            {
                UduinoManager.Instance.digitalWrite(9, State.HIGH);
            }
            else
            {
                UduinoManager.Instance.digitalWrite(9, State.LOW);
            }
        }
    }
}
