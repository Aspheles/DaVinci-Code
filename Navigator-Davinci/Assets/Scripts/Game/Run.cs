using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Run : MonoBehaviour
{

    public static Run instance;

    public string id;
    public string difficulty;
    public UserInfo player;
    public float time;
    public bool isCompleted;

    private void Awake()
    {
        instance = this;
    }

    public void CreateRun(Run run)
    {
        if(run == null)
        {
            id = "0";
            difficulty = UserInfo.instance.selectedDifficulty;
            player = UserInfo.instance;
            time = 0f;
            isCompleted = false;
        }
        else
        {
            instance = run;
        }
       
    }


    public Run GetRun()
    {
        return instance;
    }
}
