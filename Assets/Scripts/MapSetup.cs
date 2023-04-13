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
                break;
            case "Plains":
                PlayerMovement.instance.iceMovement = false;
                break;
        }
    }
}
