using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected GameObject weakEnemy;
    [SerializeField] protected GameObject strongEnemy;

    public int waveSize;
    int weakIndex;
    int strongIndex;
    List<GameObject> weakEnemyList = new List<GameObject>();
    List<GameObject> strongEnemyList = new List<GameObject>();

    [Inject] public PlayerHealth _playerHealth;
    [Inject] private EasyEnemyFactory _easyEnemyFactory;
    [Inject] private MediumEnemyFactory _mediumEnemyFactory;
    [Inject] private XpPool _xpPool;

    private void Start()
    {
        CreatePool();
    }
    public GameObject CreateStrongEnemy()
    {
        return TakeStrongEnemyFromPool();
    }

    public GameObject CreateWeakEnemy()
    {
        return TakeWeakEnemyFromPool();
    }
    void CreatePool()
    {
        for(int i = 0; i < 500; i++)
        {
            GameObject weakEnemyInst = Instantiate(weakEnemy, gameObject.transform);
            AIMaster weakMaster =  weakEnemyInst.GetComponent<AIMaster>();
            weakMaster._easyEnemyFactory = _easyEnemyFactory;
            weakMaster._mediumEnemyFactory = _mediumEnemyFactory;
            weakMaster._xpPool = _xpPool;
            weakEnemyList.Add(weakEnemyInst);

            GameObject strongEnemyInst = Instantiate(strongEnemy, gameObject.transform);
            AIMaster strongMaster = strongEnemyInst.GetComponent<AIMaster>();
            strongMaster._easyEnemyFactory = _easyEnemyFactory;
            strongMaster._mediumEnemyFactory = _mediumEnemyFactory;
            strongMaster._xpPool = _xpPool;
            strongEnemyList.Add(strongEnemyInst);
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
        enemy.GetComponent<AIMaster>().enemyDifficulty = EnemyDifficulty.easy;
        enemy.GetComponent<AIMaster>()._playerHealth = _playerHealth;
        enemy.transform.parent = null;
        return enemy;
    }
    public void ReturnEnemyToPool(GameObject enemy)
    {
        EnemySpawner.Instance.activeEnemyList.Remove(enemy);
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;
        enemy.transform.parent = gameObject.transform;
    }
    GameObject TakeStrongEnemyFromPool()
    {
        if (strongIndex >= strongEnemyList.Count)
        {
            strongIndex = 0;
        }
        GameObject enemy = strongEnemyList[strongIndex++];
        enemy.GetComponent<AIMaster>().enemyDifficulty = EnemyDifficulty.easy;
        enemy.SetActive(true);
        return enemy;
    }
}
