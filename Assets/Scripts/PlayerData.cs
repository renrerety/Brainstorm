using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    
    public int gold;
    public int kills;
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
        BinarySaveFormatter.Serialize(120,120);
        
        BinarySaveFormatter.Deserialize();
        
        
        Debug.Log("gold : "+gold+"Kill : "+kills);
    }
}
