﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using PlayFab.Json;

public class PuzzleOverview : MonoBehaviour
{ 
    [SerializeField] private Transform container;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private GameObject cover;
    public static PuzzleOverview instance;
    private List<PuzzleData> puzzles = new List<PuzzleData> { };
    private void Start()
    {
        instance = this;
        cover.SetActive(false);
        StartCoroutine(FetchPuzzles());
    }

    /// <summary>
    /// Loads all the puzzles.
    /// </summary>
    public void LoadPuzzles()
    {
        if(puzzles.Count > 0 && puzzles != null)
        {
            foreach(PuzzleData puzzle in puzzles)
            {
                GameObject puzzleCopyObject = Instantiate(puzzleObject, container.position, Quaternion.identity);
                puzzleCopyObject.transform.SetParent(container);
                puzzleCopyObject.GetComponent<Puzzle>().id = puzzle.id;
                puzzleCopyObject.GetComponent<Puzzle>().name.text = puzzle.name;
                puzzleCopyObject.GetComponent<Puzzle>().description = puzzle.description;
                puzzleCopyObject.GetComponent<Puzzle>().difficulty = puzzle.difficulty;
                puzzleCopyObject.GetComponent<Puzzle>().creator = puzzle.creator;
            }
        }
    }
    public void Open()
    {
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    public void New()
    {
        Launcher.instance.OpenPuzzleCreatorMenu();
    }

    /// <summary>
    /// Deletes chosen puzzle.
    /// </summary>
    /// <param name="puzzle"></param>
    public void Delete(Puzzle puzzle)
    {
        Destroy(puzzle.puzzleObject);
    }

    /// <summary>
    /// Opens the info modal for the chosen puzzle.
    /// </summary>
    /// <param name="puzzleinfo"></param>
    public void OpenInfo(Puzzle puzzleinfo)
    {
        if(cover.activeSelf != true)
        {
            cover.SetActive(true);
            cover.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = puzzleinfo.name.text;
            cover.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Difficulty: " + puzzleinfo.difficulty;
            cover.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Created by: " + puzzleinfo.creator;
            cover.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = puzzleinfo.description;
        }
        else if(cover.activeSelf != false)
        {
            cover.SetActive(false);
        }
        
    }

    /// <summary>
    /// Fetches all the puzzles from the database.
    /// </summary>
    /// <returns></returns>

    public IEnumerator FetchPuzzles()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("creator", "CodeerBeer")
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/fetchpuzzles.php", form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        else
        {
            byte[] puzzleInfo = www.downloadHandler.data;
            string result = System.Text.Encoding.Default.GetString(puzzleInfo);

            JSONArray jsonData = JSON.Parse(result) as JSONArray;

            for(int i = 0; i < jsonData.Count; i++)
            {
                PuzzleData newPuzzle = new PuzzleData(int.Parse(jsonData[i][0]), jsonData[i][1], jsonData[i][3], jsonData[i][2], jsonData[i][4]);
                puzzles.Add(newPuzzle);
            }
            
            if(puzzles.Count > 0)
            {
                LoadPuzzles();
            }
        }
    }
}