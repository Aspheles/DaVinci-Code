using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        if(Session.instance.question != null)
        {
            new Answers().Load();
        }
        
        
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

    public void CloseConfirmMenu()
    {
        MenuManager.instance.OpenMenu("close");
    }

    public void OpenConfirmRunMenu()
    {
        MenuManager.instance.OpenMenu("confirmrun");
        Portal.instance.CancelNow();
    }

    public void OpenGameOverMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MenuManager.instance.OpenMenu("gameover");
    }

    public void StartGame()
    {
        if(UserInfo.instance.email != null)
        {
            //Starting Game
            SceneManager.LoadScene(1);
            DontDestroyOnLoad(UserInfo.instance);
        }
    }

    public void LoadRoom()
    {
        SceneManager.LoadScene(2);
        //DontDestroyOnLoad(UserInfo.instance);
    }

    public void OpenGamePuzzle()
    {
        MenuManager.instance.OpenMenu("gamepuzzle");
        Cursor.visible = true;
    }

    public void OpenResult()
    {
        GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>().text = "";
        GameCamera.instance.canMove = false;
        Player.instance.canwalk = false;
        MenuManager.instance.OpenMenu("result");
    }
}
