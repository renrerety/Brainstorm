using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Localization;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PromoCode : MonoBehaviour
{
    public InputField codeInput;

    [SerializeField] TextTranslator _textTranslator;
    [SerializeField] private Skin superBill;

    private string objectId;

    public void RedeemCode()
    {
        StartCoroutine(ValidateCode());
    }

    private IEnumerator ValidateCode()
    {
        string userCode = codeInput.text;
        string uri = "https://parseapi.back4app.com/classes/Promo/?where={\"code\":\"" + userCode + "\"}";
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

            var redeemedMatch = Regex.Match(request.downloadHandler.text, "\"redeemed\":(.[^,]+)",
                RegexOptions.Multiline);
            var typeMatch = Regex.Match(request.downloadHandler.text, "\\\"type\\\":\"(.[^,]+)\"",
                RegexOptions.Multiline);
            var objectIdMatch = Regex.Match(request.downloadHandler.text, "\\\"objectId\\\":\"(.[^,]+)\"",
                RegexOptions.Multiline);

            objectId = objectIdMatch.Groups[1].ToString();

            if (redeemedMatch.Length <= 0)
            {
                DisplayError("invalid_promo");
            }

            if (redeemedMatch.Groups[1].ToString() == "false")
            {
                StartCoroutine(ValidateProfile(userCode, typeMatch.Groups[1].ToString()));
                Debug.Log("Code : " + request.downloadHandler.text);
            }
            else if (redeemedMatch.Groups[1].ToString() == "true")
            {
               DisplayError("used_promo");
            }
        }
    }

    public IEnumerator ValidateProfile(string userCode,string userType)
    {
        using (var request =
               UnityWebRequest.Get("https://parseapi.back4app.com/classes/PlayerProfile/?where={\"username\":\"" +
                                   PlayerData.instance.username + "\"}"))
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

            var redeemedGold = Regex.Match(request.downloadHandler.text, "\"redeemedGold\":(.[^,]+)",
                RegexOptions.Multiline);
            var redeemedSkin = Regex.Match(request.downloadHandler.text, "\"redeemedSkin\":(.[^,]+)",
                RegexOptions.Multiline);
            var redeemedMap = Regex.Match(request.downloadHandler.text, "\"redeemedMap\":(.[^,]+)",
                RegexOptions.Multiline);

            bool redeemed = false;
            switch (userType)
            {
                case "Gold":
                    if (redeemedGold.Groups[1].ToString() == "true")
                    {
                        DisplayError("duplicate_promo");
                        redeemed = true;
                    }
                    break;
                case "Skin":
                    if (redeemedSkin.Groups[1].ToString() == "true")
                    {
                        DisplayError("duplicate_promo");
                        redeemed = true;
                    }
                    break;
                case "Map":
                    if (redeemedMap.Groups[1].ToString() == "true")
                    {
                        DisplayError("duplicate_promo");
                        redeemed = true;
                    }
                    break;
            }

            if (!redeemed)
            {
                StartCoroutine(RedeemCode(userCode,userType ,objectId));
            }
        }
    }

    IEnumerator RedeemCode(string userCode,string userType,string objectId)
    {
        Debug.Log("Type: "+userType);
        Debug.Log("objectId : " +objectId);
         using (var request = new UnityWebRequest(
                   "https://parseapi.back4app.com/classes/Promo/"+objectId,
                   "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("Content-Type",
                "application/json");

            var data = new
                { redeemed = true };
            string json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
        }
        using (var request = new UnityWebRequest(
                   "https://parseapi.back4app.com/classes/PlayerProfile/"+PlayerData.instance.profileId,
                   "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("Content-Type",
                "application/json");
            
            var dataGold = new { redeemedGold = true };
            var dataSkin = new { redeemedSkin = true };
            var dataMap = new { redeemedMap = true };

            string json;
            
            switch (userType)
            {
                case "Gold": json = JsonConvert.SerializeObject(dataGold);
                    break;
                case "Skin": json = JsonConvert.SerializeObject(dataSkin);
                    break;
                case "Map": json = JsonConvert.SerializeObject(dataMap);
                    break;
                default:
                    json = null;
                    DisplayError("error");
                    break;
            }
            
            Debug.Log("Json : "+json);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
            
            switch (userType)
            { 
                    
                case "Gold":
                    PlayerData.instance.persistentData.gold += 50000;
                    int gold = PlayerData.instance.persistentData.gold;
                    int kills = PlayerData.instance.persistentData.kills;
                    LoadPlayerData.instance.UpdateDisplay();
                    break;
                case "Skin":
                    PlayerData.instance.persistentData.superBill = true;
                    break;
                case "Map": //TODO : Give exclusive map access
                    break;
                default:
                    json = null;
                    DisplayError("error");
                    break;
            }
            DisplaySuccess("valid_promo");
            BinarySaveFormatter.Serialize();
            StartCoroutine(BinarySaveFormatter.UploadToDb());
        }
    }
    public void DisplayError(string key)
    {
        _textTranslator.text.color = Color.red;
        _textTranslator.key = key;
        _textTranslator.Translate();
    }

    public void DisplaySuccess(string key)
    {
        _textTranslator.text.color = Color.green;
        _textTranslator.key = key;
        _textTranslator.Translate();
    }
}
