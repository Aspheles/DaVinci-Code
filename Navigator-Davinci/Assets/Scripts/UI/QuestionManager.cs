using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionTitle;
    public static QuestionManager instance;


    private void Start()
    {
        instance = this;
    }

    public void EditQuestion()
    {
        Question Question = QuestionOverview.instance.Questions.Find((x) => x.question == questionTitle.text);
        //Debug.Log(Question.question);
        if(Question != null)
        {
            QuestionSession.instance.question = Question;
            Launcher.instance.OpenPuzzleQuestionCreatorMenu();
        }
        
    }

    public void DeleteQuestion()
    {
        QuestionCreator.instance.RemoveAnswer(questionTitle.text);
        Destroy(this.gameObject);
    }
}
