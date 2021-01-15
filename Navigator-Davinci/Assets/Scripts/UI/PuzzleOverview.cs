using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleOverview : MonoBehaviour
{ 
    public Transform container;
    public GameObject puzzleObject;
    public static PuzzleOverview instance;
    private List<Puzzle> puzzles = new List<Puzzle> {new Puzzle(0, "Puzzle 1", null, "easy"), new Puzzle(1, "Puzzle 2", null, "medium"), new Puzzle(3, "Puzzle 3", null, "hard"), new Puzzle(0, "Puzzle 1", null, "easy"), new Puzzle(1, "Puzzle 2", null, "medium"), new Puzzle(3, "Puzzle 3", null, "hard"), new Puzzle(0, "Puzzle 1", null, "easy"), new Puzzle(1, "Puzzle 2", null, "medium"), new Puzzle(3, "Puzzle 3", null, "hard") };

    private void Start()
    {
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
            }
        }
    }
    public void Open()
    {
        print("open");
    }

    public void Delete()
    {
        print("delete");
    }

}
