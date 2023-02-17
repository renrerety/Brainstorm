using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    public static BombPool Instance;

    public List<GameObject> bombPoolList = new List<GameObject>();
    [SerializeField] private GameObject bombRef;
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
    private void CreatePool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject bomb = Instantiate(bombRef,gameObject.transform);
            bomb.SetActive(false);
            bombPoolList.Add(bomb);
        }
    }

    public GameObject TakeBombFromPool()
    {
        if (index >= bombPoolList.Count)
        {
            index = 0;
        }
        GameObject bomb = bombPoolList[index++];
        bomb.SetActive(true);
        return bomb;
    }

    public void ReturnBombToPool(GameObject bomb)
    {
        bomb.SetActive(false);
        bomb.transform.position = Vector3.zero;
        bomb.transform.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }
}
