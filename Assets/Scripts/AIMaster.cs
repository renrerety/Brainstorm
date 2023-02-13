using System.Collections;
using System.Collections.Generic;
using System.IO;
using TNRD;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class AIMaster : MonoBehaviour
{
    public EnemyDifficulty enemyDifficulty;
    public EnemyType enemyType;

    public IMovement movementStrategy;

    [SerializeField] int speed;
    [SerializeField] int hp;
    [SerializeField] AudioClip enemyHit;

    SpriteRenderer spriteRenderer;
    public Transform player;

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
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
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
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
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
public class Strategy
{
    SerializableInterface<IMovement> strategy;
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
