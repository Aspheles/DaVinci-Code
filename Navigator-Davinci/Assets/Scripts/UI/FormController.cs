using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormController : MonoBehaviour
{
    public string question;
    public List<Button> answers;
    public List<Question> answersText;

    public void Start()
    {
        answersText = new List<Question> { new Question("Variable 1", false),
         new Question("Variable 2", false), new Question("Variable 3", false), new Question("Variable 4", true), new Question("Variable 5", false)};
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
                answers[i].GetComponentInChildren<TMP_Text>().text = answersText[i].question;
                
            }
            
            foreach(Button answer in answers)
            {
                answer.onClick.AddListener(() => CompareAnswer(answer.GetComponentInChildren<TMP_Text>().text));
            }
        }
        
    }

    public void CompareAnswer(string a)
    {
        int index = answersText.FindIndex((x) => x.question == a);
        Debug.Log(index);

    }
}

public class Question
{
    public string question;
    public bool correct;
    

    public Question(string question, bool correct)
    {
        this.question = question;
        this.correct = correct;
    }
}