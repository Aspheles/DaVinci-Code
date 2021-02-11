using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    public PuzzleData puzzle;
    public Question question;
    public int questionid;
    public static Session instance;
    public GameObject answerObject;
    public string message;
    public string image;
    public List<Answer> fetchedAnswers;

    void Awake()
    {
        instance = this;     
    }

    public string ErrorHandling()
    {
        switch (message)
        {
            case "errorduplicate":
                return "Name is already in use";
            case "erroruser":
                return "Username or password is incorrect";
            case "answerduplicate":
                return "Duplicate answers found with same name";
            default:
                return "";
                
        }
    }


    public void AddAnswer(List<Answer> answers)
    {
       
        if (answers.Count > 0)
        {
            if(question == null)
            {
                questionid = 0;
            }
            else
            {
                questionid = question.id;
            }

            if (image == null) image = "test";
            question = new Question(question.id, QuestionCreator.instance.questionInput.text, QuestionCreator.instance.description.text, question.image, answers, puzzle.id);
            //StartCoroutine(SaveQuestion());

            //Send backend request from api to save question
            new Questions().Create();
            
        }
    }



}
