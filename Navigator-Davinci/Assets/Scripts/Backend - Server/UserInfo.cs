using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

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
    public int currency;
    public PlayerManager playerManager;
    public bool fetchedMoney = false;
    public bool fetchedUpgrades = false;
    

    private void Start()
    {
        instance = this;
        playerManager = GetComponent<PlayerManager>();
        playerManager.LoadUpgrades();
    }

    private void Update()
    {
        if (!fetchedMoney && !string.IsNullOrEmpty(id))
        {
            playerManager.FetchCurrencyFromDB(id);
        }

        if (!fetchedUpgrades)
        {
            playerManager.CheckUpgrdes(id);
            fetchedUpgrades = true;
        }

        if (GameObject.FindGameObjectWithTag("currency") != null)
        {
            GameObject.FindGameObjectWithTag("currency").GetComponent<TMP_Text>().text = currency.ToString();
        }



        if (GameObject.Find("LoggedInMenu") == isActiveAndEnabled)
        {          
            GameObject.Find("user").GetComponent<Text>().text = "Welcome " + username;

            if (!fetchedUpgrades && !string.IsNullOrEmpty(id))
            {
                playerManager.CheckUpgrdes(id);
                fetchedUpgrades = true;
            }


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
