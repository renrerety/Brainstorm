using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private LevelData scene;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;

    public bool logged;

    public void Submit()
    {
        StartCoroutine(UserLogin());
    }

    public IEnumerator UserLogin()
    {
        var data = new { email = emailInput.text, password = passwordInput.text };
        
        string json = JsonConvert.SerializeObject(data);
        
        Debug.Log(json);
        
        //string uri = "https://parseapi.back4app.com/login";
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/login",
                   "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            
            request.downloadHandler = new DownloadHandlerBuffer();
            
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
            
            var matches = Regex.Matches(request.downloadHandler.text,
                "\\\"sessionToken\\\":\\\"(.[^,]+)\"}",
                RegexOptions.Multiline);
            
            Secrets.sessionToken = matches[0].Groups[1].ToString();
            
            matches = Regex.Matches(request.downloadHandler.text,
                "\\\"objectId\\\":\\\"(.[^,]+)\"",
                RegexOptions.Multiline);
            
            Secrets.userObject = matches[0].Groups[1].ToString();
            
            Debug.Log("Session token : "+Secrets.sessionToken);
            Debug.Log("UserId: "+Secrets.userObject);
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                Error.instance.DisplayError("Email / Password incorrect");
                yield break;
            }

            SceneManager.LoadScene(scene.name);
            
            
        }
    }
}
