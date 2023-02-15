using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class XpPool : MonoBehaviour
{
    public static XpPool Instance;

    [SerializeField] private GameObject xpGem;
    private List<GameObject> xpPoolList = new List<GameObject>();
    private int index;

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

    private void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject xp = Instantiate(xpGem,gameObject.transform);
            xpPoolList.Add(xp);
            xp.SetActive(false);
        }
    }

    public GameObject TakeXpFromPool()
    {
        if (index > xpPoolList.Count)
        {
            index = 0;
        }
        GameObject xp = xpPoolList[index++];
        xp.SetActive(true);
        return xp;
    }

    public void ReturnXpToPool(GameObject xp)
    {
        xp.SetActive(false);
        xp.transform.position = Vector3.zero;
        xp.transform.rotation = quaternion.identity;
    }
}
