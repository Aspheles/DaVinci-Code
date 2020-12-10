using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FormController : MonoBehaviour
{
    public TMP_Text questionText;
    public string question;
    public List<Button> answers;
    public List<Answer> answersText;
  

    public void Start()
    {
        questionText = GameObject.FindGameObjectWithTag("question").GetComponent<TMP_Text>();
        questionText.text = question;
        if(answersText.Count < 5)
        {
            Debug.LogError("You need 5 answers");
        }
        ChangeAnswers();
        
              
    }

    public void ChangeQuestion()
    {

    }

    public void ChangeAnswers()
    {
        if(answers.Count > 0)
        {
            for(int i = 0; i < answersText.Count; i++)
            {
                answers[i].GetComponentInChildren<TMP_Text>().text = answersText[i].answer;
                
            }
            
            foreach(Button answer in answers)
            {
                answer.onClick.AddListener(() => CompareAnswer(answer));

            }
        }
        
    }

    void SelectAnswer()
    {

    }
    public void CompareAnswer(Button a)
    {
        
        int index = answersText.FindIndex((x) => x.answer == a.GetComponentInChildren<TMP_Text>().text);
        if(answersText[index].correct == true)
        {
            a.GetComponent<Image>().color = Color.green;
            Debug.Log("Correct answer");
        }
        else
        {
            a.GetComponent<Image>().color = Color.red;
            Debug.Log("False answer");
        }

    }
}

[Serializable]
public class Answer
{
    public string answer;
    public bool correct;
    

    public Answer(string answer, bool correct)
    {
        this.answer = answer;
        this.correct = correct;
    }
}