﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Verification : MonoBehaviour
{
    public InputField emailInput;
    public InputField codeInput;
    public Text message;
    public static Verification instance;

    private void Start()
    {
        instance = this;

        if (VerificationManager.instance.email != null) emailInput.text = VerificationManager.instance.email;
    }


    public void CheckVerification()
    {
        StartCoroutine(VerificationManager.instance.Verify(emailInput.text));
    }

}