using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;

public class FormController : MonoBehaviour
{
   
}

[Serializable]
public class CompletedPuzzle
{
    public int puzzleid;
    public int accountid;
    public int runid;

    public CompletedPuzzle(int puzzleid, int accountid, int runid)
    {
        this.puzzleid = puzzleid;
        this.accountid = accountid;
        this.runid = runid;
    }
}


[Serializable]
public class PuzzleData
{
   
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
    public List<Answer> answers;
    public int puzzleid;



    public Question(int id,string question, string description ,List<Answer> answers, int puzzleid)
    {
        this.id = id;
        this.question = question;
        this.description = description;
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

    public Answer(int id, string answer, bool isCorrect)
    {
        this.id = id;
        this.answer = answer;
        this.isCorrect = isCorrect;
    }

}

[Serializable]
public class Upgrade
{
    public enum Powers
    {
        HEALTH,
        RETRY
    }

    public int id;
    public string name;
    public int level;
    public Powers power;

    public Upgrade(int id, string name, int level, Powers power)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.power = power;

    }

   
}
