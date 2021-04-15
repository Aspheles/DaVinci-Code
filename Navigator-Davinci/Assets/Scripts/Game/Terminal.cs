using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class Terminal : MonoBehaviour
{
    public Material mat;
    public GameObject terminal;
    public GameObject screen;
    public Material terminalScreenmat;
    public ScreenProgress progress;
    public Material terminalmat;
    public PuzzleData puzzle;

    public List<Question> questions;
    public List<Answer> answers;
    public int questionNumber = 0;
    public int answeredCorrect = 0;

    public bool answeredQuestion = false;
    public bool puzzleLoaded = false;
    public bool questionsLoaded = false;
    public bool finished = false;

    private void Start()
    {
        terminalScreenmat = screen.GetComponent<MeshRenderer>().material;
    }

    public enum ScreenProgress
    {
        READY,
        BLOCKED,
        PROGRESS,
        FINISHED,
        FAILED
    }

    //Displays terminal screen depending on the progess
    void CheckTerminalProgress(ScreenProgress progress)
    {
        switch (progress)
        {
            case ScreenProgress.READY:
                terminalScreenmat.color = new Color32(77, 121, 255, 255);
                break;
            case ScreenProgress.BLOCKED:
                terminalScreenmat.color = Color.black;
                break;
            case ScreenProgress.PROGRESS:
                terminalScreenmat.color = Color.yellow;
                break;
            case ScreenProgress.FINISHED:
                terminalScreenmat.color = Color.green;
                break;
            case ScreenProgress.FAILED:
                terminalScreenmat.color = Color.red;
                break;
        }
    }


    private void Update()
    {
        mat.color = Color.red;
        CheckTerminalProgress(progress);

        if(GameManager.instance != null)
        {
            if(questions.Count > 0)
            {
                //Debug.Log("Questions Count: " + questions.Count);
                GameManager.instance.GetQuestion(questions[questionNumber]);
            }
        }

        if(progress == ScreenProgress.FINISHED || progress == ScreenProgress.FAILED)
        {
            finished = true;
        }
        else
        {
            finished = false;
        }
    }

    //Selects a puzzle from the loaded puzzles
    public void LoadPuzzle(string difficulty)
    {
        print("Puzzles loaded Count: " + RunManager.instance.puzzles.Count);

        /*
        Debug.Log(difficulty);
        
        if(RunManager.instance.puzzles[random].difficulty == difficulty)
        {
            puzzle = RunManager.instance.puzzles[random];
            puzzleLoaded = true;
        }*/

        List<PuzzleData> randomizedPuzzles = new List<PuzzleData>();
        foreach(PuzzleData puzzle in RunManager.instance.puzzles)
        {
            if (puzzle.difficulty == difficulty) randomizedPuzzles.Add(puzzle);
        }
        int random = Random.Range(0, randomizedPuzzles.Count);
        puzzle = Shuffle<PuzzleData>(randomizedPuzzles)[random];
      
           
            
            //Check if puzzle gets loaded in EXE
            
        
    }

    //Makes a api request to receive question from the current puzzle loaded in terminal
    public void GetQuestions()
    {
        if (ApiHandler.instance != null)
        {
            if (puzzle != null)
            {
                RunManager.instance.loadingScreen.SetActive(true);
                List<IMultipartFormSection> form = new List<IMultipartFormSection>
                {
                   new MultipartFormDataSection("puzzleid", puzzle.id.ToString())
                };

                ApiHandler.instance.CallApiRequest("post", form, Request.LOADPUZZLEQUESTIONS);
            }
        }
        
    }

    public void GetAnswers()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", questions[questionNumber].id.ToString())
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.LOADGAMEQUESTIONANSWERS);
    }

    public void ResetTerminal()
    {
        finished = false;
        TerminalSpawnPoints.instance.terminalsLoaded = false;
        progress = ScreenProgress.READY;
        questions = new List<Question>();
        answers = new List<Answer>();
        puzzle = null;
        answeredCorrect = 0;
        questionNumber = 0;

       
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
