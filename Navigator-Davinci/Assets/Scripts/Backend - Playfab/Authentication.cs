using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class Authentication : MonoBehaviour
{
    public static Authentication instance;

    private void Start()
    {
        instance = this;
    }
    public void Login(string username, string email, string password)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request, result => {
            Debug.Log("User " + request.Email + " has logged in");
        
        }, error => {
            Debug.Log(error.GenerateErrorReport());
        
        });

    }

    public void Register(string username, string email, string password)
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Email = email, Password = password };
        PlayFabClientAPI.RegisterPlayFabUser(request, 
        result => {
            Debug.Log("User " + result.Username + " Created");
            FormValidation.instance.message.color = Color.green;
            FormValidation.instance.message.text = "User " + result.Username + " Created";
        }, 
        error => {
            Debug.Log(error.GenerateErrorReport());
            FormValidation.instance.message.color = Color.red;
            FormValidation.instance.message.text = error.GenerateErrorReport();
        });

       
    }

}
