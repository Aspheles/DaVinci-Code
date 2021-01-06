using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSession : MonoBehaviour
{

    public Question question;
    public static QuestionSession instance;



    void Start()
    {
        instance = this;

       
    }


}
