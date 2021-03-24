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
    public int question_number = 0;

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

        if(GameManager.instance != null && progress == ScreenProgress.READY || progress == ScreenProgress.PROGRESS)
        {
            GameManager.instance.GetQuestion(questions[question_number]);
        }

    }

    public void LoadPuzzle()
    {
        if(RunManager.instance.puzzles.Count > 0)
        {
            int random = Random.Range(0, RunManager.instance.puzzles.Count);
            print(random);
            puzzle = RunManager.instance.puzzles[random];
        }
    }

    public void GetQuestions()
    {
        if(ApiHandler.instance != null)
        {
            if(puzzle != null)
            {
                List<IMultipartFormSection> form = new List<IMultipartFormSection>
                {
                   new MultipartFormDataSection("puzzleid", puzzle.id.ToString())
                };

                ApiHandler.instance.CallApiRequest("post", form, Request.LOADPUZZLEQUESTIONS);
            }
        }
    }


    public void LoadPuzzleQuestions(List<Question> _questions)
    {
        questions = _questions;
       
    }

    public void StartPuzzleGame()
    {
        //GameManager.instance.GetQuestion()
       
    }
}
