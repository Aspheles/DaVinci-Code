using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class QuestionCreator : MonoBehaviour
{
    public InputField questionInput;
    [SerializeField] GameObject answerInput;
    [SerializeField] Transform answerOptionsPos;
    [SerializeField] InputField puzzleNameInput;
    [SerializeField] TMP_Dropdown puzzleDifficultyInput;
    [SerializeField] GameObject questionsScrollView;
    [SerializeField] GameObject answersScrollView;
    [SerializeField] GameObject addButton;
    [SerializeField] GameObject finishButton;
    [SerializeField] GameObject closeButton;
    public TMP_InputField description;
    public TMP_Text message;

    public List<Question> question;
    public static QuestionCreator instance;
    public bool loaded;

    private void Awake()
    {
        instance = this;
        //Debug.Log(puzzleNameInput.text);
        //Debug.Log(puzzleDifficultyInput.options[puzzleDifficultyInput.value].text);
    }


    public void ResetData()
    {
        questionInput.text = "";
        description.text = "";

        foreach(Transform child in answerOptionsPos)
        {
            Destroy(child.gameObject);
        }
    }


    private void Update()
    {
        if(Session.instance.message != null)
        {
            message.text = Session.instance.ErrorHandling();
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
        questionInput.text = Session.instance.question.question;
        description.text = Session.instance.question.description;

        foreach (Transform child in answerOptionsPos)
        {
            Destroy(child.gameObject);
        }

        MoveAnswerRect();

        foreach (Answer answer in answers)
        {
            GameObject QuestionClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
            QuestionClone.transform.SetParent(answerOptionsPos);
            //QuestionClone.GetComponent<QuestionManager>().questionTitle.text = "Question " + QuestionCount + ": " + question.question;
            QuestionClone.GetComponent<AnswerManager>().id = answer.id;
            QuestionClone.GetComponent<AnswerManager>().answerField.text = answer.answer;
            QuestionClone.GetComponent<AnswerManager>().correctToggle.isOn = answer.isCorrect;
        }
        
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
        List<Answer> newAnswers = new List<Answer>();
        foreach (GameObject answer in GameObject.FindGameObjectsWithTag("answer"))
        {
            //Check if answer isn't empty
            if (!string.IsNullOrEmpty(answer.GetComponent<AnswerManager>().answerField.text))
            {
                Answer _answer = new Answer(answer.GetComponent<AnswerManager>().id, answer.GetComponent<AnswerManager>().answerField.text, answer.GetComponent<AnswerManager>().correctToggle.isOn);
                newAnswers.Add(_answer);
            }
            else
            {
                Session.instance.message = "erroranswerempty";
            }
        }

        print(JsonUtility.ToJson(newAnswers));
        if (HasCorrectValue(newAnswers)) 
        { 
            Session.instance.AddAnswer(newAnswers); 
        }
        else 
        {
            Session.instance.message = "erroranswervalue";
        }

    }

    /// <summary>
    /// Cancels the question creation and returns to the overwiew.
    /// </summary>

    public void CancelChanges()
    {
        Session.instance.question = null;
        loaded = false;
        ResetData();
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    private bool HasCorrectValue(List<Answer> answers)
    {
        int amount = 0;
        foreach(Answer answer in answers)
        {
            if (answer.isCorrect == false)
            {
                amount++;
            }
        }

        if (amount == answers.Count)
        {
            Debug.Log("Has no correct answer " + amount);
            return false;
        }
        else
        {
            Debug.Log("Has correct answer" + amount);
            return true;
        }
    }


}
