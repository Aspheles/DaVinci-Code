using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionTitle;
    public static QuestionManager instance;


    private void Start()
    {
        instance = this;
    }
}
