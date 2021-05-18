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
    public int points;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public bool gameOver = false;


    private void Awake()
    {
        instance = this;
        questionLoaded = false;
        maxHealth = 4;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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

        if (run != null && room.isCompleted == false && gameOver == false)
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

    public void FinishPuzzle()
    {
        if(terminal.answeredCorrect > Mathf.Round(terminal.questions.Count / 2))
        {
            terminal.progress = Terminal.ScreenProgress.FINISHED;
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

        healthBar.SetHealth(currentHealth);

        if(currentHealth == 0)
        {
            Launcher.instance.OpenGameOverMenu();
            Player.instance.canwalk = false;
            gameOver = true;
        }
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

