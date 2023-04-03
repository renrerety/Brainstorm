using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Localization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PromoCode : MonoBehaviour
{
    public InputField codeInput;
    
    [SerializeField] TextTranslator _textTranslator;
    
    public void RedeemCode()
    {
        StartCoroutine(ValidateCode());
    }

    private IEnumerator ValidateCode()
    {
        string uri = "https://parseapi.back4app.com/classes/Promo/?where={\"code\":\"" + codeInput.text + "\"}";
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
            var typeMatch = Regex.Match(request.downloadHandler.text, "\"type\":(.[^,]+)",
                RegexOptions.Multiline);

            if (redeemedMatch.Length <= 0)
            {
                _textTranslator.text.color = Color.red;
                _textTranslator.key = "invalid_promo";
                _textTranslator.Translate();
            }
            if (redeemedMatch.Groups[1].ToString() == "false")
            {
                _textTranslator.text.color = Color.green;
                _textTranslator.key = "valid_promo";
                _textTranslator.Translate();
                Debug.Log("Code : "+request.downloadHandler.text);
            }
            else if(redeemedMatch.Groups[1].ToString() == "true")
            {
                _textTranslator.text.color = Color.red;
                _textTranslator.key = "used_promo";
                _textTranslator.Translate();
            }
        }
    }

    /*public void DisplayMessage(string errorString)
    {
        _textTranslator.text.text = errorString;
    }*/
}
