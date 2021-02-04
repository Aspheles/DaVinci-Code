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


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        

 
    }

   

    public void DeleteanswersOption()
    {
       // QuestionCreator.instance.Removeanswers(answersField.text);

        if(Session.instance.question.id != 0)
        {
            Answer item = Session.instance.question.answers.Find((x) => x.answer == answerField.text);

            if (item != null && item.id != 0)
            {
                StartCoroutine(DeleteanswersFromDb(item.id.ToString()));
            }
            Session.instance.question.answers.Remove(item);
        }

        
        
      
        Destroy(this.gameObject);
       
    }


    //Backend
    private IEnumerator DeleteanswersFromDb(string id)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id)
        };

        UnityWebRequest www = UnityWebRequest.Post(Request.DELETEANSWER, form);


        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) Debug.LogError(www.error);
        else Debug.Log($"item {id} has been deleted");
    }
}
