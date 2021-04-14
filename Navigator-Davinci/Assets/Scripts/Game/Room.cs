using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string id;
    public string runId;
    public string difficulty;
    public Time roomTime;
    public bool isCompleted;

    public List<Terminal> terminals;

    public static Room instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    private void Update()
    {
        CheckLevelCleared();
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
