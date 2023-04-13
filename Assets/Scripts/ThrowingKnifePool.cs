using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ThrowingKnifePool : MonoBehaviour
{
    public static ThrowingKnifePool instance;
    public List<GameObject> throwingKnifePoolList = new List<GameObject>();
    [SerializeField] private GameObject ThrowingKnife;

    private int index;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    
    private void CreatePool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject throwingKnife = Instantiate(ThrowingKnife,gameObject.transform);
            ThrowingKnifeProjectile tkProjectile = throwingKnife.GetComponent<ThrowingKnifeProjectile>();
            
            tkProjectile._throwingKnifePool = this;

            throwingKnife.SetActive(false);
            throwingKnifePoolList.Add(throwingKnife);
        }
    }

    public GameObject TakeThrowingKnifeFromPool()
    {
        if (index >= throwingKnifePoolList.Count)
        {
            index = 0;
        }
        GameObject throwingKnife = throwingKnifePoolList[index++];
        throwingKnife.SetActive(true);
        return throwingKnife;
    }

    public void ReturnThrowingKnifeToPool(GameObject throwingKnife)
    {
        throwingKnife.SetActive(false);
        throwingKnife.GetComponent<ThrowingKnifeProjectile>().hit = 0;
        throwingKnife.transform.position = Vector3.zero;
        throwingKnife.transform.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }

}
