using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    public PuzzleData puzzle;
    public Question question;
    public int questionid;
    public static Session instance;
    public GameObject answerObject;
    public string message;
    public string image;

    void Awake()
    {
        instance = this;     
    }

    public string ErrorHandling()
    {
        switch (message)
        {
            case "errorduplicate":
                return "Name is already in use";
            case "erroruser":
                return "Username or password is incorrect";
            default:
                return "";
                
        }
    }


    public void AddAnswer(List<Answer> answers)
    {
       
        if (answers.Count > 0)
        {
            if(question == null)
            {
                questionid = 0;
            }
            else
            {
                questionid = question.id;
            }

            if (image == null) image = "test";
            question = new Question(questionid, QuestionCreator.instance.questionInput.text, QuestionCreator.instance.description.text, image, answers, puzzle.id);
            //StartCoroutine(SaveQuestion());

            //Send backend request from api to save question
            new Questions().Create();
        }
    }

 
    public IEnumerator LoadAnswers()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", question.id.ToString()),
         
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/loadanswers.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            print(www.downloadHandler.text);
            if (!www.downloadHandler.text.Contains("Error"))
            {
                byte[] dbData = www.downloadHandler.data;
                string Result = System.Text.Encoding.Default.GetString(dbData);

                JSONArray jsonData = JSON.Parse(Result) as JSONArray;
                //List<Answer> answers = new List<Answer>();
                question.answers = new List<Answer>();

                for (int i = 0; i < jsonData.Count; i++)
                {
                    string answerid = jsonData[i].AsObject["id"];
                    string answertitle = jsonData[i].AsObject["title"];
                    string answervalue = jsonData[i].AsObject["value"];
                    bool answerbool;
                    if (answervalue == "0") answerbool = false;
                    else answerbool = true;


                    question.answers.Add(new Answer(int.Parse(answerid), answertitle, answerbool));

                }
                Launcher.instance.OpenPuzzleQuestionCreatorMenu();

            }
            else
            {
                Debug.Log("Question doesn't have answers");
                QuestionCreator.instance.ResetData();
                question.answers = new List<Answer>();
                Launcher.instance.OpenPuzzleQuestionCreatorMenu();
            }
            

        }
    }



    public IEnumerator SaveAnswers()
    {
        
        //Answers
        foreach (Answer answer in question.answers)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("question_id", question.id.ToString()),
                new MultipartFormDataSection("answer_id", answer.id.ToString()),
                new MultipartFormDataSection("title", answer.answer),
                new MultipartFormDataSection("value", answer.isCorrect.ToString()),
            };
            
            UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/savepuzzledata.php", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print(www.downloadHandler.text);
                print("Success");
            }
        }
        QuestionCreator.instance.loaded = false;
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
        QuestionCreator.instance.ResetData();

    }

  


}
