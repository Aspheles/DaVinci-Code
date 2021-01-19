using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzleObject;
    public int id;
    public TextMeshProUGUI name;
    List<Question> questions;
    public string difficulty;
    public string creator;
    public string description;

    public void Edit()
    {

    }

    public void Delete()
    {
        PuzzleOverview.instance.Delete(this);
    }


    public void OnInfoButtonClicked()
    {
        PuzzleOverview.instance.OpenInfo(this);
    }
}
