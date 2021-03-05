using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Puzzles : IManager
{
    public List<IMultipartFormSection> form;


    public void Create()
    {
        //Create Puzzle
        if (string.IsNullOrEmpty(UserInfo.instance.username)) UserInfo.instance.username = "None";
        form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("name", PuzzleManager.instance.puzzleName.text),
            new MultipartFormDataSection("description", PuzzleManager.instance.description.text),
            new MultipartFormDataSection("difficulty", PuzzleManager.instance.difficulty.options[PuzzleManager.instance.difficulty.value].text),
            new MultipartFormDataSection("creator", UserInfo.instance.username)
        };
        //Start backend request
        ApiHandler.instance.CallApiRequest("post", form, Request.CREATEPUZZLE);

    }


    public void Edit()
    {
        //Edit Puzzle information
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", PuzzleOverview.instance.selectedPuzzle.id.ToString()),
            new MultipartFormDataSection("title", PuzzleOverview.instance.puzzleEditName.text),
            new MultipartFormDataSection("description", PuzzleOverview.instance.puzzleEditDescription.text),
            new MultipartFormDataSection("difficulty", PuzzleOverview.instance.puzzleEditDifficulty.options[PuzzleOverview.instance.puzzleEditDifficulty.value].text)

        };
        ApiHandler.instance.CallApiRequest("post", form, Request.EDITPUZZLE);
    }

    public void Delete(int id)
    {
        //Delete Puzzle from list
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id.ToString())
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.DELETEPUZZLE);
    }

    /// <summary>
    /// Fetches all the puzzles from the database.
    /// </summary>
    /// <returns></returns>
    public void Load()
    {
        //Load all Puzzles into list

        ApiHandler.instance.CallApiRequest("get", null, Request.FETCHPUZZLES);
    }
}
