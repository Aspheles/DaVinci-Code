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
    //public string token;
    //public int verified;
    //public string expiredate;


   
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
            
            //MenuManager.instance.OpenMenu("verification");
        }
    }

    public IEnumerator Verify(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("code", Verification.instance.codeInput.text)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/verification.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text.Contains("Success"))
            {
                Verification.instance.message.color = Color.green;
                Verification.instance.message.text = "Account activated";
                Launcher.instance.OpenLoggedInMenu();
            }
            else
            {
                Verification.instance.message.color = Color.red;
                Verification.instance.message.text = www.downloadHandler.text;
            }
            
        }
    }


}
