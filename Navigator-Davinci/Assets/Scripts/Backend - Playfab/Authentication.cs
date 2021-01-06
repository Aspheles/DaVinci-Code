using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.Networking;


public class Authentication : MonoBehaviour
{
    public static Authentication instance;
    public string message;

    private void Start()
    {
        instance = this;
    }
    public IEnumerator Login(string email, string password)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),           
            new MultipartFormDataSection("password", password)         
        };

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);

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

            if(Result == "Success")
            {
                FormValidation.instance.message.color = Color.green;
                FormValidation.instance.message.text = Result;
            }
            else
            {
                FormValidation.instance.message.text = Result;
                Debug.Log("Username/Password is incorrect");
            }
        }


    }

    public IEnumerator Register(string username, string email, string password, string classCode)
    {

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password),
            new MultipartFormDataSection("classcode", classCode)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form);

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

            if (Result == "Success")
            {
                FormValidation.instance.message.text = "Registration has been succesfull";
            }
            else
            {
                //Check what the issue is from the backend
                if(Result == "Error") FormValidation.instance.message.text = "Email is already in use";
                //FormValidation.instance.message.text = "Username/Password is incorrect";
                //Debug.Log("Username/Password is incorrect");
            }
        }
    }

    

    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        MenuManager.instance.OpenMenu("main");
    }

}
