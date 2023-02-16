using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourRef : MonoBehaviour
{
    public static MonoBehaviourRef Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }
}
