using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    AbstractFactory factory;
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnInterval;

    public List<GameObject> activeEnemyList = new List<GameObject>();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        factory = EasyEnemyFactory.Instance;
        StartCoroutine(SpawnWave());
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
            for (int i = 0; i < 5; i++)
            {
                if (i != 3)
                {
                    enemy = factory.CreateWeakEnemy();
                }
                else
                {
                    enemy = factory.CreateStrongEnemy();
                }

                Vector3 randomPos = Random.insideUnitCircle * spawnRadius;
                enemy.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + randomPos;
                activeEnemyList.Add(enemy);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
