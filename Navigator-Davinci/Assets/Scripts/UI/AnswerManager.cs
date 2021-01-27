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

   

    public void DeleteAnswerOption()
    {
       // QuestionCreator.instance.RemoveAnswer(answerField.text);

        if(QuestionSession.instance.question.id != 0)
        {
            Answer item = QuestionSession.instance.question.answer.Find((x) => x.answer == answerField.text);

            if (item != null && item.id != 0)
            {
                StartCoroutine(DeleteAnswerFromDb(item.id.ToString()));
            }
            QuestionSession.instance.question.answer.Remove(item);
        }

        
        
      
        Destroy(this.gameObject);
       
    }


    private IEnumerator DeleteAnswerFromDb(string id)
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
