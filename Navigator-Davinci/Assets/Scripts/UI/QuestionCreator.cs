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
    [SerializeField] GameObject questionsScrollView;
    [SerializeField] GameObject answersScrollView;
    [SerializeField] GameObject addButton;
    [SerializeField] GameObject finishButton;
    [SerializeField] GameObject closeButton;


    public List<Question> question;
    public static QuestionCreator instance;
    bool loaded;

    private void Start()
    {
        instance = this;
        //Debug.Log(puzzleNameInput.text);
        //Debug.Log(puzzleDifficultyInput.options[puzzleDifficultyInput.value].text);
        

      
    }

   
    private void Update()
    {
        
        

        if (QuestionSession.instance.question != null)
        {
            
            GameObject.Find("title").GetComponent<Text>().text = "Edit your question";
            questionInput.text = QuestionSession.instance.question.question;
            LoadAnswers(QuestionSession.instance.question.answer);
            //QuestionSession.instance.question = null;

        }
        else if(QuestionSession.instance.question == null)
        {
            ResetAnswerRect();
        }
      
    }

    public void LoadAnswers(List<Answer> answers)
    {

        foreach (Transform child in answerOptionsPos)
        {
            Destroy(child.gameObject);
        }

        if(answers.Count > 0)
        {
            MoveAnswerRect();
            foreach (Answer answer in answers)
            {
                GameObject QuestionClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
                QuestionClone.transform.SetParent(answerOptionsPos);
                //QuestionClone.GetComponent<QuestionManager>().questionTitle.text = "Question " + QuestionCount + ": " + question.question;
                QuestionClone.GetComponent<AnswerManager>().answerField.text = answer.answer;
                QuestionClone.GetComponent<AnswerManager>().correctToggle.isOn = answer.isCorrect;
            }
        }
        
        
        
        
        
    }

    public void RemoveAnswer(string answer)
    {
        /*
        foreach(Question q in question)
        {
          Answer item = q.answer.Find((x) => x.answer == answer);
          if(item != null)
          {
            q.answer.Remove(item);
          }
        }*/

        Answer item = QuestionSession.instance.question.answer.Find((x) => x.answer == answer);
        if (item != null) QuestionSession.instance.question.answer.Remove(item);
    }

    public void CreateAnswerOptions()
    {

        MoveAnswerRect();
        GameObject answerClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
        answerClone.transform.SetParent(answerOptionsPos);
        //Answer item = new Answer(answerClone.GetComponent<AnswerManager>().answerField.text, answerClone.GetComponent<AnswerManager>().correctToggle.isOn);
        
    }

    void MoveAnswerRect()
    {
        answersScrollView.SetActive(true);
        finishButton.SetActive(true);

        questionsScrollView.GetComponent<RectTransform>().anchoredPosition = new Vector3(-182, 0, 0);
        addButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(104, -210, 0);
        finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(289, -210, 0);
        closeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(390, 200, 0);
    }

    void ResetAnswerRect()
    {
        answersScrollView.SetActive(false);
        finishButton.SetActive(false);

        questionsScrollView.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        addButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(2, -210, 0);
        finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(99, -210, 0);
        closeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 200, 0);
    }

    public void FinishQuestions()
    {
        question = new List<Question>();

        List<Answer> answers = new List<Answer>();
        GameObject[] item = GameObject.FindGameObjectsWithTag("answer");

        for (int i = 0; i < item.Length; i++)
        {
            answers.Add(new Answer(item[i].GetComponent<AnswerManager>().answerField.text, item[i].GetComponent<AnswerManager>().correctToggle.isOn));
        }

        /*
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
        */
        
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
