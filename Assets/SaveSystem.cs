using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BinarySaveFormatter.Serialize();
        BinarySaveFormatter.Deserialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
