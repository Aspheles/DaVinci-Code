using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleOverview : MonoBehaviour
{ 
    public Transform container;
    public GameObject puzzleObject;
    public GameObject cover;
    public static PuzzleOverview instance;
    private List<Puzzle> puzzles = new List<Puzzle> { new Puzzle(0, "Puzzle 1", null, "easy", "Khizer", "this is the description"), new Puzzle(1, "Puzzle 2", null, "medium", "Yavuz", "this is the description"), new Puzzle(3, "Puzzle 3", null, "hard", "Sander", "this is the description"), new Puzzle(0, "Puzzle 1", null, "easy", "Khizer", "this is the description"), new Puzzle(1, "Puzzle 2", null, "medium", "Yavuz", "this is the description"), new Puzzle(3, "Puzzle 3", null, "hard", "Sander", "this is the description") };
   
    private void Start()
    {
        instance = this;
        cover.SetActive(false);
        LoadPuzzles();
    }

    public void LoadPuzzles()
    {
        if(puzzles.Count > 0 && puzzles != null)
        {
            foreach(Puzzle puzzle in puzzles)
            {
                GameObject puzzleCopyObject = Instantiate(puzzleObject, container.position, Quaternion.identity);
                puzzleCopyObject.transform.SetParent(container);
                puzzleCopyObject.GetComponent<PuzzleData>().name.text = puzzle.name;
                puzzleCopyObject.GetComponent<PuzzleData>().description = puzzle.description;
                puzzleCopyObject.GetComponent<PuzzleData>().difficulty = puzzle.difficulty;
                puzzleCopyObject.GetComponent<PuzzleData>().creator = puzzle.creator;
            }
        }
    }
    public void Open()
    {
        print("open");
    }

    public void Delete(GameObject puzzle)
    {
        //puzzles.Remove();
        Destroy(puzzle);
    }

    public void OpenInfo(PuzzleData puzzleinfo)
    {
        if(cover.activeSelf != true)
        {
            cover.SetActive(true);
            cover.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = puzzleinfo.name.text;
            cover.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Difficulty: " + puzzleinfo.difficulty;
            cover.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Created by: " + puzzleinfo.creator;
            cover.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = puzzleinfo.description;
        }
        else if(cover.activeSelf != false)
        {
            cover.SetActive(false);
        }
        
    }

}
