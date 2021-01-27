using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzleObject;
    public int id;
    public TextMeshProUGUI name;
    public string difficulty;
    public string creator;
    public string description;

    public void Edit()
    {
        PuzzleData Puzzle = PuzzleOverview.instance.puzzles.Find((x) => x.name == name.text);
        //Debug.Log(Question.question);
        if (Puzzle != null)
        {
            QuestionSession.instance.puzzle = Puzzle;
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
        StartCoroutine(PuzzleOverview.instance.Delete(this));
    }


    public void OnInfoButtonClicked()
    {
        PuzzleOverview.instance.OpenInfo(this);
    }
}
