using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    [SerializeField] private string map;

    private void Start()
    {
        SetupMap();
    }

    private void SetupMap()
    {
        switch (map)
        {
            case "Ice":
                
                PlayerMovement.instance.iceMovement = true;
                PlayerMovement.instance.speed = 15 + PlayerMovement.instance.speedModifier;
                break;
            case "Plains":
                PlayerMovement.instance.Init();
                PlayerMovement.instance.iceMovement = false;
                PlayerMovement.instance.speed = 3 + PlayerMovement.instance.speedModifier;
                break;
        }
    }
}
