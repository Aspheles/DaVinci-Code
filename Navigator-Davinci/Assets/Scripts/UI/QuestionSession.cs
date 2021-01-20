using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSession : MonoBehaviour
{
    public Puzzle puzzle;
    public Question question;
    public List<Question> allQuestions;
    public static QuestionSession instance;



    void Start()
    {
        instance = this;

       
    }


}
