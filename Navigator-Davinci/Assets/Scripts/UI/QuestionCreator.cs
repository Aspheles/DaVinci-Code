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
        
        
        // Checks if question is being edited so the values can be updated
        if (QuestionSession.instance.question != null && !loaded)
        {
            
            GameObject.Find("title").GetComponent<Text>().text = "Edit your question";
            questionInput.text = QuestionSession.instance.question.question;
            LoadAnswers(QuestionSession.instance.question.answer);
            loaded = true;
            //QuestionSession.instance.question = null;

        }
        
        if(GameObject.FindGameObjectsWithTag("answer").Length > 0)
        {
            MoveAnswerRect();
        }
        else
        {
            ResetAnswerRect();
        }
      
    }

    /// <summary>
    /// Removing old answers if there are any, afterwards loads in answers from current question.
    /// </summary>
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

    /// <summary>
    /// Removes a answer from a question.
    /// </summary>
    /// <param name="answer"></param>
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

        //Answer item = QuestionSession.instance.question.answer.Find((x) => x.answer == answer);
        //if (item != null) QuestionSession.instance.question.answer.Remove(item);

        //Destroy(this.gameObject);
    }

    /// <summary>
    /// Creates new answer.
    /// </summary>
    public void CreateAnswerOptions()
    {

        MoveAnswerRect();
        GameObject answerClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
        answerClone.transform.SetParent(answerOptionsPos);
        //Answer item = new Answer(answerClone.GetComponent<AnswerManager>().answerField.text, answerClone.GetComponent<AnswerManager>().correctToggle.isOn);
        
    }

    /// <summary>
    /// Moves containers to their own position, sets the answers container to active.
    /// </summary>
    void MoveAnswerRect()
    {
        answersScrollView.SetActive(true);
        finishButton.SetActive(true);

        questionsScrollView.GetComponent<RectTransform>().anchoredPosition = new Vector3(-182, 0, 0);
        addButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(104, -210, 0);
        finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(289, -210, 0);
        closeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(390, 200, 0);
    }


    /// <summary>
    /// Sets the answers container to false.
    /// </summary>
    void ResetAnswerRect()
    {
        answersScrollView.SetActive(false);
        finishButton.SetActive(false);

        questionsScrollView.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        addButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(2, -210, 0);
        finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(99, -210, 0);
        closeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 200, 0);
    }

    /// <summary>
    /// Edits or creates new questions with answers, and saves question to database.
    /// </summary>
    public void FinishQuestions()
    {
        question = new List<Question>();

        List<Answer> answers = new List<Answer>();
        GameObject[] item = GameObject.FindGameObjectsWithTag("answer");

        for (int i = 0; i < item.Length; i++)
        {
            answers.Add(new Answer(item[i].GetComponent<AnswerManager>().answerField.text, item[i].GetComponent<AnswerManager>().correctToggle.isOn));
        }
        loaded = false;
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();

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

    /// <summary>
    /// Cancels the question creation and returns to the overwiew.
    /// </summary>

    public void CancelChanges()
    {
        loaded = false;
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    /// <summary>
    /// Sends puzzle info to the backend.
    /// </summary>
    /// <returns></returns>
    IEnumerator CreatePuzzle()
    {
        //Creates a list for the data so it can be sent to the PHP file can get it through $_POST
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

    /// <summary>
    /// Creates a list for the data so it can be sent to the database, with the given values.
    /// </summary>
    /// <param name="answerTitle">
    /// This is the answer title.
    /// </param>
    /// <param name="answerValue">
    /// This is the answer value.
    /// </param>
    /// <returns>
    /// Sends back the status code for the request.
    /// </returns>
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
