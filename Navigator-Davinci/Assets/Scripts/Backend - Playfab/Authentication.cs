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
            Launcher.instance.OpenLoggedInMenu();
        
        }, error => {
            Debug.Log(error.GenerateErrorReport());
            FormValidation.instance.message.color = Color.red;
            FormValidation.instance.message.text = "Email and password do not match";
        });

    }

    public void Register(string username, string email, string password)
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Email = email, Password = password };
        PlayFabClientAPI.RegisterPlayFabUser(request, 
        result => {
            FormValidation.instance.ClearData();
            Debug.Log("User " + result.Username + " Created");
            FormValidation.instance.message.color = Color.green;
            FormValidation.instance.message.text = "User " + result.Username + " Created";
            StartCoroutine(RegisterToDatabase(email, username, password));
           
        }, 
        error => {
            Debug.Log(error.GenerateErrorReport());
            FormValidation.instance.message.color = Color.red;
            FormValidation.instance.message.text = error.GenerateErrorReport();
        });

       
    }

    IEnumerator RegisterToDatabase(string email, string username, string password)
    {
        //Creates a list for the data so it can be sent to the PHP file can get it trough $_POST
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password)
        };
        //formData.Add(new MultipartFormFileSection(email, "my file data"));

        //Sending the data 
        //UnityWebRequest www = UnityWebRequest.Post("https://davincicodeproject.000webhostapp.com/register.php", formData);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", formData);



        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            StartCoroutine(VerificationManager.instance.GetToken(email));
        }
    }

    

    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        MenuManager.instance.OpenMenu("main");
    }

}
