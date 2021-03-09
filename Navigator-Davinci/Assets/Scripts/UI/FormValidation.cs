using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FormValidation : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField email;
    public TMP_Dropdown classCode;
    public Text message;
    EventSystem system;
    public static FormValidation instance;
    public bool usernameCheck;
    public bool emailCheck;
    public bool passwordCheck;


    private void Start()
    {
        system = EventSystem.current;
        instance = this;
    }
    private void Update()
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

    public void UsernameValidator()
    {
        if(username.text.Length < 5)
        {
            message.color = Color.red;
            Session.instance.message = "Your username needs to be bigger than 5 character";
            username.Select();
            usernameCheck = false;
        }
        else
        {
            message.text = "";
            usernameCheck = true;
        }
    }

    /// <summary>
    /// Checks if the input is according to the conditions, if so, enable the check. otherwise error is shown.
    /// </summary>
    public void EmailValidator()
    {
        if(email.text.IndexOf("@mydavinci.nl") <= 0)
        {
            message.color = Color.red;
            Session.instance.message = "Email needs to be valid";
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
    /// Checks if the input is according to the conditions, if so, enable the check. otherwise error is shown.
    /// </summary>
    public void PasswordValidator()
    {
        if (password.text.Length < 6)
        {
            message.color = Color.red;
            Session.instance.message = "Your password needs to be bigger than 5 character";
            password.Select();
            passwordCheck = false;
        }
        else
        {
            message.text = "";
            passwordCheck = true;
        }
    }



    /// <summary>
    /// Clears all the input fields.
    /// </summary>
    public void ClearData()
    {
        //username.text = string.Empty;
        password.text = string.Empty;
        email.text = string.Empty;
        message.text = string.Empty;
        usernameCheck = false;
        passwordCheck = false;
        emailCheck = false;

    }

    /// <summary>
    /// Registers a user when the button is clicked.
    /// Checks if the validation is correct, if so.
    /// Executes the registration function.
    /// </summary>
    public void OnRegisterButtonClicked()
    {
        if (usernameCheck && passwordCheck && emailCheck)
        {
            //StartCoroutine(Authentication.instance.Register(username.text, email.text, password.text, classCode.options[classCode.value].text));
            new User().Register();
            ClearData();
            //username.text = string.Empty;
        }
       
       
    }
    /// <summary>
    /// Logs in a user when the button is clicked.
    /// Checks if the imput is correct, if so-
    /// login function will be executed.
    /// </summary>
    public void OnLoginButtonClicked()
    {
        if (email.text.Length <= 0 || password.text.Length <= 0)
        {
            Session.instance.message = "Email or Password can't be empty";
        }
        else
        {
            //StartCoroutine(Authentication.instance.Login(email.text, password.text));
            new User().Login();
            ClearData();
        }
    }
    /// <summary>
    /// when clicked it opens the Submit menu
    /// this is the place where you give up your emailadress for a new verification code
    /// </summary>
    public void OnForgotPasswordButtonClicked()
    {
        Launcher.instance.OpenSubmitMenu();
    }

}
