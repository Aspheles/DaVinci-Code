using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FormController : MonoBehaviour
{

}

[Serializable]
public class Question
{
    public string question;
    public List<Answer> answer;

    

    public Question(string question, List<Answer> answer)
    {
        this.question = question;
        this.answer = answer;
    }
} 


[Serializable]
public class Answer
{
    public string answer;
    public bool isCorrect;

    public Answer(string answer, bool isCorrect)
    {
        this.answer = answer;
        this.isCorrect = isCorrect;
    }

}

public class Puzzle
{
    public int id;
    public string name;
    public List<Question> questions;
    public string difficulty;
    public string creator;
    public string description;

    public Puzzle(int id, string name, List<Question> questions, string difficulty, string creator, string description)
    {
        this.id = id;
        this.name = name;
        this.questions = questions;
        this.difficulty = difficulty;
        this.creator = creator;
        this.description = description;
    }
}