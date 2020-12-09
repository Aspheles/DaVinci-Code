using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void OpenLoginMenu()
    {
        MenuManager.instance.OpenMenu("login");
    }

    public void OpenRegisterMenu()
    {
        MenuManager.instance.OpenMenu("register");
    }

    public void OpenMainMenu()
    {
        MenuManager.instance.OpenMenu("main");
    }

    public void OpenErrorMenu()
    {
        MenuManager.instance.OpenMenu("error");
    }

    public void OpenSuccessMenu()
    {
        MenuManager.instance.OpenMenu("success");
    }
}
