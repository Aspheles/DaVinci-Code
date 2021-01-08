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

        if (VerificationManager.instance.email != null) emailInput.text = VerificationManager.instance.email;
    }
    public void Verify()
    {
        Debug.Log(DateTime.Now);

        if (VerificationManager.instance.verified == 0) // Checking if account isn't verified yet
        {
            if (codeInput.text == VerificationManager.instance.token && DateTime.Now <= DateTime.Parse(VerificationManager.instance.expiredate))
            {
                Debug.Log("Account succesfully activated");
                VerificationManager.instance.VerifyAccount(emailInput.text);
            }
            else if (codeInput.text != VerificationManager.instance.token && DateTime.Now <= DateTime.Parse(VerificationManager.instance.expiredate))
            {
                Debug.Log("Token isn't valid");
            }
            else if (codeInput.text == VerificationManager.instance.token && DateTime.Now >= DateTime.Parse(VerificationManager.instance.expiredate))
            {
                Debug.Log("Code has been expired");
            }
        }
        else
        {
            Debug.Log(emailInput.text + " is already activated");
        }
        
    }

    public void CheckVerification()
    {
        StartCoroutine(VerificationManager.instance.GetToken(emailInput.text));
    }

}
