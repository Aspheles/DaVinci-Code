using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzleObject;
    public int id;
    public TMP_Text name;
    public TMP_Text difficulty;
    //public string difficulty;
    public string creator;
    public string description;
    public static Puzzle instance;


    private void Awake()
    {
        instance = this;
        
    }

    public void Edit()
    {
        PuzzleData Puzzle = PuzzleOverview.instance.puzzles.Find((x) => x.name == name.text);

        if (Puzzle != null)
        {
            Session.instance.puzzle = Puzzle;
            Launcher.instance.OpenPuzzleQuestionsOverviewMenu();


        }
        else
        {
            //QuestionCreator.instance.ResetData();
            Debug.Log("No questions in this puzzle found");
            Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
        }
    }

    public void Delete()
    {
        //StartCoroutine(PuzzleOverview.instance.Delete(this));
        PuzzleOverview.instance.selectedPuzzle = this;
        Puzzles puzzle = new Puzzles();
        puzzle.Delete(id);
    }


    public void OnInfoButtonClicked()
    {
        PuzzleOverview.instance.selectedPuzzle = this;
        PuzzleOverview.instance.OpenInfo(PuzzleOverview.instance.selectedPuzzle);
    }

    public void OnInfoEditButtonClicked()
    {

        Puzzle data = PuzzleOverview.instance.selectedPuzzle;
        PuzzleOverview.instance.OpenEditInfo(data);
    }
}
