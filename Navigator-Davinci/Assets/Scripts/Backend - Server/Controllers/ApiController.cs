using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class ApiController : MonoBehaviour
{
    public static ApiController instance;

    private void Awake()
    {
        instance = this;
    }

    public void CheckData(JSONNode Data, string type)
    {
        //Clearing session data
        Session.instance.message = "";

        switch (type)
        {
            case Request.CREATEPUZZLE:
                SavePuzzle(Data);
                break;
            case Request.EDITPUZZLE:
                EditPuzzle(Data);
                break;
            case Request.DELETEPUZZLE:
                DeletePuzzle();
                break;
            case Request.FETCHPUZZLES:
                FetchPuzzles(Data);
                break;
            case Request.CREATEQUESTION:
                SaveQuestion(Data);
                break;
            case Request.DELETEQUESTION:
                DeleteQuestion();
                break;
            case Request.FETCHQUESTIONS:
                FetchQuestions(Data);
                break;
            case Request.DELETEANSWER:
                DeleteAnswer();
                break;
            case Request.FETCHANSWERS:
                FetchAnswers(Data);
                break;
            case Request.CREATEANSWERS:
                SaveAnswers(Data);
                break;
            case Request.SAVEIMAGE:
                SaveImage();
                break;
            case Request.REGISTER:
                RegisterUser(Data);
                break;
            case Request.LOGIN:
                LoginUser(Data);
                break;
            case Request.VERIFY:
                VerifyAccount(Data);
                break;
            case Request.RESETPASSWORD:
                UpdatePassword(Data);
                break;
            case Request.RESENDCODE:
                ResendCode(Data);
                break;
            case Request.LOADPUZZLESDATA:
                LoadPuzzlesData(Data);
                break;
            case Request.LOADPUZZLEQUESTIONS:
                LoadPuzzleQuestions(Data);
                break;
            case Request.LOADGAMEQUESTIONANSWERS:
                LoadGameQuestionAnswers(Data);
                break;
            case Request.FINISHROOM:
                FinishRoom(Data);
                break;
            case Request.GETFINISHEDPUZZLES:
                GetFinishedPuzzles(Data);
                break;
            case Request.SENDMONEYTODB:
                SendMoneyToDB(Data);
                break;
            case Request.FETCHINGMONEY:
                FetchMoneyFromDB(Data);
                break;

            default:
                Debug.LogError("No Function assigned");
                break;
        }
    }

    private void FetchMoneyFromDB(JSONNode Data)
    {
        //Checking if user has money in db
        if (Data != null)
        {
            UserInfo.instance.currency = Data[0].AsObject["currency"];
            UserInfo.instance.fetchedMoney = true;
        }
    }

    private void SendMoneyToDB(JSONNode Data)
    {
        //Resetting fetched money so it can load the data again
        UserInfo.instance.fetchedMoney = false;

        Debug.Log("Money has been updated");

        //Reseting points for next room
        RunManager.instance.totalPoints = 0;
    }

    private void GetFinishedPuzzles(JSONNode Data)
    {
       
        RunManager.instance.completedPuzzles = new List<CompletedPuzzle>();

        for (int i = 0; i < Data.Count; i++)
        {

            int puzzleId = int.Parse(Data[i].AsObject["puzzleid"]);
            int accountid = int.Parse(Data[i].AsObject["accountid"]);
            int runid = int.Parse(Data[i].AsObject["runid"]);
               

            CompletedPuzzle puzzle = new CompletedPuzzle(puzzleId, accountid, runid);
            RunManager.instance.completedPuzzles.Add(puzzle);

        }

        
        //Reset terminals
        TerminalSpawnPoints.instance.ResetTerminals();
    }
    private void FinishRoom(JSONNode Data)
    {
        if(Data[0].AsObject["status"] == "success")
        {
            Debug.Log("Run has been saved with puzzles in database");
        }
        else
        {
            Debug.Log("This puzzle already has been saved");
        }
        RunManager.instance.NextRoom();
    }
    private void SaveImage()
    {
        print("Image has been saved");
    }

    private void SavePuzzle(JSONNode Data)
    {
        //Checking for error
        if (Data[1].AsObject["status"] == "success")
        {
            Session.instance.puzzle.id = Data[0].AsObject["id"];
            Session.instance.puzzle.name = Data[0].AsObject["name"];
            Session.instance.puzzle.difficulty = Data[0].AsObject["difficulty"];
            Session.instance.puzzle.description = Data[0].AsObject["description"];
            Session.instance.puzzle.creator = Data[0].AsObject["creator"];

            PuzzleManager.instance.ClearInputs();
            Launcher.instance.OpenPuzzleQuestionCreatorMenu();
            Session.instance.message = "Puzzle has been created";
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
        
    }

    private void EditPuzzle(JSONNode Data)
    {
        if(Data[1].AsObject["status"] == "success")
        {
            PuzzleOverview.instance.cover.SetActive(false);
            Launcher.instance.OpenAdminPuzzleOverviewMenu();

            if (PuzzleOverview.instance.puzzles.Count > 0)
            {
                PuzzleOverview.instance.LoadPuzzles();
            }

            Session.instance.message = "Puzzle has been edited";
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
        
    }

    /// <summary>
    /// Deletes chosen puzzle.
    /// </summary>
    /// <param name="puzzle"></param>
    private void DeletePuzzle()
    {
        Destroy(PuzzleOverview.instance.selectedPuzzle.puzzleObject);
        Session.instance.message = "Puzzle " + PuzzleOverview.instance.selectedPuzzle.name.text + " has been deleted";
    }

    private void FetchPuzzles(JSONNode Data)
    {
        PuzzleOverview.instance.puzzles = new List<PuzzleData>();

        for (int i = 0; i < Data.Count; i++)
        {
            
            int puzzleId = int.Parse(Data[i].AsObject["id"]);
            string puzzleName = Data[i].AsObject["name"];
            string puzzleDifficulty = Data[i].AsObject["difficulty"];
            string puzzleDescription = Data[i].AsObject["description"];
            string puzzleCreator = Data[i].AsObject["creator"];

            PuzzleData puzzle = new PuzzleData(puzzleId, puzzleName, puzzleDifficulty, puzzleDescription, puzzleCreator);
            PuzzleOverview.instance.puzzles.Add(puzzle);
           
        }

        if (PuzzleOverview.instance.puzzles.Count > 0)
        {
            PuzzleOverview.instance.LoadPuzzles();
        }
        
    }

    private void LoadPuzzlesData(JSONNode Data)
    {
        //Clearing puzzles
        if (RunManager.instance.puzzles.Count > 0)
        {

            RunManager.instance.puzzles.Clear();
            Debug.Log("Cleared all puzzles");
        }
        //Load in puzzles
        for (int i = 0; i < Data.Count; i++)
        {
            PuzzleData Puzzle = new PuzzleData(Data[i].AsObject["id"], Data[i].AsObject["name"], Data[i].AsObject["difficulty"], Data[i].AsObject["description"], Data[i].AsObject["creator"]);
            RunManager.instance.puzzles.Add(Puzzle);
        }

        
    }

    private void LoadPuzzleQuestions(JSONNode Data)
    {
        RunManager.instance.terminal.questions = new List<Question>();

        for (int i = 0; i < Data.Count; i++)
        {
            //Local variables
            string questionId = Data[i].AsObject["id"];
            string questionTitle = Data[i].AsObject["title"];
            string questionDescription = Data[i].AsObject["description"];
            //RawImage questionImage = Data[i].AsObject["image"] as RawImage;
            int puzzleid = Data[i].AsObject["puzzle_id"];

            Question _question = new Question(int.Parse(questionId), questionTitle, questionDescription, null, puzzleid);
            RunManager.instance.terminal.questions.Add(_question);

        }

        RunManager.instance.terminal.questionsLoaded = true;
        RunManager.instance.terminal.GetAnswers();

    }

    private void LoadGameQuestionAnswers(JSONNode Data)
    {
        List<Answer> answers = new List<Answer>();

        if (Data != null)
        {
            for (int i = 0; i < Data.Count; i++)
            {

                int answerID = int.Parse(Data[i].AsObject["id"]);
                string answerName = Data[i].AsObject["title"];
                string answerValue = Data[i].AsObject["value"];
                //string questionId = Data[i].AsObject["question_id"];
                bool answerbool;

                if (answerValue == "0") answerbool = false;
                else answerbool = true;

                answers.Add(new Answer(answerID, answerName, answerbool));

            }

            RunManager.instance.terminal.answers = answers;
            GameManager.instance.DisplayAnswers();
            print("Answers has been loaded");

        }
        else
        {
            print("There were no answers for this question");
        }
    }

    private void SaveQuestion(JSONNode Data)
    {
        
        if(Data != null)
        {
            print(Data);
            if (Data[1].AsObject["status"] == "success" || Data[0].AsObject["status"] == "success")
            {
                Session.instance.question.id = int.Parse(Data[0].AsObject["id"]);
                print("Question has been saved");
                //ry fnew Questions().SaveImage();
                new Answers().Create();
            }
            else
            {
                Session.instance.message = Data[1].AsObject["status"];
            }
        }


        Session.instance.message = "The question has been saved";
        //StartCoroutine(SaveAnswers());
    }

    private void DeleteQuestion()
    {
        Destroy(QuestionOverview.instance.questionObject);
        Session.instance.message = "Question has been deleted";
    }

    private void FetchQuestions(JSONNode Data)
    {
        QuestionOverview.instance.Questions = new List<Question>();

        for (int i = 0; i < Data.Count; i++)
        {
            //Local variables
            string questionId = Data[i].AsObject["id"];
            string questionTitle = Data[i].AsObject["title"];
            string questionDescription = Data[i].AsObject["description"];
            //RawImage questionImage = Data[i].AsObject["image"] as RawImage;
            int puzzleid = Data[i].AsObject["puzzle_id"];

            Question _question = new Question(int.Parse(questionId), questionTitle, questionDescription, null, puzzleid);
            QuestionOverview.instance.Questions.Add(_question);

        }

        //Clear the list for duplicates
        foreach (Transform child in QuestionOverview.instance.QuestionPositions)
        {
            Destroy(child.gameObject);
        }

        QuestionOverview.instance.LoadQuestions();
        print("Questions have been loaded");
        
    }

    private void DeleteAnswer()
    {
        Destroy(Session.instance.answerObject);
        Session.instance.message = "Answer has been deleted";
    }

    private void FetchAnswers(JSONNode Data)
    {
        List<Answer> answers = new List<Answer>();
        
        if(Data != null)
        {
            for (int i = 0; i < Data.Count; i++)
            {

                int answerID = int.Parse(Data[i].AsObject["id"]);
                string answerName = Data[i].AsObject["title"];
                string answerValue = Data[i].AsObject["value"];
                //string questionId = Data[i].AsObject["question_id"];
                bool answerbool;

                if (answerValue == "0") answerbool = false;
                else answerbool = true;

                answers.Add(new Answer(answerID, answerName, answerbool));

            }

            Session.instance.question.answers = answers;
            print("Answers has been loaded");
        }
        else
        {
            print("There were no answers for this question");
        }

        QuestionCreator.instance.LoadAnswers(answers);

    }


    private void SaveAnswers(JSONNode Data)
    {
        QuestionCreator.instance.loaded = false;
        //Session.instance.message = "Answers have been saved";
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    private void RegisterUser(JSONNode Data)
    {
        if (Data[1].AsObject["status"] == "success")
        {
           
            UserInfo.instance.AssignUserData(Data[0]);

            if (!string.IsNullOrEmpty(UserInfo.instance.email))
            {
                Session.instance.message = "Account has been created";
                Launcher.instance.OpenVerificationMenu();
            }
            else
            {
                Debug.Log("Couldn't find account");
            }
           
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
    }

    private void LoginUser(JSONNode Data)
    {
        if (Data[1].AsObject["status"] == "success")
        {
           
            UserInfo.instance.AssignUserData(Data[0]);

            if (UserInfo.instance.isverified)
            {
                Launcher.instance.OpenLoggedInMenu();
                Session.instance.message = "You have successfully logged in";
            }
            else
            {
                Launcher.instance.OpenVerificationMenu();
            }
        }
        else
        {
            if(Data[1].AsObject["status"] == null)
            {
                Session.instance.message = Data[0].AsObject["status"];
            }
            else
            {
                Session.instance.message = Data[1].AsObject["status"];
            }
        }
    }

    private void UpdatePassword(JSONNode Data)
    {
        if (Data[1].AsObject["status"] == "success")
        {
            Launcher.instance.OpenLoginMenu();
            Session.instance.message = "Your password has been updated";
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
    }

    private void ResendCode(JSONNode Data)
    {
        if (Data[1].AsObject["status"] == "success")
        {
            Session.instance.message = "Verification code has been sent to your email";
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
    }

    private void VerifyAccount(JSONNode Data)
    {
        if (Data[1].AsObject["status"] == "success")
        {
            //Send to loggedin menu
            //Verification.instance.message.color = Color.green;
            //Verification.instance.message.text = "Account activated";
            Session.instance.message = "Your account has been activated";
            Launcher.instance.OpenLoggedInMenu();
        }
        else
        {
            Session.instance.message = Data[1].AsObject["status"];
        }
    }
}
