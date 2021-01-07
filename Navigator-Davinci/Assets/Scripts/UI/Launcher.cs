using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;

    private void Start()
    {
        instance = this;
    }

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

    public void OpenLoggedInMenu()
    {
        MenuManager.instance.OpenMenu("loggedin");
    }

    public void OpenControlsMenu()
    {
        MenuManager.instance.OpenMenu("controls");
    }

    public void OpenPuzzleCreatorMenu()
    {
        MenuManager.instance.OpenMenu("puzzle");
    }

    public void OpenPuzzleQuestionCreatorMenu()
    {
        MenuManager.instance.OpenMenu("question");
    }

    public void OpenVerificationMenu()
    {
        MenuManager.instance.OpenMenu("verification");
    }

    public void CheckValues()
    {
        Debug.Log("Value changed");
    }
}
