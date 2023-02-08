using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyFactory : AbstractFactory
{
    public static EasyEnemyFactory Instance;

    int weakIndex;
    int strongIndex;
    List<GameObject> weakEnemyList = new List<GameObject>();
    List<GameObject> strongEnemyList = new List<GameObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        CreatePool();
    }
    public override GameObject CreateStrongEnemy()
    {
        return TakeStrongEnemyFromPool();
    }

    public override GameObject CreateWeakEnemy()
    {
        return TakeWeakEnemyFromPool();
    }
    void CreatePool()
    {
        for(int i = 0; i < 200; i++)
        {
            weakEnemyList.Add(Instantiate(weakEnemy,gameObject.transform));
            strongEnemyList.Add(Instantiate(strongEnemy,gameObject.transform));
        }
        foreach (GameObject enemy in weakEnemyList)
        {
            enemy.SetActive(false);
        }
        foreach (GameObject enemy in strongEnemyList)
        {
            enemy.SetActive(false);
        }
    }
    GameObject TakeWeakEnemyFromPool()
    {
        if(weakIndex >= weakEnemyList.Count)
        {
            weakIndex = 0;
        }
        GameObject enemy = weakEnemyList[weakIndex++];
        enemy.SetActive(true);
        return enemy;
    }
    public void ReturnWeakEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;
    }
    GameObject TakeStrongEnemyFromPool()
    {
        if (strongIndex >= strongEnemyList.Count)
        {
            strongIndex = 0;
        }
        GameObject enemy = strongEnemyList[strongIndex++];
        enemy.SetActive(true);
        return enemy;
    }
    public void ReturnStrongEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;
    }
}
