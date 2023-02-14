using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeProjectile : MonoBehaviour
{
    public int damage;

    Transform player;

    [SerializeField] int speed;
    [SerializeField] int max_range;
    [SerializeField] int maxHit;

    int hit;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += (transform.right * speed) * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) > max_range)
        {
            ThrowingKnifePool.Instance.ReturnThrowingKnifeToPool(gameObject);
        }

        if (hit >= maxHit)
        {
            ThrowingKnifePool.Instance.ReturnThrowingKnifeToPool(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit < maxHit)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<AIMaster>().TakeDamage(damage);
                hit++;
            }
        }
    }
}
