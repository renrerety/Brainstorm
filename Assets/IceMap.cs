using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IceMap : MonoBehaviour
{
    [Inject] private MapSetup mapSetup;
    // Start is called before the first frame update
    void Start()
    {
        mapSetup.SetupMap("Ice");
    }
}
