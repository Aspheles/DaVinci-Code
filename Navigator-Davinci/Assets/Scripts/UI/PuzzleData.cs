using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleData : MonoBehaviour
{
    public GameObject puzzleObject;
    int id;
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
        PuzzleOverview.instance.Delete(puzzleObject);
    }


    public void OnInfoButtonClicked()
    {
        PuzzleOverview.instance.OpenInfo(this);
    }
}
