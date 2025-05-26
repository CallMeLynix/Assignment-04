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

        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);

        StartCoroutine(SendSignUpData(jsonData));
    }

    IEnumerator SendSignUpData(string json)
{
    string url = "https://binusgat.rf.gd/unity-api-test/api/auth/signup.php";

    UnityWebRequest www = new UnityWebRequest(url, "POST");
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
    www.uploadHandler = new UploadHandlerRaw(bodyRaw);
    www.downloadHandler = new DownloadHandlerBuffer();
    www.SetRequestHeader("Content-Type", "application/json");

    yield return www.SendWebRequest();

    if (www.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error: " + www.error);
    }
    else
    {
        Debug.Log("Response: " + www.downloadHandler.text);
        Debug.Log("Form submitted successfully!");
    }
}

}
