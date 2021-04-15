using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using Lean.Gui;
using Lean;

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
    public string email;
    public Text notificationText;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(message))
        {
            notificationText.text = ErrorHandling();
            LeanPulse.PulseAll("Notification");
            message = string.Empty;
        }
    }

    public string ErrorHandling()
    {
        switch (message)
        {
            case "errorduplicate":
                return "Name is already in use";
            case "erroruser":
                return "Username or password is incorrect";
            case "erroruserempty":
                return "Inputs can't be empty";
            case "answerduplicate":
                return "Duplicate answers found with same name";
            case "erroranswervalue":
                return "You need atleast 1 true answer";
            case "erroranswerempty":
                return "Answer can't be empty";
            case "errormailused":
                return "Mail is already in use";
            case "errorcodeexpired":
                return "Verification code has been expired";
            case "errortokeninvalid":
                return "Verification code is incorrect";
            case "erroremailinvalid":
                return "Account can't be found";


            default:
                return message;
                
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

            //Creating questions, so it can be sent to the database 
            question = new Question(questionid, QuestionCreator.instance.questionInput.text, QuestionCreator.instance.description.text, answers, puzzle.id);
            //StartCoroutine(SaveQuestion());

            //Send backend request from api to save question
            new Questions().Create();


        }
    }



}
