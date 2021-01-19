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
    public string[] idData;
    public string[] QuestionData;
    public Scrollbar scrollbar;

    public void Start()
    {
        instance = this;
        StartCoroutine(FetchQuestionsData());
        StartCoroutine(changeScrollValue());
        //LoadQuestions();

        //if(QuestionSession.instance.allQuestions.Count > 0)
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
    public void LoadQuestions(List<Question> questions)
    {

        List<Answer> Answers = new List<Answer>();

        Questions = new List<Question>();

       

       
        foreach (Question question in questions)
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
            
            JSONArray jsonData = JSON.Parse(Result) as JSONArray;

            for (int i = 0; i < jsonData.Count; i++)
            {
                //Local variables
                bool isDone;
                string questionId = jsonData[i].AsObject["id"];
                string questionTitle = jsonData[i].AsObject["title"];
                string questionDescription = jsonData[i].AsObject["description"];
                string questionImage = jsonData[i].AsObject["image"];

                Question a = new Question(int.Parse(questionId), questionTitle, questionDescription, questionImage, null);
                Questions.Add(a);

            }

            if(Questions.Count > 0)
            {
                LoadQuestions(Questions);
            }
           

            //Questions.Add(new Question())

            
        }
    }
}
