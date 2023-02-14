using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TNRD;
using UnityEngine;

public class PowerUp_Clock : PowerUpMaster, IPowerUp
{
    private IMovement stopStrategy;

    private IMovement walkStrategy;

    private void Start()
    {
        stopStrategy = ScriptableObject.CreateInstance<StopMoving>();
        walkStrategy = ScriptableObject.CreateInstance<WalkTowardsPlayer>();
    }

    public override void ApplyEffect()
    {
        StartCoroutine(FreezeEnemies());
    }

    IEnumerator FreezeEnemies()
    {
        foreach (GameObject enemy in EnemySpawner.Instance.activeEnemyList)
        {
            enemy.GetComponent<AIMaster>().movementStrategy = stopStrategy;
            enemy.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        yield return new WaitForSeconds(5);
        foreach (GameObject enemy in EnemySpawner.Instance.activeEnemyList)
        {
            enemy.GetComponent<AIMaster>().movementStrategy = walkStrategy;
            enemy.GetComponent<SpriteRenderer>().color = Color.white;
        }
        Destroy(gameObject);
    }
}
