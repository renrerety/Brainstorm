using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
    public static PersistentData Deserialize()
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
        
        Debug.Log("Deserialized data :" +data.gold+ data.kills);
        return data;
    }

    public static void Serialize()
    {
        var data = new PersistentData(200,200);
        
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
        Debug.Log("Serialized data :" +data.gold+ data.kills);
    }
}
