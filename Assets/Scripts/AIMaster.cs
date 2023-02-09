using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIMaster : MonoBehaviour
{
    public EnemyDifficulty enemyDifficulty;
    public EnemyType enemyType;

    [SerializeField] int speed;
    [SerializeField] int hp;
    [SerializeField] AudioClip enemyHit;

    SpriteRenderer spriteRenderer;
    Transform player;

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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);

        float moveX = transform.position.x - player.transform.position.x;

        Debug.Log(moveX);

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
