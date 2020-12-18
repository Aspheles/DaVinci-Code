using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Verification : MonoBehaviour
{
    public InputField emailInput;
    [SerializeField] InputField codeInput;
    public static Verification instance;

    private void Start()
    {
        instance = this;
    }
    public void Verify()
    {
        Debug.Log(DateTime.Parse(VerificationManager.instance.Data[1]));
        Debug.Log(DateTime.Now);

        if (codeInput.text == VerificationManager.instance.Data[0] && DateTime.Now <= DateTime.Parse(VerificationManager.instance.Data[1]))
        {
            Debug.Log("Account succesfully activated");
        }
        else if (codeInput.text != VerificationManager.instance.Data[0] && DateTime.Now <= DateTime.Parse(VerificationManager.instance.Data[1]))
        {
            Debug.Log("Token isn't valid");
        }
        else if (codeInput.text == VerificationManager.instance.Data[0] && DateTime.Now >= DateTime.Parse(VerificationManager.instance.Data[1])) 
        {
            Debug.Log("Code has been expired");
        }
    }

}
