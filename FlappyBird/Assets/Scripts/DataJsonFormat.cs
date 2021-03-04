using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class DataJsonFormat
{
    public string PlayerID = "Test";
    public int Rounds = -1;
    public int Dead_Pipe = -1;
    public bool dead_windison;
    public float timecomsume_inround = -1.0f;
    public int passed_wind_count = -1;
    public int mouse_click_count = -1;
    public float Total_time_comsume = -1;

    public DataJsonFormat()
    {
        PlayerID = "Test";
        Rounds = -1;
        Dead_Pipe = -1;
        timecomsume_inround = -1;
        passed_wind_count = -1;
        mouse_click_count = -1;
        Total_time_comsume = -1;
    }
}
