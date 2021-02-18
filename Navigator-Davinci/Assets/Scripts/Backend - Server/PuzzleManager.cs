using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;

public class PuzzleManager : MonoBehaviour
{
    public InputField puzzleName;
    public TMP_InputField description;
    public TMP_Dropdown difficulty;
    public Text message;
    public static PuzzleManager instance;


    private void Awake()
    {
       
        instance = this;
    }

    private void Update()
    {
        if (Session.instance.message != null)
        {
            message.color = Color.red;
            message.text = Session.instance.ErrorHandling();
        }
    }

    public void OnNextClicked()
    {
        if(!string.IsNullOrEmpty(puzzleName.text) && !string.IsNullOrEmpty(description.text))
        {
            //StartCoroutine(CreatePuzzle(name.text, difficulty.options[difficulty.value].text, description.text, UserInfo.instance.username));
            Puzzles puzzleData = new Puzzles();
            puzzleData.Create();
            
            

        }
        else
        {
            message.color = Color.red;
            message.text = "Inputs can't be empty";
        }
        
       
            
    }


    public void ClearInputs()
    {
        puzzleName.text = "";
        description.text = "";
        message.text = "";
    }

}
