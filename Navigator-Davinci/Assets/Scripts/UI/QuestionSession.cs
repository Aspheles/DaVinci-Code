using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class QuestionSession : MonoBehaviour
{
    public PuzzleData puzzle;
    public Question question;
    public static QuestionSession instance;

    void Awake()
    {
        instance = this;     
    }


    public void AddAnswer(List<Answer> answers)
    {
        question = new Question(question.id, QuestionCreator.instance.questionInput.text, QuestionCreator.instance.description.text, question.image, answers, puzzle.id);
        print(question.id);
        print(question.question);
        StartCoroutine(SaveQuestion());
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
                question.answer = new List<Answer>();

                for (int i = 0; i < jsonData.Count; i++)
                {
                    string answerid = jsonData[i].AsObject["id"];
                    string answertitle = jsonData[i].AsObject["title"];
                    string answervalue = jsonData[i].AsObject["value"];
                    bool answerbool;
                    if (answervalue == "0") answerbool = false;
                    else answerbool = true;


                    question.answer.Add(new Answer(int.Parse(answerid), answertitle, answerbool));

                }
                Launcher.instance.OpenPuzzleQuestionCreatorMenu();

            }
            else
            {
                Debug.Log("Question doesn't have answers");
                QuestionCreator.instance.ResetData();
                question.answer = new List<Answer>();
                Launcher.instance.OpenPuzzleQuestionCreatorMenu();
            }
            

        }
    }

    public IEnumerator SaveQuestion()
    {
        //Questions
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("question_id", question.id.ToString()),
            new MultipartFormDataSection("question_title", QuestionCreator.instance.questionInput.text),
            new MultipartFormDataSection("question_description",  QuestionCreator.instance.description.text),
            new MultipartFormDataSection("question_image", "test"),
            new MultipartFormDataSection("puzzle_id", puzzle.id.ToString()),
        };
        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/savequestion.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            if(!string.IsNullOrEmpty(www.downloadHandler.text)) question.id = int.Parse(www.downloadHandler.text);
            print("Success");
            StartCoroutine(SaveAnswers());
           
        }
    }


    public IEnumerator SaveAnswers()
    {
        
        //Answers
        foreach (Answer answer in question.answer)
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
