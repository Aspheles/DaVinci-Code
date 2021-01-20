using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionSession : MonoBehaviour
{
    public Puzzle puzzle;
    public Question question;
    public List<Question> allQuestions;
    public static QuestionSession instance;



    void Start()
    {
        instance = this;

    }

    public void AddAnswer(List<Answer> answers)
    {
        question = new Question(question.id, question.question, question.description, question.image, answers, puzzle.id);

    }

    public IEnumerator SaveAnswers()
    {
       foreach(Answer answer in question.answer)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("id", question.id.ToString()),
                new MultipartFormDataSection("title", answer.answer),
                new MultipartFormDataSection("value", answer.isCorrect.ToString())

            };
            UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/savepuzzledata.php", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                print("Success");
            }
        }
        
    }


   


}
