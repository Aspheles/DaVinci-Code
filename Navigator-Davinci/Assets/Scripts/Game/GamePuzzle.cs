using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePuzzle : MonoBehaviour
{
    public static GamePuzzle instance;
    public GameObject menuScreen;
    public GameObject gameScreen;

    private void Start()
    {
        instance = this;
    }

    public void StartPuzzle()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        RunManager.instance.puzzleStarted = true;
        


    }

    public void LeavePuzzle()
    {
        RunManager.instance.ClosePuzzle();
    }
}
