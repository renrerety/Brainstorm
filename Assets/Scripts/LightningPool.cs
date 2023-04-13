using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPool : MonoBehaviour
{
    public static LightningPool instance;
    public List<GameObject> lightningPoolList = new List<GameObject>();
    [SerializeField] private GameObject lightningRef;

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
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(lightningRef,transform);
            lightningPoolList.Add(obj);
            obj.SetActive(false);
        }
    }

    public GameObject GetPooledObject(Vector3 position)
    {
        if (index >= lightningPoolList.Count)
        {
            index = 0;
        }

        GameObject obj = lightningPoolList[index++];
        obj.SetActive(true);
        obj.transform.position = position;
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        obj.SetActive(false);
    }
}
