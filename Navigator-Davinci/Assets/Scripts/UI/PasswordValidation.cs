using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using SimpleJSON;

public class PasswordValidation : MonoBehaviour
{
    public InputField repeatpassword;
    public InputField newpassword;
    public InputField authcode;
    public Text message;
    public InputField email;
    EventSystem system;
    bool emailCheck = false;
    public bool repeatpasswordCheck;
    public bool newpasswordCheck;
    public static PasswordValidation instance;

    private void Start()
    {
        system = EventSystem.current;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }

        
    }

    /// <summary>
    /// Checks if the input is according to the conditions, if so, enable the check. otherwise error is shown.
    /// </summary>
    public void EmailValidator()
    {
        if (email.text.IndexOf("@mydavinci.nl") <= 0)
        {
            message.color = Color.red;
            message.text = "Email needs to be valid";
            email.Select();
            emailCheck = false;
        }
        else
        {
            message.text = "";
            emailCheck = true;
        }
    }

    /// <summary>
    /// checks if the input field have been filled in to the conditions otherwise show an error
    /// checks whether both the passwords have the exact same input and if they passwords are not too short.
    /// </summary>
    public void NewPasswordValidator()
    {
        if (repeatpassword.text == newpassword.text)
        {
            if (repeatpassword.text.Length > 5 && newpassword.text.Length > 5)
            {
                message.text = "";
                repeatpasswordCheck = true;
                newpasswordCheck = true;
            }
            else
            {
                message.color = Color.red;
                message.text = "Your password needs to be bigger than 5 characters";
                repeatpasswordCheck = false;
                newpasswordCheck = false;
            }
        }
        else
        {
            message.color = Color.red;
            message.text = "Your passwords do not match";
            repeatpasswordCheck = false;
            newpasswordCheck = false;
        }
    }

    public void ClearData()
    {
        newpassword.text = string.Empty;
        repeatpassword.text = string.Empty;
        message.text = string.Empty;
        newpasswordCheck = false;
        repeatpasswordCheck = false;
        emailCheck = false;
    }

    /// <summary>
    /// when clicked it opens the ResetPassword menu
    /// first it checks whether you filled in your email correctly or not
    /// if not you get an error and if it is correct it will fire the verification code function
    /// this is where you fill in your verification code and the new password you want to use
    /// </summary>
    public void OnPasswordSubmitButtonClicked()
    {
        if (emailCheck)
        {
            Session.instance.email = email.text;
            SendVerification(email.text);
            Launcher.instance.OpenResetPasswordMenu();
            message.color = Color.green;
            message.text = "A verification code has been sent to your emailadress";

        }
        else
        {
            message.color = Color.red;
            message.text = "Your Email adress needs to be valid";
        }
    }


    /// <summary>
    /// calls the function to resend the verification code
    /// </summary>
    /// <param name="email"></param>
    public void SendVerification(string email)
    {
        StartCoroutine(VerificationManager.instance.ResendCode(email));
    }


    /// <summary>
    /// when clicked it will fire the function to send a verification code to your email
    /// this new code can be used to verify your account or new password
    /// </summary>
    /// <param name="email"></param>
    public void OnPasswordResendCodeButtonClicked()
    {
        StartCoroutine(VerificationManager.instance.ResendCode(Session.instance.email));
    }

    /// <summary>
    /// Updates the password of a user when the button is clicked.
    /// checks if the input has some input, if so it will start to check whether the input fills the requirements
    /// If that's the case it will check whether the two filled in passwords match
    /// If so, the function will be executed.
    /// If any of the steps fail, show corresponding error
    /// ** Moet nog aangevuld worden met authenticatie met de reset code die vanaf de DB komt **
    /// </summary>
    public void OnPasswordResetButtonClicked()
    {
        if (newpasswordCheck && repeatpasswordCheck)
        {
            StartCoroutine(VerificationManager.instance.ResetPassword(Session.instance.email, newpassword.text, repeatpassword.text, authcode.text));
            new User().UpdatePassword(false);
            ClearData();
        }
        else
        {
            message.color = Color.red;
            message.text = "your verification code or passwords have been filled in invalid";
        }
    }
}
