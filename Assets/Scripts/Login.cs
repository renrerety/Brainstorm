using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Login : MonoBehaviour
{
    [SerializeField] private LevelData scene;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;

    public void Submit()
    {
        StartCoroutine(UserLogin());
    }

    public IEnumerator UserLogin()
    {
        
        var data = new { email = usernameInput.text, password = passwordInput.text };
        
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
            var errorMatch = Regex.Match(request.downloadHandler.text,"\\\"error\\\":\\\"(.[^,]+)\\\"");
            if (errorMatch.Length > 0)
            {
                Error.instance.DisplayError(errorMatch.Groups[1].ToString());
            }
            
            
            var matches = Regex.Matches(request.downloadHandler.text,
                "\"username\":\"(.[^,]+)\"",
                RegexOptions.Multiline);
            var saveIdMatch = Regex.Match(request.downloadHandler.text, "\"saveId\":\"(.[^,]+)\"");
            

            PlayerData.instance.username = matches[0].Groups[1].ToString();
            PlayerData.instance.saveId = saveIdMatch.Groups[1].ToString();
            

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                Error.instance.DisplayError("Email / Password incorrect");
                yield break;
            }
            
        }
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/PlayerProfile/?where={\"objectId\":\""+PlayerData.instance.saveId+"\"}",
                   "GET"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);

            request.downloadHandler = new DownloadHandlerBuffer();
            
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
            
            
            var urlMatch = Regex.Match(request.downloadHandler.text,
                "\"saveUrl\":\"(.[^,]+)\"",
                RegexOptions.Multiline);
            
            var nameMatch = Regex.Match(request.downloadHandler.text,
                "\"saveName\":\"(.[^,]+)\"",
                RegexOptions.Multiline);
            var profileIdMatch = Regex.Match(request.downloadHandler.text, "\"objectId\":\"(.[^,]+)\"");

            PlayerData.instance.saveUrl = urlMatch.Groups[1].ToString();
            PlayerData.instance.saveName = nameMatch.Groups[1].ToString();
            PlayerData.instance.profileId = profileIdMatch.Groups[1].ToString();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
        }

        if (PlayerData.instance.saveUrl.Length > 0)
        {
            using (var request = UnityWebRequest.Get(PlayerData.instance.saveUrl))
            {
                request.SetRequestHeader("X-Parse-Application-Id",
                    Secrets.appId);
                request.SetRequestHeader("X-Parse-REST-API-Key",
                    Secrets.restApi);
                request.SetRequestHeader("X-Parse-Master-Key",Secrets.masterKey);

                string path = Path.Combine(Application.persistentDataPath, "Save.data");

                request.downloadHandler = new DownloadHandlerFile(path,false);

                yield return request.SendWebRequest();
            
            
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
            }
        }
        PlayerData.instance.logged = true;
        
        if (PlayerData.instance.saveUrl.Length == 0)
        {
            BinarySaveFormatter.CreateEmptySaveData();
            StartCoroutine(BinarySaveFormatter.UploadToDb());
        }
        StartCoroutine(BinarySaveFormatter.UploadToDb());
        SceneLoader.instance.LoadScene(scene,false,new List<string>{"Menu"});
    }
}
