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
    public Terminal terminal;
    public bool roomCompleted;
    public float timer = 0f;
    public GameObject puzzleUI;
    public Transform startingPosition;
    public List<PuzzleData> puzzles;
    public List<PuzzleData> randomizedPuzzles;
    public TMP_Text timerText;
    public bool questionLoaded;
    public bool answersLoaded;
    public bool puzzleStarted = false;
    public GameObject loadingScreen;
    public GameObject startTerminalText;
    public GameObject result;
    public GameObject mark;
    public int points;
   

    private void Awake()
    {
        instance = this;
        questionLoaded = false;
        startingPosition = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        room = GameObject.Find("Room").GetComponent<Room>();
        LoadPuzzlesData();

    }

    private void Update()
    {
        
        if (run == null)
        {
            Run.instance.CreateRun(run);
            run = Run.instance.GetRun();
        }

        if (run != null && room.isCompleted == false)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();
            
              
        }
      
    }

    public void OpenPuzzle()
    {
        puzzleUI.SetActive(true);
        startTerminalText.SetActive(false);
        Cursor.visible = true;
    }

    public void ClosePuzzle()
    {
        print("ssdf");
        puzzleUI.SetActive(false);
        startTerminalText.SetActive(true);
        result.SetActive(false);
        Cursor.visible = false;
        Player.instance.player.transform.position = startingPosition.position;
    }

    public void LoadPuzzlesData()
    {
        ApiHandler.instance.CallApiRequest("get", null, Request.LOADPUZZLESDATA);
    }

    public void FinishPuzzle()
    {
        if(terminal.answeredCorrect > Mathf.Round(terminal.questions.Count / 2))
        {
            terminal.progress = Terminal.ScreenProgress.FINISHED;
           
            ClosePuzzle();
            
        }
        else
        {
            terminal.progress = Terminal.ScreenProgress.FAILED;
            ClosePuzzle();
        }

        points = 0;
        
    }


    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
}

