using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LightningPool : MonoBehaviour
{
    public List<GameObject> lightningPoolList = new List<GameObject>();
    [SerializeField] private GameObject lightningRef;

    private int index;
    [Inject] private PlayerWeapons _playerWeapons;
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
            obj.GetComponent<Lightning>()._playerWeapons = _playerWeapons;
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
