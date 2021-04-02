using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Unity;

//download the .dll from https://github.com/JamesNK/Newtonsoft.Json/releases and add it to the references.
//better to use JsonConvert.SerializeObject instead of JsonUtility when nested jsons are being made 
//you can also just use Jsonutility that comes with unity if you have a simple json
using Newtonsoft.Json;

public class JsonConverter : MonoBehaviour
{
    public List<DataJsonFormat> jsonObj_ = new List<DataJsonFormat>();
    public PlayerInfoKeep keeper;
    public int count_obj = 0;

    private void Start()
    {
    }
    public void collect()
    {
        jsonObj_.Add(new DataJsonFormat());
        Debug.Log(count_obj);
        /*
        add things here
        */
        if(count_obj == 0)
        {   
            jsonObj_[count_obj].timecomsume_inround = keeper.timecomsume_inround[count_obj];
            jsonObj_[count_obj].mouse_click_count = keeper.mouse_click_count[count_obj];
        }
        else if(count_obj >= 1)
        {
            jsonObj_[count_obj].timecomsume_inround = keeper.timecomsume_inround[count_obj] - keeper.timecomsume_inround[count_obj - 1];
            jsonObj_[count_obj].mouse_click_count = keeper.mouse_click_count[count_obj] - keeper.mouse_click_count[count_obj - 1];

        }
        jsonObj_[count_obj].PlayerID = keeper.PlayerID;
        jsonObj_[count_obj].Rounds = keeper.Rounds;
        jsonObj_[count_obj].Dead_Pipe = keeper.Dead_Pipes[count_obj];
        jsonObj_[count_obj].dead_windison = keeper.dead_windison[count_obj];
        jsonObj_[count_obj].passed_wind_count = keeper.passed_wind_count[count_obj]; 
        jsonObj_[count_obj].Total_time_comsume = keeper.timecomsume_inround[count_obj];
        count_obj++;
        //string json = JsonUtility.ToJson(jsonObj_);

        string json = JsonConvert.SerializeObject(jsonObj_);
        writeout(json);
    }

    void writeout(string json_)
    {
        string output_file = Application.streamingAssetsPath + "/Data/JsonOutput" + "/" + System.DateTime.Now.ToString("yyyy-MM-dd") + $"_{keeper.PlayerID}" + ".json";
        File.WriteAllText(output_file, json_);
    }
}
