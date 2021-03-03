using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class VerificationManager : MonoBehaviour
{
    public static VerificationManager instance;




   
    private void Start()
    {
        instance = this;
    }


    private void Update()
    {
        

    }

    /// <summary>
    /// Creates new verification code for the user.
    /// </summary>
    public void RequestNewCode()
    {
        //StartCoroutine(ResendCode(Verification.instance.emailInput.text));
        //Passing for resend code in verify
        new User().Verify(true);
    }

    /// <summary>
    /// Verifies the user account.
    /// </summary>
    /// <param name="email"></param>
    public void VerifyAccount(string email)
    {
        StartCoroutine(Verify(email));
    }

    /// <summary>
    /// Executes the backend code to resend the verification code.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public IEnumerator ResendCode(string email)
    {
        Debug.Log(email);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/resendcode.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            PasswordValidation.instance.message.text = "A new code has been sent to your email adress";
            PasswordValidation.instance.message.color = Color.green;
        }
        else
        {
            PasswordValidation.instance.message.color = Color.red;
            PasswordValidation.instance.message.text = www.downloadHandler.text;

        }
    }

    /// <summary>
    /// Checks if the verification code is correct.
    /// Sends verification request to the backed.
    /// If the verification code is correct, user will be sent to the logged in page.
    /// </summary>
    /// <param name="email"></param>
    /// <returns>
    /// Sends back the status code for the request.
    /// </returns>
    public IEnumerator Verify(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("code", Verification.instance.codeInput.text)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/verification.php", formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text.Contains("Success"))
            {
                Verification.instance.message.color = Color.green;
                Verification.instance.message.text = "Account activated";
                Launcher.instance.OpenLoginMenu();
            }
            else
            {
                Verification.instance.message.color = Color.red;
                Verification.instance.message.text = www.downloadHandler.text;
            }
            
        }
    }

    public IEnumerator ResetPassword(string email, string newpassword, string repeatpassword, string authcode)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email),
            new MultipartFormDataSection("password", newpassword),
            new MultipartFormDataSection("newpassword", repeatpassword),
            new MultipartFormDataSection("verificationcode", authcode)
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/updatepassword.php", formData);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text.Contains("Success"))
            {
                Launcher.instance.OpenLoginMenu();
                PasswordValidation.instance.message.text = www.downloadHandler.text;
            }
            else
            {
                PasswordValidation.instance.message.color = Color.red;
                PasswordValidation.instance.message.text = www.downloadHandler.text;
            }
        }
    }



   

}
