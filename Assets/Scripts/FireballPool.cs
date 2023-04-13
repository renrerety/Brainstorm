using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    public static FireballPool instance;
    public List<GameObject> fireballPoolList = new List<GameObject>();

    [SerializeField] private GameObject fireball;
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
            GameObject fireball = Instantiate(this.fireball,gameObject.transform);
            fireball.GetComponent<FireballProjectile>()._fireballPool = this;
            this.fireball.SetActive(false);
            fireballPoolList.Add(fireball);
        }
    }

    public GameObject TakeFireballFromPool()
    {
        if (index >= fireballPoolList.Count)
        {
            index = 0;
        }
        GameObject fireball = fireballPoolList[index++];
        fireball.SetActive(true);
        return fireball;
    }

    public void ReturnFireballToPool(GameObject fireball)
    {
        fireball.SetActive(false);
        fireball.GetComponent<FireballProjectile>().hit = 0;
        fireball.transform.position = Vector3.zero;
        fireball.transform.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }

}
