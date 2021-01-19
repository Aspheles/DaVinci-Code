using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Authentication : MonoBehaviour
{
    public static Authentication instance;
    public string message;

    private void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Sends a login request to the backend, with the credentials.
    /// Checks if the a connection with the server is established.
    /// Checks if the user is verified, if so user will be logged in, if not user can verify himself.
    /// If requests fails error will be displayed on the UI
    /// </summary>
    /// <param name="email">Inputfield for the mail</param>
    /// <param name="password">Inputfield for the password</param>
    /// <returns>
    /// Sends back the status code for the request.
    /// </returns>
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
            byte[] dbData = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(dbData);

            if (!Result.Contains("Error"))
            {
               
                JSONArray jsonData = JSON.Parse(Result) as JSONArray;

                for (int i = 0; i < jsonData.Count; i++)
                {
                    //Debug.Log(jsonData[i].AsObject["id"]);
                    //SaveData(jsonData[i]);
                    UserInfo.instance.AssignUserData(jsonData[i]);
                }

                if (UserInfo.instance.isverified)
                {
                    Launcher.instance.OpenLoggedInMenu();
                }
                else
                {
                    Launcher.instance.OpenVerificationMenu();
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

    /// <summary>
    /// Sends a register request to the backend, with the credentials.
    /// Checks if the a connection with the server is established.
    /// Checks if the user is verified, if so user will be registered, if not user can verify himself.
    /// If requests fails error will be displayed on the UI
    /// </summary>
    /// <param name="email">Inputfield for the mail</param>
    /// <param name="password">Inputfield for the password</param>
    /// <param name="username">Inputfield for the username</param>
    /// <param name="classCode">Inputfield for the class</param>
    /// <returns>
    /// Sends back the status code for the request.
    /// </returns>
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
           
            byte[] dbData = www.downloadHandler.data;
            string Result = System.Text.Encoding.Default.GetString(dbData);

            if (!Result.Contains("Error"))
            {

                JSONArray jsonData = JSON.Parse(Result) as JSONArray;

                for (int i = 0; i < jsonData.Count; i++)
                {
                    //Debug.Log(jsonData[i].AsObject["id"]);
                    //SaveData(jsonData[i]);
                    UserInfo.instance.AssignUserData(jsonData[i]);
                }

                FormValidation.instance.message.color = Color.green;
                FormValidation.instance.message.text = "Account has been created";
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

    
    /// <summary>
    /// Logged out the user
    /// </summary>
    public void Logout()
    {
        ResetData();
        Launcher.instance.OpenMainMenu();
    }


    /// <summary>
    /// Empties the verification in the manager from a certain user.
    /// </summary>
    public void ResetData()
    {
        UserInfo.instance.id = string.Empty;
        UserInfo.instance.username = string.Empty;
        UserInfo.instance.email = string.Empty;
        UserInfo.instance.isadmin = false;
        UserInfo.instance.class_code = string.Empty;
        UserInfo.instance.isverified = false;
    }
}
