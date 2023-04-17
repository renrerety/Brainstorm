using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestBuilder : IDisposable
{
    private UnityWebRequest request = new UnityWebRequest();
    private string contentType;
    public string json;
    private string url;
    private string method;

    public WebRequestBuilder SetJSON(object json)
    {
        this.json = JsonConvert.SerializeObject(json);
        contentType = "application/json";
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(this.json));
        return this;
    }
    public WebRequestBuilder SetUploadHandler(UploadHandler handler)
    {
        request.uploadHandler = handler;
        return this;
    }

    public WebRequestBuilder SetDownloadHandler(DownloadHandler handler)
    {
        request.downloadHandler = handler;
        return this;
    }
    public WebRequestBuilder SetURL(string args,string method)
    {
        url = "https://parseapi.back4app.com/" + args;
        this.method = method;
        return this;
    }
    public WebRequestBuilder SetURLFull(string url,string method)
    {
        this.url = url;
        this.method = method;
        return this;
    }
    public WebRequestBuilder Revocable()
    {
        request.SetRequestHeader("X-Parse-Revocable-Session","1");
        return this;
    }
    public UnityWebRequest Build()
    {
        request.SetRequestHeader("X-Parse-Application-Id",Secrets.appId);
        request.SetRequestHeader("X-Parse-REST-API-Key",Secrets.restApi);
        request.url = url;
        request.method = method;
        return request;
    }

    public void Dispose()
    {
        request?.Dispose();
    }
}
