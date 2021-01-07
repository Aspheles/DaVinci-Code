using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class VerificationManager : MonoBehaviour
{

    public string token;
    public byte[] results;
    public static VerificationManager instance;
    public string testData;
    public string[] Data;

   

    private void Start()
    {
        instance = this;
    }

    public IEnumerator GetToken(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/verification.php", formData);

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

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/resendcode.php", formData);

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
            MenuManager.instance.OpenMenu("verification");
        }
    }

    public IEnumerator Verify(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/verifyaccount.php", formData);

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
