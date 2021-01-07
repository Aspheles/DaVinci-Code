using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    public InputField answerField;
    public Toggle correctToggle;
    public static AnswerManager instance;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        

 
    }

   

    public void DeleteAnswerOption()
    {
        Destroy(this.gameObject);
    }
}
