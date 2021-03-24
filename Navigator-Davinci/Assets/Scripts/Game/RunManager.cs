using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    public Run run;
    public Room room;
    public bool roomCompleted;
    public float timer = 0f;

    public GameObject puzzleUI;
    public Transform startingPosition;
    public List<PuzzleData> puzzles;
    public List<Question> questions;
    public TMP_Text timerText;




    private void Awake()
    {
        instance = this;
        startingPosition = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        puzzles = new List<PuzzleData>();
        LoadPuzzlesData();

    }

    private void Update()
    {
        if (room == null)
        {
            room = GameObject.Find("Room").GetComponent<Room>();
        }

        if (run == null)
        {
            Run.instance.CreateRun(run);
            run = Run.instance.GetRun();
        }

        if (run != null && run.isCompleted == false)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();
        }
    }

    public void OpenPuzzle()
    {
        puzzleUI.SetActive(true);
        Cursor.visible = true;
    }

    public void ClosePuzzle()
    {
        puzzleUI.SetActive(false);
        Cursor.visible = false;
        Player.instance.player.transform.position = startingPosition.position;
    }

    public void LoadPuzzlesData()
    {
        ApiHandler.instance.CallApiRequest("get", null, Request.LOADPUZZLESDATA);
    }

    public void SetPuzzleQuestions(Terminal terminal)
    {
        terminal.questions = questions;
    }
}

