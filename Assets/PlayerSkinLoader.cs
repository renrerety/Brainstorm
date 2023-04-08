using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = PlayerData.instance.currentSkin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
