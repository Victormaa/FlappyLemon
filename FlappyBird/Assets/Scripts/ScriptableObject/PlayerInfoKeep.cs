using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Scriptable", order = 0)]
public class PlayerInfoKeep : ScriptableObject
{
    public string PlayerID;
    public int Rounds;

    public List<int> Dead_Pipes = new List<int>(); // = game_score + 1;

    public List<bool> dead_windison = new List<bool>(); // game_score inside the range 3-5, etc, etc

    public List<float> timecomsume_inround = new List<float>(); //Timer on begin end dead;

    public List<int> passed_wind_count = new List<int>(); //every time the windoff count ++

    public List<int> mouse_click_count = new List<int>(); //every click + 1;

    public void AddData(int a, bool b, float c, int d, int e)
    {
        Dead_Pipes.Add(a);
        dead_windison.Add(b);
        timecomsume_inround.Add(c);
        passed_wind_count.Add(d);
        mouse_click_count.Add(e);

    }

    public void InitializeData()
    {
        PlayerID = "";
        Rounds = 0;
        Dead_Pipes.Clear();
        dead_windison.Clear();
        timecomsume_inround.Clear();
        passed_wind_count.Clear();
        mouse_click_count.Clear();
    }
}
