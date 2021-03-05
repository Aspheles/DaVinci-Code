using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AnswerManager : MonoBehaviour
{
    public int id;
    public InputField answerField;
    public Toggle correctToggle;
    public static AnswerManager instance;
    public GameObject answerObject;


    private void Start()
    {
        instance = this;
    }

    public void DeleteanswersOption()
    {
       // QuestionCreator.instance.Removeanswers(answersField.text);

        if(Session.instance.question.id != 0)
        {
            Answer item = Session.instance.question.answers.Find((x) => x.answer == answerField.text);

            if (item != null && item.id != 0)
            {
                //StartCoroutine(DeleteanswersFromDb(item.id.ToString()));
                Session.instance.answerObject = answerObject;
                new Answers().Delete(item.id);
            }
            Session.instance.question.answers.Remove(item);
        }

        
        
      
        Destroy(this.gameObject);
       
    }


}
