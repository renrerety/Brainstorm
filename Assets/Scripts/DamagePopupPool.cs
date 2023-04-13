using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    public static DamagePopupPool instance;
    private int index;
    [SerializeField] private GameObject damagePopup;
    public List<GameObject> damagePopupList = new List<GameObject>();
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
    

    public void CreatePool()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(damagePopup,transform);
            damagePopupList.Add(obj);
            obj.SetActive(false);
            obj.GetComponent<DamagePopup>()._damagePopupPool = this;
        }
    }

    public void TakePooledObject(Vector3 position,int damageAmount,bool isCritical)
    {
        if (index >= damagePopupList.Count)
        {
            index = 0;
        }
        GameObject obj = damagePopupList[index++];
        obj.SetActive(true);
        obj.transform.position = position;
        obj.GetComponent<DamagePopup>().Setup(damageAmount,isCritical);
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void Start()
    {
        CreatePool();
    }
}
