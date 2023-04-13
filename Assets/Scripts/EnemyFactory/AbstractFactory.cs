using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected GameObject weakEnemy;
    [SerializeField] protected GameObject strongEnemy;

    public bool isActive;
    public int waveSize;
    int weakIndex;
    int strongIndex;
    public List<GameObject> weakEnemyList = new List<GameObject>();
     public List<GameObject> strongEnemyList = new List<GameObject>();
     
     
    [HideInInspector] private EasyEnemyFactory _easyEnemyFactory;
    [HideInInspector] private MediumEnemyFactory _mediumEnemyFactory;
    [HideInInspector] private HardEnemyFactory _hardEnemyFactory;
    [HideInInspector] private XpPool _xpPool;
    [HideInInspector] private DamagePopupPool _damagePopupPool;
    [HideInInspector] public EnemySpawner _enemySpawner;
    [HideInInspector] private KillCounter _killCounter;

    public GameObject enemies;

    private void Start()
    {
        StartCoroutine(CreatePool());
    }
    public GameObject CreateStrongEnemy()
    {
        return TakeStrongEnemyFromPool();
    }

    public GameObject CreateWeakEnemy()
    {
        return TakeWeakEnemyFromPool();
    }
    IEnumerator CreatePool()
    {
        for(int i = 0; i < 500; i++)
        {
            GameObject weakEnemyInst = Instantiate(weakEnemy, gameObject.transform);
            AIMaster weakMaster =  weakEnemyInst.GetComponent<AIMaster>();
            weakMaster._easyEnemyFactory = _easyEnemyFactory;
            weakMaster._mediumEnemyFactory = _mediumEnemyFactory;
            weakMaster._hardEnemyFactory = _hardEnemyFactory;
            weakMaster._xpPool = _xpPool;
            weakMaster._damagePopupPool = _damagePopupPool;
            weakMaster._enemySpawner = _enemySpawner;
            weakMaster._killCounter = _killCounter;
            weakEnemyList.Add(weakEnemyInst);
            weakEnemyInst.SetActive(false);
            yield return new WaitForEndOfFrame();
            

            GameObject strongEnemyInst = Instantiate(strongEnemy, gameObject.transform);
            AIMaster strongMaster = strongEnemyInst.GetComponent<AIMaster>();
            strongMaster._easyEnemyFactory = _easyEnemyFactory;
            strongMaster._mediumEnemyFactory = _mediumEnemyFactory;
            strongMaster._hardEnemyFactory = _hardEnemyFactory;
            strongMaster._xpPool = _xpPool;
            strongMaster._damagePopupPool = _damagePopupPool;
            strongMaster._enemySpawner = _enemySpawner;
            strongMaster._killCounter = _killCounter;
            strongEnemyList.Add(strongEnemyInst);
            strongEnemyInst.SetActive(false);
            
            yield return new WaitForEndOfFrame();
            isActive = true;
        }

        
    }
    public IEnumerator SpawnWave()
    {
        GameObject enemy;
            for (int i = 0; i < waveSize; i++)
            {
                if (i > waveSize - (waveSize / 4))
                {
                    enemy = CreateStrongEnemy();
                }
                else
                {
                    enemy = CreateWeakEnemy();
                }

                Vector3 randomPos = Random.insideUnitCircle.normalized * _enemySpawner.spawnRadius;
                randomPos += new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f));
                enemy.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + randomPos;
                enemy.transform.parent = enemies.transform;
                _enemySpawner.activeEnemyList.Add(enemy);
            }
            yield return new WaitForSeconds(_enemySpawner.spawnInterval);
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
                aiMaster.maxHp *= 3;
        }
        foreach (GameObject enemy in strongEnemyList)
        {
            AIMaster aiMaster = enemy.GetComponent<AIMaster>();
                aiMaster.maxHp *= 3;
        }
    }
}
