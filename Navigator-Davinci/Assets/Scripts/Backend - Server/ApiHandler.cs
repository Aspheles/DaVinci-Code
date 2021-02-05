using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ApiHandler : MonoBehaviour
{
    public static ApiHandler instance;

    private void Awake()
    {
      
        instance = this;
    }

    public enum RequestType{
        CREATE_PUZZLE,
        EDIT_PUZZLE,
        DELETE_PUZZLE
    }

    /// <summary>
    /// Makes a api request to the backend with the data
    /// </summary>
    /// <returns></returns>
    /// 
    public void CallApiRequest(string type, List<IMultipartFormSection> form, string url)
    {
        StartCoroutine(ApiRequest(type, form, url));
    }

    public IEnumerator ApiRequest(string type, List<IMultipartFormSection> form, string url)
    {
       
        if (type == "post")
        {
            UnityWebRequest www = UnityWebRequest.Post(url, form);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                byte[] dbData = www.downloadHandler.data;
                string Result = System.Text.Encoding.Default.GetString(dbData);
                
                JSONArray Data = JSON.Parse(Result) as JSONArray;
                print(Result);
                ApiController.instance.CheckData(Data, url);
            }
        }
        else if(type == "get")
        {
            UnityWebRequest www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                byte[] dbData = www.downloadHandler.data;
                string Result = System.Text.Encoding.Default.GetString(dbData);

                JSONArray Data = JSON.Parse(Result) as JSONArray;

                ApiController.instance.CheckData(Data, url);
            }
        }
        
    }

    public string Callback(string error)
    {
       

        return "";
    }

}



