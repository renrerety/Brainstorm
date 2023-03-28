using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public PersistentData persistentData;
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

    public void LoadPlayerData()
    {
        BinarySaveFormatter.Deserialize();
    }

    /*private void Start()
    {
        Debug.Log("gold : "+gold+"Kill : "+kills);
    }*/
}
