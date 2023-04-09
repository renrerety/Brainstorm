using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MapSetup : MonoBehaviour
{
    [Inject] private PlayerMovement playerMovement;
    
    public void SetupMap(string map)
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
