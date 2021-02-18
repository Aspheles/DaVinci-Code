using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ResetPassword : MonoBehaviour
{
    [SerializeField] InputField repeatpassword;
    [SerializeField] InputField newpassword;
    [SerializeField] InputField email;
    public Text message;
    EventSystem system;
    public static ResetPassword instance;
    public bool emailCheck;
    public bool repeatpasswordCheck;
    public bool newpasswordCheck;

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
        if (newpassword.text == repeatpassword.text)
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
        repeatpassword.text = string.Empty;
        newpassword.text = string.Empty;
        email.text = string.Empty;
        message.text = string.Empty;
        repeatpasswordCheck = false;
        newpasswordCheck = false;
        emailCheck = false;
    }

    public void OnSubmitButtonClicked()
    {
        if (emailCheck = true)
        {
            Launcher.instance.OpenResetPasswordMenu();
        }
        else
        {
            message.color = Color.red;
            message.text = "Your Email adress needs to be valid";
        }
    }

    /// <summary>
    /// Updates the password of a user when the button is clicked.
    /// checks if the input has some input, if so it will start to check whether the input fills the requirements
    /// If that's the case it will check whether the two filled in passwords match
    /// If so, the function will be executed.
    /// If any of the steps fail, show corresponding error
    /// ** Moet nog aangevuld worden met authenticatie met de reset code die vanaf de DB komt **
    /// </summary>
    public void OnResetButtonClicked()
    {
        if (repeatpassword.text.Length <= 0 && newpassword.text.Length <= 0)
        {
            message.color = Color.red;
            message.text = "Both passwords need to be filled in";
            Debug.Log("Nope1");
        }
        else
        {
            if (repeatpassword == true && newpassword == true)
            {
                if (repeatpassword.text == newpassword.text)
                {
                    Debug.Log("succes");
                    //StartCoroutine(Authentication.instance.Reset(email.text, repeatpassword.text, newpassword.text));
                    //ClearData();
                    Launcher.instance.OpenLoginMenu();
                } 
                else
                {
                    Debug.Log("Nope3");
                    message.color = Color.red;
                    message.text = "Your passwords do not match";
                }

            }
            else
            {
                message.color = Color.red;
                message.text = "Passwords need to be bigger than 5 characters";
                Debug.Log("Nope2");
            }

        }
    }

    public IEnumerator OnResendCodeButtonClicked(string email)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", email)
        };

        UnityWebRequest www = UnityWebRequest.Post(Request.RESETPASSWORD, formData);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data);

            Launcher.instance.OpenResetPasswordMenu();
        }
    }

}
