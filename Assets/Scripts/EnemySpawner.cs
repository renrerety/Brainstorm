using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [HideInInspector] public AbstractFactory factory;
    [SerializeField] public float spawnRadius;
    [SerializeField] public float spawnInterval;
    [SerializeField] private float factorySwapTime;

    public List<GameObject> activeEnemyList = new List<GameObject>();
    
    private Transform playerTransform;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Init()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    public void StartWaveLoop()
    {
        InvokeRepeating("WaveLoop",5,spawnInterval);
    }
    public void WaveLoop()
    {
        if (factory.isActive)
        {
            StartCoroutine(factory.SpawnWave());
        }
    }
    public IEnumerator SwapFactory()
    {
        yield return new WaitForSeconds(factorySwapTime);
        factory = MediumEnemyFactory.instance;
        yield return new WaitForSeconds(factorySwapTime);
        factory = HardEnemyFactory.instance;
        yield return new WaitForSeconds(factorySwapTime);
        ScaleFactories();

        StartCoroutine(SwapFactory());
    }

    private void ScaleFactories()
    {
        EasyEnemyFactory.instance.waveSize *= 3;
        MediumEnemyFactory.instance.waveSize *= 3;
        HardEnemyFactory.instance.waveSize *= 3;

        EasyEnemyFactory.instance.ScaleEnemies();
        MediumEnemyFactory.instance.ScaleEnemies();
        HardEnemyFactory.instance.ScaleEnemies();
    }
    


    private void FixedUpdate()
    {
        if (activeEnemyList.Count > 0)
        {
            if (Time.frameCount % 10 == 0)
            {
                if (PlayerWeapons.Instance.FindWeapon("Laser Gun"))
                {
                    //Physics2D.OverlapCircle()
                    //TODO: Optimize code for finding closest enemies
                }
                
                /*activeEnemyList.Sort((a, b) => Vector2.Distance(playerTransform.position, a.transform.position)
                    .CompareTo(
                        Vector2.Distance(playerTransform.position, b.transform.position)));*/
                 
                 activeEnemyList.Sort((x, y) => { return (playerTransform.position - x.transform.position).sqrMagnitude.CompareTo((playerTransform.position - y.transform.position).sqrMagnitude); });
            }
        }
    }

    public Transform FindNearestEnemy(float maxDistance)
    {
        Transform enemyPos = activeEnemyList[0].transform;
        if (Vector2.Distance(enemyPos.position, playerTransform.position) > maxDistance)
        {
            return null;
        }
        return activeEnemyList[0].transform;
    }

    public Transform FindRandomEnemy()
    {
        int rng = (int)Random.Range(0f, activeEnemyList.Count);
        return activeEnemyList[rng].transform;
    }
}

    