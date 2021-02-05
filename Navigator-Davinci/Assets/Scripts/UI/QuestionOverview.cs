using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;


public class QuestionOverview : MonoBehaviour
{
    public Transform QuestionPositions;
    public GameObject QuestionInput;
    public List<Question> Questions;
    public static QuestionOverview instance;
    public Scrollbar scrollbar;
    public GameObject questionObject;

    public void Awake()
    {
        instance = this;
        //StartCoroutine(FetchQuestionsData());
        StartCoroutine(changeScrollValue());
        //LoadQuestions();

        //if(Session.instance.allQuestions.Count > 0)
    }

    IEnumerator changeScrollValue()
    {
        Debug.Log("Now its called");
        yield return new WaitForSeconds(0.1f);
        scrollbar.value = 1;
        Debug.Log("Now its setted");
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadQuestions()
    {
        

        foreach (Question question in Questions)
        {

            GameObject QuestionClone = Instantiate(QuestionInput, QuestionPositions.position, Quaternion.identity);
            QuestionClone.transform.SetParent(QuestionPositions);
            //QuestionClone.GetComponent<QuestionManager>().questionTitle.text = "Question " + QuestionCount + ": " + question.question;
            QuestionClone.GetComponent<QuestionManager>().questionTitle.text = question.question;
            QuestionClone.GetComponent<QuestionManager>().id = question.id;

            
        } 



    }
    /// <summary>
    /// Add new question.
    /// </summary>
    public void CreateNewQuestion()
    {
        Session.instance.question = null;
        //QuestionCreator.instance.ResetData();
        Launcher.instance.OpenPuzzleQuestionCreatorMenu();
    }


  

}
