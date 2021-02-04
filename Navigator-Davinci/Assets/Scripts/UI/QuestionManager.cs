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

    /// <summary>
    /// Finds the question in the question overview and sets questionTitle to its text.
    /// If the value is not null, sends the data to the question session with the given values.
    /// Launcher will open the next page.
    /// </summary>
    public void EditQuestion()
    {
        Question Question = QuestionOverview.instance.Questions.Find((x) => x.question == questionTitle.text);
        //Debug.Log(Question.question);
        if (Question != null)
        {
            Session.instance.question = Question;
            StartCoroutine(Session.instance.LoadAnswers());
            

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
        Destroy(this.gameObject);
    }
}
