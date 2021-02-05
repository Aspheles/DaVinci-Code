using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Answers : IManager
{
    public void Create()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
