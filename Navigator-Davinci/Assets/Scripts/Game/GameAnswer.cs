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
            RunManager.instance.ShowResult();
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
