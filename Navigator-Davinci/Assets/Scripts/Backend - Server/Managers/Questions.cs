using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Questions : IManager
{
    public List<IMultipartFormSection> form;
    public void Create()
    {
        //Questions
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("question_id", Session.instance.question.id.ToString()),
            new MultipartFormDataSection("question_title", QuestionCreator.instance.questionInput.text),
            new MultipartFormDataSection("question_description",  QuestionCreator.instance.description.text),
            new MultipartFormDataSection("puzzle_id", Session.instance.puzzle.id.ToString()),
            new MultipartFormDataSection("image", Session.instance.question.image),
        };
        ApiHandler.instance.CallApiRequest("post", form, Request.CREATEQUESTION);
    }

    public void Delete(int id)
    {
        
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id.ToString()),
        };
        ApiHandler.instance.CallApiRequest("post", form, Request.DELETEQUESTION);
    }

    public void Edit()
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            //Session.instance.puzzle.id.ToString()
            new MultipartFormDataSection("puzzle_id", Session.instance.puzzle.id.ToString()),
            //Session.instance.puzzle.id
        };
        ApiHandler.instance.CallApiRequest("post", form, Request.FETCHQUESTIONS);
    }
}
