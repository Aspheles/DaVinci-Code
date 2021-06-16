using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public int retries = 0;
    public bool gameOver = false;
    public int maxRoomNumber = 6;
    public int CompletedTerminalsAmount;
    public List<CompletedPuzzle> completedPuzzles;
    public List<PuzzleData> completed;
    public Result result;
    public int totalPoints;

    public bool loadedUpgrades = false;


    private void Awake()
    {
        instance = this;
        questionLoaded = false;
        maxHealth = 4;
        UserInfo.instance.playerManager.ApplyUpgrades();
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

        if (CompletedTerminalsAmount >= 4)
        {
            SendMoneyToDB();
            FinishRoom();
            if (currentHealth < maxHealth) currentHealth++;
            
            
            
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            CompletedTerminalsAmount = 4;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth = maxHealth;
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Launcher.instance.OpenGameOverMenu();
            Player.instance.canwalk = false;
            gameOver = true;
        }

    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public int CalculatePoints()
    {
        return totalPoints *10;
    }
    
    public void NextRoom()
    {

        
        Debug.Log(CalculatePoints().ToString());

        if(room.difficulty == "Easy" && room.roomNumber > 6)
        {
            room.difficulty = "Medium";
            room.roomNumber = 1;
        }else if(room.difficulty == "Medium" && room.roomNumber > 6)
        {
            room.difficulty = "Hard";
            room.roomNumber = 1;
        }

        room.terminals.Clear();

        GetFinishedPuzzles();
        
        //Reset Values
        CompletedTerminalsAmount = 0;

        //LoadPuzzlesData();

        TerminalSpawnPoints.instance.difficultyList.Clear();
      
        //Remove interaction text
        GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>().text = "";

        
    }

    public void OpenPuzzle()
    {
        puzzleUI.SetActive(true);
        Cursor.visible = true;
    }

    public void ClosePuzzle()
    {
        puzzleUI.SetActive(false);
        
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



        totalPoints = totalPoints + points;
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


   public void FilterPuzzles()
    {
        RunManager.instance.completed = new List<PuzzleData>();

        foreach (PuzzleData puzzle in RunManager.instance.puzzles)
        {
            for (int i = 0; i < RunManager.instance.completedPuzzles.Count; i++)
            {
                if (puzzle.id == RunManager.instance.completedPuzzles[i].puzzleid)
                {
                    RunManager.instance.completed.Add(puzzle);
                }
            }
        }

        if (completed.Count > 0)
        { 
            for (int i = 0; i < completed.Count; i++)
            {
                if (puzzles.Contains(completed[i]))
                {
                    puzzles.Remove(completed[i]);
                }
            }

            Debug.Log("Finished filtering puzzles");
        }
       



    }


    //Checking if user can go to next room and updating values
    public void FinishRoom()
    {
        if (room.roomNumber <= maxRoomNumber)
        {
            room.roomNumber++;

          
            foreach(PuzzleData puzzle in randomizedPuzzles)
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

    public void SendMoneyToDB()
    {
        Debug.Log("Sending money to db");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", UserInfo.instance.id.ToString()),
            new MultipartFormDataSection("money", CalculatePoints().ToString())
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.SENDMONEYTODB);
    }

    public void SendPurchaseRequest()
    {
        Debug.Log("Requesting to buy upgrade ");
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

