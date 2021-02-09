using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class PWMAnalog : MonoBehaviour
{
    [Range(0,255)]
    public int intensity = 0;
    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(11, PinMode.PWM);
    }

    // Update is called once per frame
    void Update()
    {
        UduinoManager.Instance.analogWrite(11, intensity);
    }
}
