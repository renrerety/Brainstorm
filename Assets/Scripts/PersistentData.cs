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

[Serializable]
public class PersistentData
{
    public int gold;
    public int kills;

    public PersistentData(int gold, int kills)
    {
        this.gold = gold;
        this.kills = kills;
    }
}

public sealed class BinarySaveFormatter
{
    public static IEnumerator UploadToDb()
    {
        Debug.Log("Upload to DB");
        
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/users/"+Secrets.userObject,
                   "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id",
                Secrets.appId);
            request.SetRequestHeader("X-Parse-REST-API-Key",
                Secrets.restApi);
            request.SetRequestHeader("X-Parse-Session-Token",Secrets.sessionToken);
            request.SetRequestHeader("Content-Type","application/json");

            string path = Path.Combine(Application.persistentDataPath,"Save.data");

            var data = new {saveData = path};

            Debug.Log("Path : "+path);

            var json = JsonConvert.SerializeObject(data);
            
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
    public static void Deserialize()
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
    }

    public static void Serialize(int gold, int kills)
    {
        var data = new PersistentData(gold,kills);
        
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
