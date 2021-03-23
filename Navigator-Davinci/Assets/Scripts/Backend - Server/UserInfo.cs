using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class UserInfo : MonoBehaviour
{
    public string id;
    public string email;
    public string username;
    public string class_code;
    public bool isverified = false;
    public bool isadmin = false;
    public static UserInfo instance;
    public string selectedDifficulty;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (GameObject.Find("LoggedInMenu") == isActiveAndEnabled)
        {
            GameObject.Find("user").GetComponent<Text>().text = "Welcome " + username;

        }

        if (GameObject.Find("Admin") == isActiveAndEnabled)
            GameObject.Find("Admin").SetActive(isadmin);
    }

    public void AssignUserData(JSONNode Data)
    {
        id = Data.AsObject["id"];
        username = Data.AsObject["username"];
        email = Data.AsObject["email"];
        class_code = Data.AsObject["classcode"];

        if (int.Parse(Data.AsObject["verified"]) == 0)
            isverified = false;
        else
            isverified = true;

        if (int.Parse(Data.AsObject["isadmin"]) == 0)
            isadmin = false;
        else
            isadmin = true;
    }
}
