using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleOverview : MonoBehaviour
{ 
    [SerializeField] private Transform container;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private GameObject cover;
    public static PuzzleOverview instance;
    private List<PuzzleData> puzzles = new List<PuzzleData> { new PuzzleData(0, "Puzzle 1", null, "easy", "Khizer", "this is the description"), new PuzzleData(1, "Puzzle 2", null, "medium", "Yavuz", "this is the description"), new PuzzleData(2, "Puzzle 3", null, "hard", "Sander", "this is the description"), new PuzzleData(3, "Puzzle 1", null, "easy", "Khizer", "this is the description"), new PuzzleData(4, "Puzzle 2", null, "medium", "Yavuz", "this is the description"), new PuzzleData(5, "Puzzle 3", null, "hard", "Sander", "this is the description") };
   
    private void Start()
    {
        instance = this;
        cover.SetActive(false);
        LoadPuzzles();
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

}
