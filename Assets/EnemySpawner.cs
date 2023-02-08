using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    AbstractFactory factory;
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnInterval;
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
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
