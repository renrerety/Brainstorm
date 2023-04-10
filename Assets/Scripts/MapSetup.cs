using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MapSetup : MonoBehaviour
{
    [Inject] private PlayerMovement playerMovement;
    
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
                playerMovement.iceMovement = true;
                break;
            case "Plains":
                playerMovement.iceMovement = false;
                break;
        }
    }
}
