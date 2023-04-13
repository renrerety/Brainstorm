using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class FireballProjectile : MonoBehaviour
{
    public FireballPool _fireballPool;
    public int damage;

    Transform player;

    [SerializeField] private AudioClip fireballClip;
    [SerializeField] int speed;
    [SerializeField] int max_range;
    public int maxHit;
    public int hit;

    private WeaponMaster fireballWeapon;

    private void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(fireballClip);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fireballWeapon = player.GetComponent<PlayerWeapons>().FindWeapon("Fireball");
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
            _fireballPool.ReturnFireballToPool(gameObject);
        }

        if (hit >= fireballWeapon.maxHit)
        {
           
            _fireballPool.ReturnFireballToPool(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit < fireballWeapon.maxHit)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<AIMaster>().TakeDamage(fireballWeapon.damage);
                hit++;
            }
        }
    }
}
