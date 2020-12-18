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

        if(int.Parse(VerificationManager.instance.Data[2]) == 0) // Checking if account isn't verified yet
        {
            if (codeInput.text == VerificationManager.instance.Data[0] && DateTime.Now <= DateTime.Parse(VerificationManager.instance.Data[1]))
            {
                Debug.Log("Account succesfully activated");
                VerificationManager.instance.VerifyAccount(emailInput.text);
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
        else
        {
            Debug.Log(emailInput.text + " is already activated");
        }
        
    }

}
