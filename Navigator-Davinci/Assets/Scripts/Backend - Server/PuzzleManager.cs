using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] InputField name;
    [SerializeField] TMP_InputField description;
    [SerializeField] TMP_Dropdown difficulty;
    [SerializeField] Text message;


    public void OnNextClicked()
    {
        if(!string.IsNullOrEmpty(name.text) && !string.IsNullOrEmpty(description.text))
        {
            StartCoroutine(CreatePuzzle(name.text, difficulty.options[difficulty.value].text, description.text, UserInfo.instance.username));
        }
        else
        {
            message.color = Color.red;
            message.text = "Inputs can't be empty";
        }
        

        
    }

   
    IEnumerator CreatePuzzle(string name, string difficulty, string description, string creator)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("name", name),
            new MultipartFormDataSection("difficulty", difficulty),
            new MultipartFormDataSection("description", description),
            new MultipartFormDataSection("creator", creator)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/createpuzzle.php", form);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            if (!www.downloadHandler.text.Contains("Error"))
            {
                byte[] dbData = www.downloadHandler.data;
                string Result = System.Text.Encoding.Default.GetString(dbData);
                //string[] Data = Result.Split("b"[0]);

                JSONArray Data = JSON.Parse(Result) as JSONArray;

                message.color = Color.green;
                for(int i = 0; i < Data.Count; i++)
                {
                    QuestionSession.instance.puzzle.id = Data[i].AsObject["id"];
                    QuestionSession.instance.puzzle.name = Data[i].AsObject["name"];
                    //SelectDifficulty(Data[i].AsObject["difficulty"]);

                    message.text = "Puzzle: " + Data[i].AsObject["name"] + " Has been created";

                }

                Launcher.instance.OpenPuzzleQuestionsOverviewMenu();

            }
            else
            {
                message.color = Color.red;
                message.text = www.downloadHandler.text;
            }
            

            
        }
    }

    void SelectDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            /*
            case "easy":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.easy;
                break;
            case "medium":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.medium;
                break;
            case "hard":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.hard;
                break;
            */
        }
    }
}
