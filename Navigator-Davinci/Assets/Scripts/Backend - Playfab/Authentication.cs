using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class Authentication : MonoBehaviour
{

    private void Start()
    {
        Login();
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = "yavuzkaand@hotmail.com", Password = "poker12345" };
        PlayFabClientAPI.LoginWithEmailAddress(request, result => {
            Debug.Log("User " + request.Email + " has logged in");
        
        }, error => {
            Debug.Log(error.GenerateErrorReport());
        
        });

    }
}
