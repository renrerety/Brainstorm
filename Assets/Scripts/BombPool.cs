using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BombPool : MonoBehaviour
{
    public List<GameObject> bombPoolList = new List<GameObject>();
    [SerializeField] private GameObject bombRef;
    private int index;

    [Inject] private PlayerWeapons _playerWeapons;
    private void CreatePool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject bombInst = Instantiate(bombRef,gameObject.transform);
            Bomb bomb = bombInst.GetComponent<Bomb>();
            
            bomb._bombPool = this;
            bomb._playerWeapons = _playerWeapons;
            
            bombPoolList.Add(bombInst);
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
        bomb.GetComponent<Bomb>().explosionObj.SetActive(false);
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
