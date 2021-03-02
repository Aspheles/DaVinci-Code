using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Answers : IManager
{
    public List<IMultipartFormSection> form;
    public void Create()
    {
        foreach (Answer answer in Session.instance.question.answers)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("question_id", Session.instance.question.id.ToString()),
                new MultipartFormDataSection("answer_id", answer.id.ToString()),
                new MultipartFormDataSection("title", answer.answer),
                new MultipartFormDataSection("value", answer.isCorrect.ToString()),
            };

            ApiHandler.instance.CallApiRequest("post", form, Request.CREATEANSWERS);    
        }
    }

    public void Delete(int id)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id.ToString())
        };
        ApiHandler.instance.CallApiRequest("post", form, Request.DELETEANSWER);
    }

    public void Edit()
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", Session.instance.question.id.ToString()),

        };
        ApiHandler.instance.CallApiRequest("post", form, Request.FETCHANSWERS);
    }
}
