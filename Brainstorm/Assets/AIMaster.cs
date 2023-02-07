using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMaster : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    [SerializeField] int speed;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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
