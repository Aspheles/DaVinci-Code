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
    public List<Answer> answers;

    private void Awake()
    {
        instance = this;
    }

    public void GetQuestion(Question question)
    {
        question_title.text = question.question;
        question_description.text = question.description;
    }

}
