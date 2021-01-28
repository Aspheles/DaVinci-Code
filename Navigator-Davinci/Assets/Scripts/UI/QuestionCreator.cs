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
        
        
        // Checks if question is being edited so the values can be updated
        if (QuestionSession.instance.question != null && !loaded)
        {
            
            GameObject.Find("title").GetComponent<Text>().text = "Edit your question";
            questionInput.text = QuestionSession.instance.question.question;
            description.text = QuestionSession.instance.question.description;
           
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

        if(answers != null && answers.Count > 0)
        {
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
        List<Answer> newAnswers = new List<Answer>();
       // question = new List<Question>();

        GameObject[] item = GameObject.FindGameObjectsWithTag("answer");

        if (item.Length > 0)
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (string.IsNullOrEmpty(item[i].GetComponent<AnswerManager>().answerField.text))
                {
                    print("Answer can't be empty");
                }
                else
                {

                    int id = 0;

                    if(QuestionSession.instance.question != null && QuestionSession.instance.question.answer != null)
                    {
                        Answer foundAnswer = QuestionSession.instance.question.answer.Find((x) => x.id == item[i].GetComponent<AnswerManager>().id);

                        if (foundAnswer != null)
                        {
                            id = foundAnswer.id;
                        }
                    }
                    
                   
                    
                    Answer answer = new Answer(id, item[i].GetComponent<AnswerManager>().answerField.text, item[i].GetComponent<AnswerManager>().correctToggle.isOn);
                    newAnswers.Add(answer);

                }
            }
        }
            

        QuestionSession.instance.AddAnswer(newAnswers);
        
        //StartCoroutine(QuestionSession.instance.SaveAnswers());


       
        //questiontest.Add(new Question(question.text, ))
    }

    /// <summary>
    /// Cancels the question creation and returns to the overwiew.
    /// </summary>

    public void CancelChanges()
    {
        QuestionSession.instance.question = null;
        loaded = false;
        ResetData();
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    


}
