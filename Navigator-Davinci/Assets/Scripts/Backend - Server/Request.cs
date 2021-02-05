using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Request
{
    public const string server = "http://davinci-code.nl";

    public const string
        LOGIN = server + "/login.php",
        REGISTER = server + "/register.php",
        DELETEANSWER = server + "/deleteanswer.php",
        CREATEPUZZLE = server + "/createpuzzle.php",
        EDITPUZZLE = server + "/editpuzzle.php",
        DELETEPUZZLE = server + "/deletepuzzle.php",
        FETCHPUZZLES = server + "/fetchpuzzles.php",
        CREATEQUESTION = server + "/savequestion.php",
        DELETEQUESTION = server + "/deletequestion.php",
        FETCHQUESTIONS = server + "/fetchquestions.php";
    
}
