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
    /// Logged out the user
    /// </summary>
    public void Logout()
    {
        ResetData();
        Launcher.instance.OpenMainMenu();
        Session.instance.message = "You have been successfully logged out";
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
