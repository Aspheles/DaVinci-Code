using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class User : MonoBehaviour
{
    public List<IMultipartFormSection> form;
    public void Register()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", FormValidation.instance.email.text),
            new MultipartFormDataSection("username", FormValidation.instance.username.text),
            new MultipartFormDataSection("password", FormValidation.instance.password.text),
            new MultipartFormDataSection("classcode", FormValidation.instance.classCode.options[FormValidation.instance.classCode.value].text)
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.REGISTER);
    }

    public void Login()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("email", FormValidation.instance.email.text),
            new MultipartFormDataSection("password", FormValidation.instance.password.text)
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.LOGIN);
    }

    public void Verify(bool resend)
    {
        if (resend)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("email", Verification.instance.emailInput.text)
            };

            ApiHandler.instance.CallApiRequest("post", form, Request.RESENDCODE);
        }
        else
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("email", Verification.instance.emailInput.text),
                new MultipartFormDataSection("code", Verification.instance.codeInput.text)
            };

            ApiHandler.instance.CallApiRequest("post", form, Request.VERIFY);
        }
       
    }
    public void UpdatePassword(bool resend)
    {
        if (resend)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("email", PasswordValidation.instance.email.text)
            };

            ApiHandler.instance.CallApiRequest("post", form, Request.RESENDCODE);
        }
        else
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("email", Session.instance.email),
                new MultipartFormDataSection("password", PasswordValidation.instance.repeatpassword.text),
                new MultipartFormDataSection("newpassword", PasswordValidation.instance.newpassword.text),
                new MultipartFormDataSection("verificationcode", PasswordValidation.instance.authcode.text)
            };

            ApiHandler.instance.CallApiRequest("post", form, Request.RESETPASSWORD);
        }
       
    }

}
