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

    public AudioSource WindSounds;

    public GameObject StillWind;

    UnityAction WindOnAction;
    UnityAction WindOffAction;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(0.5f);
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
            UduinoManager.Instance.ReadingMethod = HardwareReading.Coroutines;
            UduinoManager.Instance.pinMode(11, PinMode.Output);
            Debug.Log("OutPut Set Up");
        }
        else
        {
            GameManager.Instance.ArduinoSetOnNoDevice();
            Debug.Log("No PinMode Setup");
        }

        WindOnAction += delegate {
            ArduinoFanControl(true);
            WindOn();
        };
        WindOffAction += delegate {
            ArduinoFanControl(false);
            WindOff();
        };
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
                    //warning tag

                    //GameManager.Instance.SwitchInGameState("Normal");
                }
                else
                {
                    //cancel warning tag

                   //GameManager.Instance.SwitchInGameState("WindOn");
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
                UduinoManager.Instance.digitalWrite(11, State.HIGH);
                StillWind.SetActive(!windOn);
            }
            else
            {
                UduinoManager.Instance.digitalWrite(11, State.LOW);
                StillWind.SetActive(!windOn);
            }
        }
        else
        {
            if (windOn)
            {
                StillWind.SetActive(!windOn);
            }
            else
            {
                StillWind.SetActive(!windOn);
            }
        }
    }

    private void WindOn() =>
        //happens when winds on
        WindOnSounds();

    private void WindOff()
    {
        //happens when winds on
        WindOffSounds();
        WindOffSign();
    }

    private void WindOnSounds() => WindSounds.Play();

    private void WindOffSounds() => WindSounds.Stop();

    private void WindOnSign() => GameManager.Instance.WarningSign.SetActive(true);

    private void WindOffSign() => GameManager.Instance.WarningSign.SetActive(false);
}
