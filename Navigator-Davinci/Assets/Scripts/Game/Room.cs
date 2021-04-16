using System.Collections;
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
    public int roomNumber = 0;

    public List<Terminal> terminals;
    public bool terminalsLoaded = false;

    public static Room instance;
    public TMP_Text roomText;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }

        instance = this;
        difficulty = UserInfo.instance.selectedDifficulty;
    }

    private void Update()
    {
        roomText.text = "Room: " + roomNumber.ToString();
        LoadTerminals();
        CheckLevelCleared();
    }

    public void LoadTerminals()
    {
        if(TerminalSpawnPoints.instance.difficultyList.Count > 0 && !terminalsLoaded)
        {
            for (int i = 0; i < terminals.Count; i++)
            {
                terminals[i].LoadPuzzle(TerminalSpawnPoints.instance.difficultyList[i]);
            }
            terminalsLoaded = true;
        }
        
    }

    public void CheckLevelCleared()
    {
        int count = 0;
        foreach(Terminal terminal in terminals)
        {
            if (terminal.finished) count++;
        }

        if (terminals.Count == count) isCompleted = true;
        else isCompleted = false;
       
    }
}
