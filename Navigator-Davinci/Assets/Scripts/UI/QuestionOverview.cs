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
    public string[] idData;
    public string[] QuestionData;

    public void Start()
    {
        instance = this;
        StartCoroutine(FetchQuestionsData());
        //LoadQuestions();

        //if(QuestionSession.instance.allQuestions.Count > 0)
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadQuestions(string[] questions)
    {

        List<Answer> Answers = new List<Answer>();

        Questions = new List<Question>();

       

       
        

       

        
        foreach (Question question in Questions)
        {

            GameObject QuestionClone = Instantiate(QuestionInput, QuestionPositions.position, Quaternion.identity);
            QuestionClone.transform.SetParent(QuestionPositions);
            //QuestionClone.GetComponent<QuestionManager>().questionTitle.text = "Question " + QuestionCount + ": " + question.question;
            QuestionClone.GetComponent<QuestionManager>().questionTitle.text = question.question;

            
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


    public IEnumerator FetchQuestionsData()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            //QuestionSession.instance.puzzle.id.ToString()
            new MultipartFormDataSection("puzzle_id", 19.ToString()),
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/fetchquestions.php", form);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
           
            byte[] dbData = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(dbData);
            string[] Data = Result.Split("b"[0]);
            string[] id = Result.Split("id:"[0]);
            string[] title = Result.Split("title:"[0]);

            idData = Data;

            LoadQuestions(Data);


            //Questions.Add(new Question())

            
        }
    }
}
