using System.Collections;
using System.Collections.Generic;
using System.IO;
using TNRD;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMaster : MonoBehaviour
{
    public EnemyDifficulty enemyDifficulty;
    public EnemyType enemyType;

    public IMovement movementStrategy;

    [SerializeField] int speed;
    [SerializeField] int hp;
    [SerializeField] AudioClip enemyHit;

    SpriteRenderer spriteRenderer;
    public Transform player;

    [SerializeField] GameObject[] powerUps = new GameObject[5];

    public void TakeDamage(int damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(enemyHit);
        StartCoroutine(Flicker());
        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Flicker()
    {
        Color temp = spriteRenderer.color;
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = temp;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        
        RandomDrop();
        DropXp();
        ReturnToPool();
    }

    private void RandomDrop()
    {
        int rng = Random.Range(0, 101);
        if (rng <= 10)
        {
            GameObject powerUp;
            rng = Random.Range(1, 5);
            switch (rng)
            {
                case 1:
                    powerUp = powerUps[0];
                    break;
                case 2:
                    powerUp = powerUps[1];
                    break;
                case 3:
                    powerUp = powerUps[2];
                    break;
                case 4:
                    powerUp = powerUps[3];
                    break;
                default:
                    powerUp = powerUps[0];
                    break;
            }

            GameObject obj = Instantiate(powerUp);
            obj.transform.position = gameObject.transform.position;
        }
    }

    private void ReturnToPool()
    {
        switch (enemyDifficulty)
        {
            case EnemyDifficulty.easy:
                EasyEnemyFactory.Instance.ReturnEnemyToPool(gameObject);
                break;
            case EnemyDifficulty.medium:
                break;
            case EnemyDifficulty.hard:
                break;
        }
    }

    private void DropXp()
    {
        GameObject xp = XpPool.Instance.TakeXpFromPool();
        xp.transform.position = gameObject.transform.position;
    }

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementStrategy = new WalkTowardsPlayer();
    }

    public virtual void Update()
    {
        movementStrategy.Move(gameObject.transform, player);
        FlipTowardsPlayer();
    }

    private void FlipTowardsPlayer()
    {
        float moveX = transform.position.x - player.transform.position.x;

        if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
public enum EnemyType
{
    weak,
    strong
}
public enum EnemyDifficulty
{
    easy,
    hard,
    medium
}
