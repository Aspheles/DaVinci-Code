using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.Networking;


public class Authentication : MonoBehaviour
{
    public static Authentication instance;

    private void Start()
    {
        instance = this;
    }
    public void Login(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request, result => {
            Debug.Log("User " + request.Email + " has logged in");
            StartCoroutine(VerificationManager.instance.GetToken(email));
        
        }, error => {
            Debug.Log(error.GenerateErrorReport());
            FormValidation.instance.message.color = Color.red;
            FormValidation.instance.message.text = "Email and password do not match";
        });

        StartCoroutine(Post(new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("password", password),               
        }, "http://localhost/sqlconnect/login.php"));

    }

    public void Register(string username, string email, string password, string classCode)
    {
       
        StartCoroutine(Post(new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password),
            new MultipartFormDataSection("classcode", classCode)
        }, "http://localhost/sqlconnect/register.php"));



        if (VerificationManager.instance.testData == "success")
        {
            Debug.Log("Registartion was succesfull");
            Debug.Log("User " + username + " Created");
            FormValidation.instance.message.color = Color.green;
            FormValidation.instance.message.text = "User " + username + " Created";
            FormValidation.instance.ClearData();
        }
        else
        {
            Debug.Log(VerificationManager.instance.testData);
            FormValidation.instance.message.color = Color.red;
            FormValidation.instance.message.text = VerificationManager.instance.testData;
        }

    }

    public IEnumerator Post(List<IMultipartFormSection> formData, string target)
    {
        UnityWebRequest www = UnityWebRequest.Post(target, formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.data);
            byte[] Data = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(Data);

            VerificationManager.instance.testData = Result;
        }
    }

    public IEnumerator Get(string target)
    {
        UnityWebRequest www = UnityWebRequest.Get(target);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.data);
            byte[] Data = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(Data);

            VerificationManager.instance.testData = Result;
        }
    }

    

    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        MenuManager.instance.OpenMenu("main");
    }

}
