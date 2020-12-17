using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;


public class QuestionOverview : MonoBehaviour
{
    public Transform QuestionPositions;
    public GameObject QuestionInput;


    public void Start()
    {
        LoadQuestions();
    }
    public void LoadQuestions()
    {
        List<Question> Questions;
        List<Answer> Answers;
        Answers = new List<Answer>
        {
            new Answer("name", false),
            new Answer("yolo", true),
            new Answer("ding", false)
        };

        Questions = new List<Question>
        {
            new Question("wat denk jij vandaag?", Answers),
            new Question("hoe praat jij tegen vrouwen?", Answers),
            new Question("Wat is het weer vandaag?", Answers)
        };

        foreach (Question question in Questions)
        {
            GameObject QuestionClone = Instantiate(QuestionInput, QuestionPositions.position, Quaternion.identity);
            QuestionClone.transform.SetParent(QuestionPositions);
            QuestionClone.GetComponent<QuestionManager>().questionTitle.text = question.question;

        } 



    }
}
