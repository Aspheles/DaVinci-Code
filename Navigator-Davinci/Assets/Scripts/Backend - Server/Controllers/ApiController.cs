using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ApiController : MonoBehaviour
{
    public static ApiController instance;

    private void Awake()
    {
        instance = this;
    }



    public void CheckData(JSONNode Data, string type)
    {
        switch (type)
        {
            case Request.CREATEPUZZLE:
                SavePuzzle(Data);
                break;
            case Request.EDITPUZZLE:
                EditPuzzle();
                break;
            case Request.DELETEPUZZLE:
                DeletePuzzle();
                break;
            case Request.FETCHPUZZLES:
                FetchPuzzles(Data);
                break;

        }
    }

    public void CheckError(JSONNode Data, string type)
    {
        switch (type)
        {
            case Request.CREATEPUZZLE:
                SavePuzzle(Data);
                break;
            case Request.EDITPUZZLE:
                EditPuzzle();
                break;
            case Request.DELETEPUZZLE:
                DeletePuzzle();
                break;
            case Request.FETCHPUZZLES:
                FetchPuzzles(Data);
                break;

        }
    }

    private void SavePuzzle(JSONNode Data)
    {
        for (int i = 0; i < Data.Count; i++)
        {
            Session.instance.puzzle.id = Data[i].AsObject["id"];
            Session.instance.puzzle.name = Data[i].AsObject["name"];
            Session.instance.puzzle.difficulty = Data[i].AsObject["difficulty"];
            Session.instance.puzzle.description = Data[i].AsObject["description"];
            Session.instance.puzzle.creator = Data[i].AsObject["creator"];
        }
        print("Puzzle has been saved");
        Launcher.instance.OpenPuzzleQuestionCreatorMenu();
    }

    private void EditPuzzle()
    {

        PuzzleOverview.instance.cover.SetActive(false);
        Launcher.instance.OpenAdminPuzzleOverviewMenu();

        if (PuzzleOverview.instance.puzzles.Count > 0)
        {
            PuzzleOverview.instance.LoadPuzzles();
        }

        print("Puzzle has been edited");
    }

    /// <summary>
    /// Deletes chosen puzzle.
    /// </summary>
    /// <param name="puzzle"></param>
    private void DeletePuzzle()
    {
        Destroy(PuzzleOverview.instance.selectedPuzzle.puzzleObject);
    }

    private void FetchPuzzles(JSONNode Data)
    {
        //PuzzleOverview.instance.puzzles = new List<PuzzleData>();

        for (int i = 0; i < Data.Count; i++)
        {
            
            int puzzleId = int.Parse(Data[i].AsObject["id"]);
            string puzzleName = Data[i].AsObject["name"];
            string puzzleDifficulty = Data[i].AsObject["difficulty"];
            string puzzleDescription = Data[i].AsObject["description"];
            string puzzleCreator = Data[i].AsObject["creator"];

            PuzzleData puzzle = new PuzzleData(puzzleId, puzzleName, puzzleDifficulty, puzzleDescription, puzzleCreator);
            PuzzleOverview.instance.puzzles.Add(puzzle);
           
        }

        if (PuzzleOverview.instance.puzzles.Count > 0)
        {
            PuzzleOverview.instance.LoadPuzzles();
        }
        
    }
}
