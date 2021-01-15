using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class UserInfo : MonoBehaviour
{
    public string id;
    public string email;
    public string username;
    public string class_code;
    public bool isadmin = false;
    public static UserInfo instance;

    private void Start()
    {
        instance = this;
    }

    public void GetData(JSONNode Data)
    {
        id = Data.AsObject["id"];
        username = Data.AsObject["username"];
        email = Data.AsObject["email"];
        class_code = Data.AsObject["class"];
        if (int.Parse(Data.AsObject["isadmin"]) == 0)
            isadmin = false;
        else
            isadmin = true;
    }
}
