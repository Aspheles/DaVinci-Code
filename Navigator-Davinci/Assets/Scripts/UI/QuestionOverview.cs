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
    public List<Question> Questions;
    public static QuestionOverview instance;


    public void Start()
    {
        instance = this;
        LoadQuestions();
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadQuestions()
    {
        
        List<Answer> Answers;
        Answers = new List<Answer>
        {
            new Answer("name", false),
            new Answer("yolo", true),
            new Answer("ding", false),
            new Answer("mah", false),
            new Answer("Ik weiger", true)
        };

        List<Answer> newAnswers = new List<Answer>();
        

        Questions = new List<Question>
        {
            new Question("wat denk jij vandaag?", Answers),
            new Question("hoe praat jij tegen mensen?", Answers),
            new Question("Wat is het weer vandaag?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("Wat weegt zwaarder, een kilo staal of een kilo veren?", newAnswers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland?", Answers),
            new Question("willen wij meer of minder vakantie hebben in Nederland? Ik ben namelijk van mening dat het veel en veel meer moet zijn", Answers),
            new Question("Wat weegt zwaarder, een kilo staal of een kilo veren?", Answers)
        };

        int QuestionCount = 1;
        foreach (Question question in Questions)
        {

            GameObject QuestionClone = Instantiate(QuestionInput, QuestionPositions.position, Quaternion.identity);
            QuestionClone.transform.SetParent(QuestionPositions);
            //QuestionClone.GetComponent<QuestionManager>().questionTitle.text = "Question " + QuestionCount + ": " + question.question;
            QuestionClone.GetComponent<QuestionManager>().questionTitle.text = question.question;

            QuestionCount++;
        } 



    }
    /// <summary>
    /// Add new question.
    /// </summary>
    public void CreateNewQuestion()
    {
        QuestionSession.instance.question = null;
        Launcher.instance.OpenPuzzleQuestionCreatorMenu();
    }
}
