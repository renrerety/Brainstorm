using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantCanvasSingleton : MonoBehaviour
{
    public static PersistantCanvasSingleton instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(base.gameObject); 
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
}
