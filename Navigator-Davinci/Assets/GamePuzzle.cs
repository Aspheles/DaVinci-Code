using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePuzzle : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject gameScreen;

    public void StartPuzzle()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);


    }

    public void LeavePuzzle()
    {
        RunManager.instance.ClosePuzzle();
    }
}
