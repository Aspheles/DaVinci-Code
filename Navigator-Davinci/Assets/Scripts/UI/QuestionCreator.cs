using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class QuestionCreator : MonoBehaviour
{
    [SerializeField] InputField questionInput;
    [SerializeField] GameObject answerInput;
    [SerializeField] Transform answerOptionsPos;
    [SerializeField] InputField puzzleNameInput;
    [SerializeField] TMP_Dropdown puzzleDifficultyInput;

    public List<Question> question;
    public static QuestionCreator instance;


    private void Start()
    {
        instance = this;
        Debug.Log(puzzleNameInput.text);
        Debug.Log(puzzleDifficultyInput.options[puzzleDifficultyInput.value].text);
    }

    private void Update()
    {
        
    }

    public void CreateAnswerOptions()
    {
        
        GameObject answerClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
        answerClone.transform.SetParent(answerOptionsPos);
        //Answer item = new Answer(answerClone.GetComponent<AnswerManager>().answerField.text, answerClone.GetComponent<AnswerManager>().correctToggle.isOn);
        
    }

    public void FinishQuestions()
    {
        question = new List<Question>();

        List<Answer> answers = new List<Answer>();
        GameObject[] item = GameObject.FindGameObjectsWithTag("answer");

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("answer").Length; i++)
        {
            answers.Add(new Answer(item[i].GetComponent<AnswerManager>().answerField.text, item[i].GetComponent<AnswerManager>().correctToggle.isOn));
        }

        if(answers.Count > 0)
        {
            question.Add(new Question(questionInput.text, answers));
            StartCoroutine(CreatePuzzle());
            foreach(Question q in question)
            {
                for(int i = 0; i< q.answer.Count; i++)
                {
                    StartCoroutine(CreateQuestion(q.answer[i].answer, q.answer[i].isCorrect));
                }
                
            }
        }
        
        //questiontest.Add(new Question(question.text, ))
    }

    IEnumerator CreatePuzzle()
    {
        //Creates a list for the data so it can be sent to the PHP file can get it trough $_POST
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("puzzlename", puzzleNameInput.text),
            new MultipartFormDataSection("puzzledifficulty", puzzleDifficultyInput.options[puzzleDifficultyInput.value].text),
           
        };
        //formData.Add(new MultipartFormFileSection(email, "my file data"));

        //Sending the data 
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", formData);

        yield return www.SendWebRequest();

    }

    IEnumerator CreateQuestion(string answerTitle, bool answerValue)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            //Creates a list for the data so it can be sent to the PHP file can get it trough $_POST

            new MultipartFormDataSection("answertitle", answerTitle),
            new MultipartFormDataSection("answervalue", answerValue.ToString())
        };



        //formData.Add(new MultipartFormFileSection(email, "my file data"));

        //Sending the data 
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/answer.php", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

}
