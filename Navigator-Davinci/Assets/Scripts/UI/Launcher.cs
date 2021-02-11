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
        //Authentication.instance.ResetData();
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

    public void OpenPuzzleQuestionsOverviewMenu()
    {
        MenuManager.instance.OpenMenu("questionoverview");
        //StartCoroutine(QuestionOverview.instance.FetchQuestionsData());
        new Questions().Load();
    }

    public void OpenVerificationMenu()
    {
        MenuManager.instance.OpenMenu("verification");
    }

    public void OpenAdminPuzzleOverviewMenu()
    {
        MenuManager.instance.OpenMenu("adminpuzzleoverview");
        //StartCoroutine(PuzzleOverview.instance.FetchPuzzles());
        new Puzzles().Load();
    }

    public void OpenPreRunMenu()
    {
        MenuManager.instance.OpenMenu("prerun");
    }

    public void CheckValues()
    {
        Debug.Log("Value changed");
    }

    public void OpenResetPasswordMenu()
    {
        MenuManager.instance.OpenMenu("resetpassword");
    }

    public void OpenSubmitMenu() 
    {
        MenuManager.instance.OpenMenu("submit");    
    }
}
