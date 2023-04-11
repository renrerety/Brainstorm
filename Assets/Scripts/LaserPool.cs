using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LaserPool : MonoBehaviour
{
    public List<GameObject> laserPoolList = new List<GameObject>();
    [SerializeField] private GameObject Laser;
    private int index;
    private Transform playerTransform;
    
    public void CreatePool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject obj =  Instantiate(Laser,gameObject.transform);
            laserPoolList.Add(obj);
            LaserRay _laserRay = obj.GetComponent<LaserRay>();
            _laserRay._laserPool = this;
            obj.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        if (index >= laserPoolList.Count)
        {
            index = 0;
        }

        GameObject obj = laserPoolList[index++];
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        
        SpriteRenderer _spriteRenderer = obj.GetComponent<SpriteRenderer>();
        _spriteRenderer.size = new Vector2(1, _spriteRenderer.size.y);
    }

    private void Start()
    {
        CreatePool();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
