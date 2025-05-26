using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class SignUpData
{
    public string email;
    public string password;
    public string createDate;
}

public class SignUpForm : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
    }

    void OnSubmit()
    {
        if (passwordInput.text != confirmPasswordInput.text)
        {
            Debug.Log("Passwords do not match!");
            return;
        }

        SignUpData data = new SignUpData
        {
            email = emailInput.text,
            password = passwordInput.text,
            createDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }


}
