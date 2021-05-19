using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int points;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public bool gameOver = false;
    public int maxRoomNumber = 6;
    public int CompletedTerminalsAmount;
    public List<CompletedPuzzle> completedPuzzles;

    private void Awake()
    {
        instance = this;
        questionLoaded = false;
        maxHealth = 4;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        CompletedTerminalsAmount = 0;
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

        if (run != null && run.isCompleted == false && gameOver == false)
        {
            healthBar.SetHealth(currentHealth);
            timer += Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();
        }

        //Mathf.Round(TerminalSpawnPoints.instance.spawnpoints.Count / 2) +1 && room.isCompleted == true

        if (CompletedTerminalsAmount >= 1)
        {
            FinishRoom();
            if (currentHealth < maxHealth) currentHealth++;
            NextRoom();
            
            
        }

    }
    

    public void NextRoom()
    {
        GetFinishedPuzzles();

        room.terminals.Clear();

        //Reset Values
        CompletedTerminalsAmount = 0;

        LoadPuzzlesData();

        TerminalSpawnPoints.instance.difficultyList.Clear();
      
        //Remove interaction text
        GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>().text = "";

        //Reset terminals
        TerminalSpawnPoints.instance.ResetTerminals();
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

    public void FinishPuzzle()
    {
        if (terminal.answeredCorrect > Mathf.Round(terminal.questions.Count / 2))
        {
            terminal.progress = Terminal.ScreenProgress.FINISHED;
            CompletedTerminalsAmount++;
        }
        else
        {
            terminal.progress = Terminal.ScreenProgress.FAILED;
            TakeDamage(1);
        }

        print(currentHealth);
        points = 0;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

       
        if (currentHealth == 0)
        {
            Launcher.instance.OpenGameOverMenu();
            Player.instance.canwalk = false;
            gameOver = true;
        }
    }


    //Creating Run in the database


    //Checking if user can go to next room and updating values
    public void FinishRoom()
    {
        if (room.roomNumber <= maxRoomNumber)
        {
            room.roomNumber++;

            //Getting the puzzles
            List<PuzzleData> puzzles = randomizedPuzzles;

            foreach(PuzzleData puzzle in puzzles)
            {
                List<IMultipartFormSection> form = new List<IMultipartFormSection>
                {
                    new MultipartFormDataSection("runid", room.id.ToString()),
                    new MultipartFormDataSection("puzzleid", puzzle.id.ToString()),
                    new MultipartFormDataSection("accountid", UserInfo.instance.id.ToString())

                };

                ApiHandler.instance.CallApiRequest("post", form, Request.FINISHROOM);
            }

            CompletedTerminalsAmount = 0;
        }
        else
        {
            //Completed all the rooms
        }

    }

    public void GetFinishedPuzzles()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", UserInfo.instance.id.ToString())
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.GETFINISHEDPUZZLES);
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

