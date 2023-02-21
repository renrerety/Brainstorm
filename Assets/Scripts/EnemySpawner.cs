using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    //public static EnemySpawner Instance { get; private set; }

    public AbstractFactory factory;
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnInterval;

    public List<GameObject> activeEnemyList = new List<GameObject>();

    [Inject] private EasyEnemyFactory _easyEnemyFactory;
    [Inject] private MediumEnemyFactory _mediumEnemyFactory;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwapFactory());
        StartCoroutine(SpawnWave());

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator SwapFactory()
    {
        factory = _easyEnemyFactory;
        yield return new WaitForSeconds(300);
        factory = _mediumEnemyFactory;
        yield return new WaitForSeconds(300);
        StartCoroutine(SwapFactory());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            GameObject enemy;
            for (int i = 0; i < factory.waveSize; i++)
            {
                if (i > factory.waveSize - (factory.waveSize / 4))
                {
                    enemy = factory.CreateStrongEnemy();
                }
                else
                {
                    enemy = factory.CreateWeakEnemy();
                }

                Vector3 randomPos = Random.insideUnitCircle.normalized * spawnRadius;
                enemy.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + randomPos;
                activeEnemyList.Add(enemy);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

    }

    public Transform FindNearestEnemy()
    {
        float temp = 200;
        Transform nearestEnemy = null;
        foreach (GameObject enemy in activeEnemyList)
        {
            float distance = Vector2.Distance(playerTransform.position, enemy.transform.position);
            if (distance < temp)
            {
                nearestEnemy = enemy.transform;
                temp = distance;
            }
        }

        temp = 100;
        return nearestEnemy.transform;
    }

    public Transform FindRandomEnemy()
    {
        int rng = (int)Random.Range(0f, activeEnemyList.Count);
        return activeEnemyList[rng].transform;
    }
}

    