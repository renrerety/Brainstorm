using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    
    public static int gold;
    public static int kills;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        BinarySaveFormatter.Serialize();
        
        var data = BinarySaveFormatter.Deserialize();
        PlayerData.gold = data.gold;
        PlayerData.kills = data.kills;
        
        Debug.Log("gold : "+gold+"Kill : "+kills);
    }
}
