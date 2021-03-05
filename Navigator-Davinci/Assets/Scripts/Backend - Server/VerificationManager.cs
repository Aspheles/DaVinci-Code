using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class VerificationManager : MonoBehaviour
{
    public static VerificationManager instance;
    public User user = new User();

    private void Start()
    {
        instance = this;
        
    }

    /// <summary>
    /// Creates new verification code for the user.
    /// </summary>
    public void RequestNewCode()
    {
        //StartCoroutine(ResendCode(Verification.instance.emailInput.text));
        //Passing for resend code in verify
        //new User().Verify(true);
        user.Verify(true);
    }

    /// <summary>
    /// Verifies the user account.
    /// </summary>
    /// <param name="email"></param>
    public void VerifyAccount()
    {
  
        //StartCoroutine(Verify(email));
        user.Verify(false);
    }

    
}
