using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] InputField name;
    [SerializeField] TMP_Dropdown difficulty;
    [SerializeField] Text message;


    public void OnNextClicked()
    {
        if(!string.IsNullOrEmpty(name.text) && !string.IsNullOrEmpty(difficulty.options[difficulty.value].text))
        {
            StartCoroutine(CreatePuzzle(name.text, difficulty.options[difficulty.value].text));
        }
        else
        {
            message.color = Color.red;
            message.text = "Puzzle name can't be empty";
        }
        

        
    }

   
    IEnumerator CreatePuzzle(string name, string difficulty)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("name", name),
            new MultipartFormDataSection("difficulty", difficulty)
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
            if (www.downloadHandler.text.Contains("Success"))
            {
                byte[] dbData = www.downloadHandler.data;
                string Result = System.Text.Encoding.Default.GetString(dbData);
                string[] Data = Result.Split("b"[0]);

                message.color = Color.green;
                message.text = "Puzzle: " + Data[2] + " Has been created";
                QuestionSession.instance.puzzle.id = int.Parse(Data[1]);
                QuestionSession.instance.puzzle.name = Data[2];
                SelectDifficulty(Data[3]);
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
            case "easy":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.easy;
                break;
            case "medium":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.medium;
                break;
            case "hard":
                QuestionSession.instance.puzzle.difficulty = PuzzleDifficulty.hard;
                break;
        }
    }
}
