using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDropdownValues : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt("Dropdown");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
