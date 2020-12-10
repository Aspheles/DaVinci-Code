using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FormValidation : MonoBehaviour
{
    [SerializeField] InputField username;
    [SerializeField] InputField password;
    [SerializeField] InputField email;
    public Text message;
    EventSystem system;
    public static FormValidation instance;
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

    public void UsernameValidator()
    {
        if(username.text.Length < 6)
        {
            message.color = Color.red;
            message.text = "Your username needs to be bigger than 5 character";
            username.Select();
        }
        else
        {
            message.text = "";
        }
    }

    public void EmailValidator()
    {
        if(email.text.IndexOf("@") <= 0)
        {
            message.color = Color.red;
            message.text = "Email needs to be valid";
            email.Select();
        }
        else
        {
            message.text = "";
        }
    }

    public void PasswordValidator()
    {
        if (password.text.Length < 6)
        {
            message.color = Color.red;
            message.text = "Your password needs to be bigger than 5 character";
            password.Select();
        }
        else
        {
            message.text = "";
        }
    }

    public void ClearData()
    {

    }

    public void Register()
    {
        Authentication.instance.Register(username.text, email.text, password.text);
        message.color = Color.green;
        
    }

    public void Login()
    {
        Authentication.instance.Login(email.text, password.text);

    }
}
