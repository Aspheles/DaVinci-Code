﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    public string id;
    public string runId;
    public string difficulty;
    public Time roomTime;
    public bool isCompleted;
    public int roomNumber = 1;

    public List<Terminal> terminals;
    public bool terminalsLoaded = false;

    public static Room instance;
    public TMP_Text roomText;
    public TMP_Text roomDifficulty;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }

        instance = this;
        difficulty = UserInfo.instance.selectedDifficulty;
        roomNumber = 1;
    }

    private void Update()
    {
        roomText.text = "Room: " + roomNumber.ToString();
        roomDifficulty.text = "Difficulty: " + difficulty;

        if(!terminalsLoaded)
        {
            TerminalSpawnPoints.instance.GetDifficulty();
            terminalsLoaded = true;
        }
        CheckLevelCleared();
    }

    public void CheckLevelCleared()
    {
        int count = 0;
        foreach(Terminal terminal in terminals)
        {
            if (terminal.finished) count++;
        }

        if (count == 6) RunManager.instance.roomCompleted = true;
        else RunManager.instance.roomCompleted = false;

    }
}
