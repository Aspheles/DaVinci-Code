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


        }
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
            print("Puzzle has been saved");
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

            print("Puzzle has been edited");
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

    private void SaveQuestion(JSONNode Data)
    {
        
        if(Data != null)
        {
            print(Data);
            if (Data[1].AsObject["status"] == "success" || Data[0].AsObject["status"] == "success")
            {
                Session.instance.question.id = int.Parse(Data[0].AsObject["id"]);
                print("Question has been saved");
                new Questions().SaveImage();
                new Answers().Create();
            }
            else
            {
                Session.instance.message = Data[1].AsObject["status"];
            }
        }
        

        
        //StartCoroutine(SaveAnswers());
    }

    private void DeleteQuestion()
    {
        Destroy(QuestionOverview.instance.questionObject);
        print("Question Has been deleted");
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

            Question _question = new Question(int.Parse(questionId), questionTitle, questionDescription, null, null, puzzleid);
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
        print("Answer has been deleted");
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
        print("Answers have been saved");
        print(Data);
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();

        int count = 0;
        count++;
    }
}
