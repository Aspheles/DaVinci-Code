using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FormController : MonoBehaviour
{
   
}

public enum PuzzleDifficulty
{
    easy,
    medium,
    hard
}

[Serializable]
public class PuzzleData
{
    public string errorMessage;

    public static PuzzleData instance;

    public int id;
    public string name;
    public string difficulty;
    public string description;
    public string creator;


    public PuzzleData(int id ,string name, string difficulty, string description, string creator)
    {
        this.id = id;
        this.name = name;
        this.difficulty = difficulty;
        this.description = description;
        this.creator = creator;
        
    }

}


[Serializable]
public class Question
{
    public int id;
    public string question;
    public string description;
    public Texture2D image;
    public List<Answer> answers;
    public int puzzleid;
    public string errorMessage;



    public Question(int id,string question, string description, Texture2D image ,List<Answer> answers, int puzzleid)
    {
        this.id = id;
        this.question = question;
        this.description = description;
        this.image = image;
        this.answers = answers;
        this.puzzleid = puzzleid;
    }
} 


[Serializable]
public class Answer
{
    public int id;
    public string answer;
    public bool isCorrect;
    public string errorMessage;

    public Answer(int id, string answer, bool isCorrect)
    {
        this.id = id;
        this.answer = answer;
        this.isCorrect = isCorrect;
    }

}

