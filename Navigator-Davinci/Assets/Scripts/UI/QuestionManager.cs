﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public int id;
    public TMP_Text questionTitle;
    public static QuestionManager instance;
    public GameObject questionObject;
    


    private void Start()
    {
        instance = this;
        
    }

    /// <summary>
    /// Finds the question in the question overview and sets questionTitle to its text.
    /// If the value is not null, sends the data to the question session with the given values.
    /// Launcher will open the next page.
    /// </summary>
    public void EditQuestion()
    {
        Question Question = QuestionOverview.instance.Questions.Find((x) => x.id == id);
        //Debug.Log(Question.question);
        if (Question != null)
        {
            Session.instance.question = Question;
            //StartCoroutine(Session.instance.LoadAnswers());
            Launcher.instance.OpenPuzzleQuestionCreatorMenu();


        }
        else
        {
            //QuestionCreator.instance.ResetData();
            Debug.Log("No answers in this question found");
            Launcher.instance.OpenPuzzleQuestionCreatorMenu();
        }
        
    }

    /// <summary>
    /// Question will be deleted.
    /// </summary>
    public void DeleteQuestion()
    {

        //QuestionCreator.instance.RemoveAnswer(questionTitle.text);
        //print(instance);
        Question Question = QuestionOverview.instance.Questions.Find((x) => x.id == id);
        //Debug.Log(Question.question);
        if (Question != null)
        {
            
            //questionObject = this;
            QuestionOverview.instance.questionObject = questionObject;
            new Questions().Delete(Question.id);
            //StartCoroutine(Session.instance.LoadAnswers());
        }
       
    }
}
