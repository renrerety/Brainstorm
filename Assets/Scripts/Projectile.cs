using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public int damage;

    Transform player;

    [SerializeField] int speed;
    [SerializeField] int max_range;
    

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
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<AIZombie>().TakeDamage(damage);
            
            player.GetComponentInChildren<FireballWeapon>().ReturnWeaponToPool(gameObject);
        }
    }
}
