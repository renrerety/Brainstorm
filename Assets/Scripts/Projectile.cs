using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
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
            player.GetComponentInChildren<WeaponMaster>().ReturnWeaponToPool(gameObject);
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
        else
        {
            player.GetComponentInChildren<WeaponMaster>().ReturnWeaponToPool(gameObject);
        }
       
    }
}
