using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Signup : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;

    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject signupCanvas;

    public void Submit()
    {
        StartCoroutine(CreateAccount());
    }

    public IEnumerator CreateAccount()
    {
        bool error = false;
        //Check if email is valid
        try
        {
            string address = new MailAddress(emailInput.text).Address;
        }
        catch (FormatException)
        {
            Error.instance.DisplayError("Error : Email format invalid");
        }

        //Check if username is already used
        string uri = "https://parseapi.back4app.com/users/?where={\"username\":\"" + usernameInput.text + "\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }


            var matches = Regex.Matches(request.downloadHandler.text,
                "\"username\":\"(.[^,]+)",
                RegexOptions.Multiline);

            if (matches.Count > 0)
            {
                Error.instance.DisplayError("Error : the username is already in use");
                error = true;
                yield break;
            }

        }

        //Check if email is already in use
        uri = "https://parseapi.back4app.com/users/?where={\"email\":\"" + emailInput.text + "\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            if (request.downloadHandler.text != "{\"results\":[]}")
            {
                Error.instance.DisplayError("Error : the mail is already in use");
                error = true;
                //yield break;
            }
        }

        if (!error)
        {
            using (var request = new UnityWebRequest("https://parseapi.back4app.com/users",
                       "POST"))
            {
                request.SetRequestHeader("X-Parse-Application-Id",
                    Secrets.appId);
                request.SetRequestHeader("X-Parse-REST-API-Key",
                    Secrets.restApi);
                request.SetRequestHeader("X-Parse-Revocable-Session", "1");
                request.SetRequestHeader("Content-Type",
                    "application/json");

                var data = new
                    { username = usernameInput.text, email = emailInput.text, password = passwordInput.text };
                string json = JsonConvert.SerializeObject(data);

                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    Error.instance.DisplayError("Success !");
                }
            }
            
            using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/UserSaveData",
                       "POST"))
            {
                request.SetRequestHeader("X-Parse-Application-Id",
                    Secrets.appId);
                request.SetRequestHeader("X-Parse-REST-API-Key",
                    Secrets.restApi);
                request.SetRequestHeader("Content-Type",
                    "application/json");

                var data = new
                    { username = usernameInput.text};
                string json = JsonConvert.SerializeObject(data);

                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                request.downloadHandler = new DownloadHandlerBuffer();
                
                var matches = Regex.Matches(request.downloadHandler.text,
                    "\\\"objectId\\\":\\\"(.[^,]+)\"",
                    RegexOptions.Multiline);

                Secrets.saveObjectId = matches[0].Groups[1].ToString();
                
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    Error.instance.DisplayError("Success !");
                }
            }
            
            
            
            
            loginCanvas.SetActive(true);
            signupCanvas.SetActive(false);
            error = false;
        }
    }
}
