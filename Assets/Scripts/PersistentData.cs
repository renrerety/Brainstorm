using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class PersistentData
{
    public int gold;
    public int kills;

    public PlayerUpgrades upgrades;

    public PersistentData(int gold, int kills, PlayerUpgrades upgrades)
    {
        this.gold = gold;
        this.kills = kills;
        this.upgrades = upgrades;
    }
}

public sealed class BinarySaveFormatter
{
    public static IEnumerator UploadToDb()
    {
        string saveNameBuffer = "";
        if (PlayerData.instance.saveName.Length != 0)
        {
            saveNameBuffer = PlayerData.instance.saveName;
        }

        using (var request = new UnityWebRequest("https://parseapi.back4app.com/parse/files/Save.data",
                   "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("X-Parse-Master-Key",Secrets.masterKey);
            request.SetRequestHeader("X-Parse-File-Key",Secrets.fileKey);
            
            request.SetRequestHeader("Content-Type","application/octet-stream");

            string path = Application.persistentDataPath + "/Save.data";
            
            request.uploadHandler = new UploadHandlerFile(path);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
            
            var match = Regex.Match(request.downloadHandler.text,
                "\\\"url\\\":\\\"(.[^,]+)\",",
                RegexOptions.Multiline);

            PlayerData.instance.saveUrl = match.Groups[1].ToString();
            
            match = Regex.Match(request.downloadHandler.text,
                "\\\"name\\\":\"(.+)\"",
                RegexOptions.Multiline);

            PlayerData.instance.saveName = match.Groups[1].ToString();
            Debug.Log(PlayerData.instance.saveName);
        }
        
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/PlayerProfile/"+PlayerData.instance.saveId,
                   "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("Content-Type","application/json");

            var data = new {saveName = PlayerData.instance.saveName,saveUrl = PlayerData.instance.saveUrl};
            
            var json = JsonConvert.SerializeObject(data);
            
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);
        }

        if (saveNameBuffer.Length > 0)
        {
            using (var request = new UnityWebRequest(
                       "https://parseapi.back4app.com/parse/files/"+saveNameBuffer,
                       "DELETE"))
            {
                request.SetRequestHeader("X-Parse-Application-Id",
                    Secrets.appId);
                request.SetRequestHeader("X-Parse-REST-API-Key",Secrets.restApi);
                request.SetRequestHeader("X-Parse-Master-Key",Secrets.masterKey);
                
                request.downloadHandler = new DownloadHandlerBuffer();
            
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
                Debug.Log("DELETE");
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
    
    public static void Deserialize()
    {
        if (PlayerData.instance.logged)
        {
            PersistentData data  = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath,"Save.data"), FileMode.Open);
            try
            {
                BinaryFormatter saveFormatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                data = (PersistentData) saveFormatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        
            Debug.Log("Deserialized data :" +data.gold+" "+ data.kills);
            PlayerData.instance.persistentData = data;
            PlayerData.instance.persistentData.upgrades = data.upgrades;
            
        }
    }

    public static void Serialize()
    {
        if (PlayerData.instance.logged)
        {
            var data = new PersistentData(PlayerData.instance.persistentData.gold, PlayerData.instance.persistentData.kills, new PlayerUpgrades
            (PlayerData.instance.persistentData.upgrades.hpUp,
                PlayerData.instance.persistentData.upgrades.damageUp,
                PlayerData.instance.persistentData.upgrades.speedUp,
                PlayerData.instance.persistentData.upgrades.speedUp));
        
            // To serialize the hashtable and its key/value pairs,
            // you must first open a stream for writing.
            // In this case, use a file stream.
            FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath,"Save.data"), FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter saveFormatter = new BinaryFormatter();
            try
            {
                saveFormatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            Debug.Log("Serialized data :" +data.gold+" " +data.kills);
        }
    }
}
