using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text question_title;
    public TMP_Text question_description;
    public Question _question;
    public List<Answer> answers;

    public Transform answerPosition;
    public GameObject answerObject;

    private void Awake()
    {
        instance = this;
       // _question = RunManager.instance.questions[0];
        //GetQuestion(_question);
    }

    private void Update()
    {
        if (RunManager.instance.answersLoaded)
        {
            RunManager.instance.loadingScreen.SetActive(true);
            DisplayAnswers();
            RunManager.instance.answersLoaded = false;
        }

        if(RunManager.instance.terminal.questions.Count > 0)
        {
            //RunManager.instance.loadingScreen.SetActive(false);
            GetQuestion(RunManager.instance.terminal.questions[RunManager.instance.terminal.questionNumber]);
        }
        else
        {
            //RunManager.instance.loadingScreen.SetActive(true);
        }
        
    }

    public void GetQuestion(Question question)
    {
        question_title.text = question.question;
        question_description.text = question.description;
    }

    public void DisplayAnswers()
    {
        foreach(Transform answerObject in GameManager.instance.answerPosition)
        {
            Destroy(answerObject.gameObject);
        }

        foreach(Answer answer in RunManager.instance.terminal.answers)
        {
            GameObject copyAnswer = Instantiate(answerObject, answerPosition.position, Quaternion.identity);
            copyAnswer.transform.SetParent(answerPosition);
            copyAnswer.GetComponent<GameAnswer>().id = answer.id;
            copyAnswer.GetComponent<GameAnswer>().answer.text = answer.answer;
            copyAnswer.GetComponent<GameAnswer>().isCorrect = answer.isCorrect;
        }

        RunManager.instance.terminal.questionsLoaded = false;
        Debug.Log("Trying to instantiate answers");
        RunManager.instance.loadingScreen.SetActive(false);
    }
}
