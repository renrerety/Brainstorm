using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMaster : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int hp;

    SpriteRenderer spriteRenderer;
    Transform player;
    Rigidbody2D rb;
    

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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
