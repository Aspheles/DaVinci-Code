using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class VerificationManager : MonoBehaviour
{
    public static VerificationManager instance;
    public string email;
    public string username;
    public string token;
    public int verified;
    public string expiredate;

    public byte[] results;
    
    public string testData;
    public string[] Data;
    

   

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(GameObject.Find("LoggedInMenu") == isActiveAndEnabled)
        {
            GameObject.Find("user").GetComponent<Text>().text = "Welcome " + username;
        }
    }

    public IEnumerator GetToken(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/verification.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data);
            results = www.downloadHandler.data;

         
            testData = System.Text.Encoding.Default.GetString(results);
            Data = testData.Split("b" [0]);
            
            if(int.Parse(Data[2]) == 1)
            {
                Debug.Log("Account is activated");
                MenuManager.instance.OpenMenu("loggedin");
            }
            else
            {
                MenuManager.instance.OpenMenu("verification");
            }
        }
    }

    public void RequestNewCode()
    {
        StartCoroutine(ResendCode(Verification.instance.emailInput.text));
    }

    public void VerifyAccount(string email)
    {
        StartCoroutine(Verify(email));
    }

    public IEnumerator ResendCode(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/resendcode.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data);
            results = www.downloadHandler.data;


            testData = System.Text.Encoding.Default.GetString(results);
            Data = testData.Split("b"[0]);
            Authentication.instance.SaveData(Data);
            //MenuManager.instance.OpenMenu("verification");
        }
    }

    public IEnumerator Verify(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/verifyaccount.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Account has been activated");
            MenuManager.instance.OpenMenu("loggedin");
            
        }
    }


}
