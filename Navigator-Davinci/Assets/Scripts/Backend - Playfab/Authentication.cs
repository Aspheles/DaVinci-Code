using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.Networking;
using System;

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

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/login.php", form);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.data);
            byte[] dbData = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(dbData);

       
            if(Result.Contains("Success"))
            {
                

                string[] Data = Result.Split("b"[0]);
                SaveData(Data);

                //Checking if account isn't verified
                if (int.Parse(Data[3]) == 0)
                {
                    Launcher.instance.OpenVerificationMenu();
                }
                else
                {

                    Launcher.instance.OpenLoggedInMenu();
                }

            }
            else
            {
                FormValidation.instance.message.color = Color.red;
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

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/register.php", form);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.data);
            byte[] dbData = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(dbData);

            if (Result.Contains("Success"))
            {
               

                FormValidation.instance.message.color = Color.green;
                FormValidation.instance.message.text = "Account has been created";
               
                string[] Data = Result.Split("b"[0]);
                SaveData(Data);

                Launcher.instance.OpenVerificationMenu();

            }
            else
            {

                //Check what the issue is from the backend
                if (Result == "Error") FormValidation.instance.message.text = "Email is already in use";
                //FormValidation.instance.message.text = "Username/Password is incorrect";
                //Debug.Log("Username/Password is incorrect");
            }
        }
    }

    

    public void Logout()
    {
        Launcher.instance.OpenMainMenu();
    }


    public void SaveData(string[] Data)
    {
        VerificationManager.instance.username = Data[1];
        VerificationManager.instance.email = Data[2];
    }

    public void ResetData()
    {
        VerificationManager.instance.username = string.Empty;
        VerificationManager.instance.email = string.Empty;
    }

}
