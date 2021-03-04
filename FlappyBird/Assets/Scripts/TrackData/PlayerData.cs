using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
   /*
    #region Singleton for PlayerData
    private static PlayerData _instance = null;

    public static PlayerData Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            PlayerData[] playerDatas = FindObjectsOfType(typeof(PlayerData)) as PlayerData[];
            if (playerDatas.Length == 0)
            {
                Debug.Log("PlayerData not present on the scene. Creating a new one.");
                PlayerData manager = new GameObject("PlayerData").AddComponent<PlayerData>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return playerDatas[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.Log("You can only use one PlayerDataManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    #endregion
   */
    public bool FirstTime = false;

    public PlayerInfoKeep keeper;

    private void Awake()
    {
        PlayerData[] playerDatas = FindObjectsOfType(typeof(PlayerData)) as PlayerData[];
        if (playerDatas.Length == 1)
        {
            //First Time Mask and Input PlayerID
            FirstTime = true;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            for(int i = 1; i < playerDatas.Length; i++)
            {
                if (playerDatas[i])
                    Destroy(playerDatas[i].gameObject);
            }
        }
        if (!FirstTime)
        {
            GameObject.Find("PlayerInfoPanel").SetActive(false);
            GameManager.Instance.SwitchState("Menu");
        }
        else
        {
            keeper.InitializeData();
        }
        GameManager._offMenuEvent_O += BeginARound;
    }

    public int Rounds = 0;

    public void BeginARound()
    {
        Rounds++;
        keeper.Rounds = Rounds;
    }

    public int Dead_Pipes; // = game_score + 1;

    public bool dead_windison = false; // game_score inside the range 3-5, etc, etc

    public float timecomsume_inround = 0; //Timer on begin end dead;

    public int passed_wind_count = 0; //every time the windoff count ++

    public int mouse_click_count = 0; //every click + 1;

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timecomsume_inround += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
                mouse_click_count += 1;
        } 
    }

    public void EndGameDataCollect()
    {
        // Check dead point
        Dead_Pipes = GameManager.Instance.game_Score + 1;

        // Check if dead inside a wind
        IEnumerable<int> dead1anyWindOn =
            from pipes in GameManager.Instance._windOnPoint
            where pipes < Dead_Pipes
            select pipes;

        if (dead1anyWindOn.Count() != 0)
        {
            dead_windison = (Dead_Pipes <= GameManager.Instance._windOffPoint[dead1anyWindOn.Count() - 1]);
        }
        else
        {
            dead_windison = false;
        }

        //check how many winds passed
        passed_wind_count = dead1anyWindOn.Count();

        //Time comsume in a round
        //timecomsume_inround;

        //Mouse Click in a Round
        //mouse_click_count;
        keeper.AddData(Dead_Pipes, dead_windison, timecomsume_inround, passed_wind_count, mouse_click_count);
    }
}
