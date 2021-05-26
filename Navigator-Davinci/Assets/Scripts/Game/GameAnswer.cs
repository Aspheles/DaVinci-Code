using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameAnswer : MonoBehaviour
{
    public int id;
    public TMP_Text answer;
    public bool isCorrect;
    
    public void OnAnswerClicked()
    {

        if (RunManager.instance.terminal.questionNumber >= RunManager.instance.terminal.questions.Count -1)
        {
            Debug.Log("Puzzle has been finished");
            RunManager.instance.terminal.answeredCorrect = RunManager.instance.points;
            RunManager.instance.FinishPuzzle();
            RunManager.instance.ClosePuzzle();
        }
        else
        { 
            if (isCorrect)
            {
                RunManager.instance.points++;
            }
            RunManager.instance.terminal.questionNumber++;
            RunManager.instance.terminal.GetAnswers();
        }

    }


}
