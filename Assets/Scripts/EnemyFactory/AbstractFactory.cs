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
    public List<GameObject> weakEnemyList = new List<GameObject>();
     public List<GameObject> strongEnemyList = new List<GameObject>();

    [HideInInspector] [Inject] public PlayerHealthProxy _playerHealthProxy;
    [HideInInspector] [Inject] public PlayerHealth _playerHealth;
    [HideInInspector] [Inject] private EasyEnemyFactory _easyEnemyFactory;
    [HideInInspector] [Inject] private MediumEnemyFactory _mediumEnemyFactory;
    [HideInInspector] [Inject] private HardEnemyFactory _hardEnemyFactory;
    [HideInInspector] [Inject] private XpPool _xpPool;
    [HideInInspector] [Inject] private DamagePopupPool _damagePopupPool;
    [HideInInspector] [Inject] public PlayerWeapons _playerWeapons;
    [HideInInspector] [Inject] public EnemySpawner _enemySpawner;
    [HideInInspector] [Inject] private KillCounter _killCounter;

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
        for(int i = 0; i < 3000; i++)
        {
            GameObject weakEnemyInst = Instantiate(weakEnemy, gameObject.transform);
            AIMaster weakMaster =  weakEnemyInst.GetComponent<AIMaster>();
            weakMaster._easyEnemyFactory = _easyEnemyFactory;
            weakMaster._mediumEnemyFactory = _mediumEnemyFactory;
            weakMaster._hardEnemyFactory = _hardEnemyFactory;
            weakMaster._xpPool = _xpPool;
            weakMaster._playerHealth = _playerHealth;
            weakMaster._playerHealthProxy = _playerHealthProxy;
            weakMaster._damagePopupPool = _damagePopupPool;
            weakMaster._playerWeapons = _playerWeapons;
            weakMaster._enemySpawner = _enemySpawner;
            weakMaster._killCounter = _killCounter;
            weakEnemyList.Add(weakEnemyInst);
            weakEnemyInst.SetActive(false);

            GameObject strongEnemyInst = Instantiate(strongEnemy, gameObject.transform);
            AIMaster strongMaster = strongEnemyInst.GetComponent<AIMaster>();
            strongMaster._easyEnemyFactory = _easyEnemyFactory;
            strongMaster._mediumEnemyFactory = _mediumEnemyFactory;
            strongMaster._hardEnemyFactory = _hardEnemyFactory;
            strongMaster._playerHealth = _playerHealth;
            strongMaster._playerHealthProxy = _playerHealthProxy;
            strongMaster._xpPool = _xpPool;
            strongMaster._damagePopupPool = _damagePopupPool;
            strongMaster._playerWeapons = _playerWeapons;
            strongMaster._enemySpawner = _enemySpawner;
            strongMaster._killCounter = _killCounter;
            strongEnemyList.Add(strongEnemyInst);
            strongEnemyInst.SetActive(false);
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
        AIMaster aiMaster = enemy.GetComponent<AIMaster>();
        aiMaster._playerHealthProxy = _playerHealthProxy;
        aiMaster.hp = aiMaster.maxHp;
        enemy.transform.parent = null;
        return enemy;
    }
    public void ReturnEnemyToPool(GameObject enemy)
    {
        _enemySpawner.activeEnemyList.Remove(enemy);
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
        enemy.SetActive(true);
        enemy.transform.parent = null;
        return enemy;
    }

    public void ScaleEnemies()
    {
        foreach (GameObject enemy in weakEnemyList)
        {
            AIMaster aiMaster = enemy.GetComponent<AIMaster>();
                aiMaster.maxHp *= 2;
        }
        foreach (GameObject enemy in strongEnemyList)
        {
            AIMaster aiMaster = enemy.GetComponent<AIMaster>();
                aiMaster.maxHp *= 2;
        }
    }
}
