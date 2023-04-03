using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class PromoCodeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateCodes());
    }

    IEnumerator GenerateCodes()
    {
        for(int i = 0 ; i< 50 ; i++)
        {
            StartCoroutine(CodeGenerator("Gold"));
        }

        yield return new WaitForSeconds(1);
        for(int i = 0 ; i< 50 ; i++)
        {
            StartCoroutine(CodeGenerator("Skin"));
        }
        yield return new WaitForSeconds(1);
        for(int i = 0 ; i< 50 ; i++)
        {
            StartCoroutine(CodeGenerator("Map"));
        }
    }
    IEnumerator CodeGenerator(string typeString)
    {
        string codeGen = Random.Range(1000, 999999999).ToString();
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/Promo",
                   "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("Content-Type",
                "application/json");

            var data = new
                { code = codeGen, redeemed = false, type = typeString};
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
    }
}
