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
            if (isCorrect)
            {
                RunManager.instance.points++;
            }

            Debug.Log("Puzzle has been finished");
            RunManager.instance.terminal.answeredCorrect = RunManager.instance.points;
            RunManager.instance.FinishPuzzle();
            RunManager.instance.ClosePuzzle();

            Launcher.instance.OpenResult();

            RunManager.instance.result.ShowResult(RunManager.instance.terminal.questions.Count, RunManager.instance.terminal.answeredCorrect);
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
