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
public class Puzzle
{
    public int id;
    public string name;
    public PuzzleDifficulty difficulty;
   
    
    public Puzzle(int id ,string name, PuzzleDifficulty difficulty)
    {
        this.id = id;
        this.name = name;
        this.difficulty = difficulty;
        
    }

}


[Serializable]
public class Question
{
    public int id;
    public string question;
    public string description;
    public string image;
    public List<Answer> answer;

    

    public Question(int id, string question, string description, string image ,List<Answer> answer)
    {
        this.id = id;
        this.question = question;
        this.description = description;
        this.image = image;
        this.answer = answer;
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