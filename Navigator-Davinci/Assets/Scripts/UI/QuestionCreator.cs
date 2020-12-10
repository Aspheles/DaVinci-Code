using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionCreator : MonoBehaviour
{
    [SerializeField] TMP_InputField question;
    [SerializeField] TMP_Dropdown answersAmount;
    [SerializeField] TMP_InputField answerInput;
    [SerializeField] TMP_Dropdown answerValue;
    [SerializeField] Transform answerOptionsPos;
    //private List<>


    private void Start()
    {
        answersAmount.ClearOptions();
        for(int i = 1; i < 6; i++)
        {
            answersAmount.AddOptions(new List<string> {i.ToString()});
        }
        CreateAnswerOptions();
    }


    

    public void CreateAnswerOptions()
    {
        for (int i = 0; i < 2 ; i++)
        {
            TMP_InputField answerClone = Instantiate(answerInput, answerOptionsPos.position, Quaternion.identity);
            answerClone.transform.SetParent(answerOptionsPos);

        }
    }

}
